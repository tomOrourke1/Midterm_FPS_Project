using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVelocityReciever : MonoBehaviour, IApplyVelocity
{

    [Header("--- States ---")]
    [SerializeField] PlayerMovementState moveState;
    IApplyVelocity playerVel;
    private void Start()
    {
        playerVel = moveState.GetComponent<IApplyVelocity>();
    }

    private void Update()
    {
        
    }

    public void ApplyVelocity(Vector3 velocity)
    {
        playerVel.ApplyVelocity(velocity);
    }


    Vector3 ClampVector3Min(Vector3 vector, float min, float change)
    {
        var dir = vector.normalized;

        var delta = dir * change;

        Vector3 nextVec = vector + delta;

        var dot = Vector3.Dot(dir, nextVec);

        var initMag = vector.magnitude;
        var nextMag = nextVec.magnitude;


        if(nextMag > initMag || dot < 0 || nextMag < min)
        {
            nextVec = dir * min;
        }


        return nextVec;
    }



}
