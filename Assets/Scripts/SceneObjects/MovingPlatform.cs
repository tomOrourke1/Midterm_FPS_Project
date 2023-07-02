using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, IEnvironment
{
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;
    [SerializeField] float waitTime;
    /*[Range(1, 100)]*/
    [SerializeField] float speed;

    float currentTime;
    bool toEnd;
    bool waiting;

    // Added these to store initial moving platform stats
    Transform initialStartPos;
    Transform initialEndPos;

    // Start is called before the first frame update
    void Start()
    {
        initialStartPos = startPos;
        initialEndPos = endPos;

        waiting = false;

        StopObject();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Debug.Log(GetComponent<Rigidbody>().velocity);

        if (!waiting)
        {
            move();
        }
        else
        {
            wait();
        }
    }

    void move()
    {
        currentTime += Time.deltaTime * speed;
        float t = currentTime;

        var currStart = toEnd ? endPos : startPos;
        var currEnd = toEnd ? startPos : endPos;

        var currPos = Vector3.MoveTowards(currStart.localPosition, currEnd.localPosition, t);

        transform.localPosition = currPos;

        if (t >= Vector3.Distance(currStart.localPosition, currEnd.localPosition))
        {
            toEnd = !toEnd;
            currentTime = 0;
            waiting = true;
        }
    }

    void wait()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= waitTime)
        {
            currentTime = 0;
            waiting = false;
        }
    }

    // This is a function tied to IEnvironment meant to be used to reset a room
    public void ResetObject()
    {
        //// This needs to be done because I swap the start and end positions when the platform is heading backwards.
        //startPos = initialStartPos;
        //endPos = initialEndPos;

        //currentTime = 0;
    }

    public void StartObject()
    {
        gameObject.SetActive(true);

        // This needs to be done because I swap the start and end positions when the platform is heading backwards.
        startPos = initialStartPos;
        endPos = initialEndPos;

        // Restarts the move
        currentTime = 0;

        toEnd = false;

        // Move platform to start position
        transform.localPosition = startPos.localPosition;
    }

    public void StopObject()
    {
        gameObject.SetActive(false);
    }
}
