using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class LaserCast : MonoBehaviour
{
    // Laser
    LineRenderer laser;

    // Laser positions
    Vector3 startPos = Vector3.zero;
    Vector3 direction = Vector3.zero;

    // Laser Stats
    float damage = 0;
    float range = 0;

    bool laserOn = false;

    // Laser FX
    ParticleSystem impactFX;
    Light impactLight;


    // This Function should be called every frame by an object's update
    public RaycastHit RecieveLaser(LineRenderer Laser, Vector3 StartPos, Vector3 Direction, float Damage, float Range, ParticleSystem ImpactFX, Light ImpactLight)
    {
        laser = Laser;

        startPos = StartPos;
        direction = Direction;

        damage = Damage;
        range = Range;

        impactFX = ImpactFX;
        impactLight = ImpactLight;

        return CastLaser();
    }

    RaycastHit CastLaser()
    {
        // Checks to see if the laser hits
        RaycastHit hit = RayCast();

        if (hit.collider != null)
        {
            // Updates the line renderer on a miss
            OnHit(hit.point);
        } 
        else
        {
            // Updates the line renderer on a hit
            OnMiss();
        }

        return hit;
    }
    
    RaycastHit RayCast()
    {
        RaycastHit hit;

        Physics.Raycast(startPos, direction, out hit, range);

        return hit;
    }

    void OnHit(Vector3 hitPoint)
    {
        laser.SetPosition(0, startPos);
        laser.SetPosition(1, hitPoint);

        Vector3 dir = startPos - hitPoint;
        impactFX.Play();
        impactFX.transform.position = hitPoint;
        impactFX.transform.rotation = Quaternion.LookRotation(dir);
    }

    void OnMiss()
    {
        // Stop reflections because there are no colliders
        //if (StoredReflector != null)
        //{
        //    StopReflections();
        //}

        Vector3 endPoint;
        endPoint = startPos;

        Vector3 maxDistPoint = direction * range;
        endPoint += maxDistPoint;

        laser.SetPosition(0, startPos);
        laser.SetPosition(1, endPoint);

        // FX Playing
        Vector3 dir = startPos - endPoint;
        impactFX.Play();
        impactFX.transform.position = endPoint;
        impactFX.transform.rotation = Quaternion.LookRotation(dir);
    }



    

}
