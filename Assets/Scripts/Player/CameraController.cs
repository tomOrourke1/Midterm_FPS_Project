using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float sensitivity;

    [SerializeField] int lockVerMin;
    [SerializeField] int lockVerMax;

    [SerializeField] bool invertY;

    float zTiltOrig;
    float lockZMin;
    float lockZMax;
    float zTilt;
    float zTiltCurrent;
    bool tilting;

    float xRotation;

    // Start is called before the first frame update
    void Start()
    {
        sensitivity = GameManager.instance.GetSettingsManager().settings.mouseSensitivity;
        tilting = false;
        zTiltCurrent = 0;
        zTiltOrig = transform.localRotation.z;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if ( !tilting )
        {
            zTiltCurrent = Mathf.MoveTowards(zTiltCurrent, zTiltOrig, Time.deltaTime * 20);
        } else
        {
            zTiltCurrent = Mathf.Lerp(zTiltCurrent, zTilt, Time.deltaTime * 100);
            if (zTiltCurrent == zTilt)
            {
                tilting = false;
            }
        }
        

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
        zTiltCurrent = Mathf.Clamp(zTiltCurrent, lockZMin, lockZMax);

        // Rotates up and down (X-axis)
        transform.localRotation = Quaternion.Euler(xRotation, 0, zTiltCurrent);

        // Rotates left and right (Y-axis)
        transform.parent.Rotate(Vector3.up * mouseX);

    }

    public void DashCam(float tiltDir, float min, float max)
    {
        lockZMin = min;
        lockZMax = max;
        zTilt = tiltDir;
        transform.Rotate(Vector3.forward, tiltDir);
        tilting = true;
    }

    public void SetSensitivity(float s)
    {
        sensitivity = s;
    }

    public float GetSensitivity()
    {
        return sensitivity;
    }

    public void SetInvert(bool b)
    {
        invertY = b;
    }

    public bool GetInvert()
    {
        return invertY;
    }

    public void ResetCamera()
    {
        tilting = false;
        zTiltCurrent = zTiltOrig;
    }
}
