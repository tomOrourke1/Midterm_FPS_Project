using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;
    [SerializeField] float waitTime;
    [Range(1, 100)][SerializeField] float speed;

    Transform placeholder;
    float translatedSpeed;
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
        translatedSpeed = 0.0001f * (speed);
        if (currentDistance >= 1)
        {
            moving = false;
            StartCoroutine(Wait());
        }
        else if (moving)
        {
            // moving
            currentDistance += translatedSpeed;
            transform.localPosition = Vector3.Lerp(startPos.localPosition, endPos.localPosition, currentDistance);
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
