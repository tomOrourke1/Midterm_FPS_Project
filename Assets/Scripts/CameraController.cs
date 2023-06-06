using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] int sensitivity;

    [SerializeField] int lockVerMin;
    [SerializeField] int lockVerMax;

    [SerializeField] bool invertY;

    float xRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;

        // mouse's y location will modify the camera's x rotation
        if (invertY)
        {
            xRotation += mouseY;
        }
        else
        {
            xRotation -= mouseY;
        }

        // Limit up and down
        xRotation = Mathf.Clamp(xRotation, lockVerMin, lockVerMax);

        // Rotates up and down (X-axis)
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        // Rotates left and right (Y-axis)
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
