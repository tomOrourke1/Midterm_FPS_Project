using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBlock : MonoBehaviour
{
    [Header("----- Rotation -----")]
    [SerializeField] float rotationSpeed;
    [SerializeField] bool clockwise;

    [Header("----- Smooth Rotation -----")]
    [SerializeField] bool smoothRotation;
    [SerializeField] float stopDuration;
    [SerializeField] float stopRate;

    Quaternion currentRotation;

    // Start is called before the first frame update
    void Start()
    {
        currentRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Quaternion q;
       

        // Quaternion.Lerp(currentRotation, q, rotationSpeed * Time.deltaTime);
    }
}
