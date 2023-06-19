using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomsPlatformMover : MonoBehaviour
{

    [SerializeField] Transform objectToMove;
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;

    [SerializeField] float speed;


    [SerializeField] float timeToWait;



    float currentTime = 0;


    bool toEnd = false;
    bool waiting;

    private void FixedUpdate()
    {

        if(!waiting)
        {

            currentTime += Time.deltaTime * speed;


            float t = currentTime;

            var currPos = toEnd ? endPos : startPos;
            var currEnd = toEnd ? startPos : endPos;

            var desPos = Vector3.MoveTowards(currPos.localPosition, currEnd.localPosition, t);


            objectToMove.localPosition = desPos;



            if (t >= Vector3.Distance(currPos.localPosition, currEnd.localPosition))
            {
                toEnd = !toEnd;
                currentTime = 0;
                waiting = true;
            }

        }
        else
        {

            currentTime += Time.deltaTime;

            if(currentTime >= timeToWait)
            {
                currentTime = 0;
                waiting = false;
            }


        }





        
    }





}
