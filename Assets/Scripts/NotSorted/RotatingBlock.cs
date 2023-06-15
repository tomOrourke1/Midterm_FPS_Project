using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBlock : MonoBehaviour
{
    [Header("----- Rotation -----")]
    [SerializeField] float rotationSpeed;
    [SerializeField] bool clockwise;
    [SerializeField] float rotationAngle;

    [Header("----- Intermittant Rotation -----")]
    [SerializeField] bool intermittantRotation;
    [SerializeField] float stopDuration;
    [SerializeField] float stopRate;


    Quaternion currentRotation;
    
    float timeCount;
    Quaternion newRotation;

    // Update is called once per frame
    void Update()
    {
        if (clockwise)
        {
            rotationAngle *= -1;
        }

        var rotation = Quaternion.AngleAxis(rotationAngle * Time.deltaTime, Vector3.up);

        transform.localRotation = Quaternion.Lerp(transform.localRotation, transform.localRotation * rotation, Time.deltaTime * rotationSpeed);
    }
}
