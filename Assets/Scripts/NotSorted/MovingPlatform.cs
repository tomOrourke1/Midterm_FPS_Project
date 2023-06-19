using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, IEnvironment
{
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;
    [SerializeField] float waitTime;
    [Range(1, 100)][SerializeField] float speed;

    Transform placeholder;
    float totalDistance;
    float currentDistance;
    bool moving;

    // Added these to store initial moving platform stats
    Transform initialStartPos;
    Transform initialEndPos;

    // Start is called before the first frame update
    void Start()
    {
        initialStartPos = startPos;
        initialEndPos = endPos;
        StopObject();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        totalDistance = Vector3.Distance(startPos.position, endPos.position);

        if (currentDistance >= totalDistance)
        {
            moving = false;
            StartCoroutine(Wait());
        }
        else if (moving)
        {
            // moving
            currentDistance = Math.Clamp(currentDistance + speed * Time.deltaTime, 0, totalDistance);
            transform.localPosition = Vector3.MoveTowards(startPos.localPosition, endPos.localPosition, currentDistance);
        }
    }
    
    IEnumerator Wait()
    {
        currentDistance = 0;

        placeholder = startPos;
        startPos = endPos;
        endPos = placeholder;

        yield return new WaitForSeconds(2);

        moving = true;
    }

    // This is a function tied to IEnvironment meant to be used to reset a room
    public void ResetObject()
    {
        // This needs to be done because I swap the start and end positions when the platform is heading backwards.
        startPos = initialStartPos;
        endPos = initialEndPos;

        currentDistance = 0;
    }

    public void StartObject()
    {
        gameObject.SetActive(true);
        ResetObject();

        moving = true;
        transform.localPosition = startPos.localPosition;
    }

    public void StopObject()
    {
        //gameObject.SetActive(false);
    }
}
