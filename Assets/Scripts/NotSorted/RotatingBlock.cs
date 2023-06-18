using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBlock : MonoBehaviour, IEnvironment
{
    [Header("----- Rotation -----")]
    [SerializeField] float rotationSpeed;
    [SerializeField, Range(-360, 360)] float rotationAngle;

    [Header("----- Intermittant Rotation -----")]
    [SerializeField] bool intermittantRotation;
    [SerializeField] float stopDuration;

    bool rotating;
    bool waiting;
    Quaternion rotation;

    private void Start()
    {
        rotating = false;
        waiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (intermittantRotation)
        {
            IntermittantRotation();
        }
        else
        {
            SmoothRotation();
        }


    }

    void IntermittantRotation()
    {
        if (!rotating)
        {
            rotation = transform.localRotation * Quaternion.AngleAxis(rotationAngle, Vector3.up);
            rotating = true;
        }
        else
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rotation, Time.deltaTime * rotationSpeed);

            if (transform.localRotation != rotation && !waiting)
            {
                StartCoroutine(wait());
            }
        }
    }

    IEnumerator wait()
    {
        waiting = true;
        yield return new WaitForSeconds(stopDuration);
        rotating = false;
        waiting = false;
    }

    void SmoothRotation()
    {
        rotation = Quaternion.AngleAxis(90, Vector3.up);

        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, transform.localRotation * rotation, Time.deltaTime * rotationSpeed);
    }

    public void ResetObject()
    {
        // Come back to this
    }

    public void StartObject()
    {
        gameObject.SetActive(true);
    }

    public void StopObject()
    {
        gameObject.SetActive(false);
    }
}
