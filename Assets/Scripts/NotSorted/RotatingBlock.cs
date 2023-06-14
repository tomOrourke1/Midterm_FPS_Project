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
    [SerializeField] float rotationAngle;


    Quaternion currentRotation;
    
    float timeCount;
    Quaternion newRotation;

    // Update is called once per frame
    void Update()
    {
        var rotation = Quaternion.AngleAxis(rotationAngle * Time.deltaTime, Vector3.up);

        transform.localRotation = Quaternion.Lerp(transform.localRotation, transform.localRotation * rotation, Time.deltaTime * rotationSpeed);
    }
}
