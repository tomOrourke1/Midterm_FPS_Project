using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;
    [SerializeField] float waitTime;
    [Range(1, 100)][SerializeField] float speed;

    Transform placeholder;
    float totalDistance;
    float currentDistance;
    bool moving;

    // Start is called before the first frame update
    void Start()
    {
        moving = true;
        transform.localPosition = startPos.localPosition;
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
}
