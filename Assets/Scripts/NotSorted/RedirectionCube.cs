using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedirectionCube : MonoBehaviour, IReflector
{
    bool isReflecting = false;

    float laserDamage = 0;
    float laserRange = 0;

    LineRenderer laser;

    ParticleSystem impactFX;
    Light impactLight;

    GameObject StoredReflector;

    // Start is called before the first frame update
    void Start()
    {
        laser = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReflecting)
        {
            laser.enabled = true;
            HitHandlers(CastLaser());
        }
        else
        {
            laser.enabled = false;
        }
    }

    void HitHandlers(RaycastHit hit)
    {
        if (hit.collider != null)
        {
            HandleDamage(hit);
            HandleReflect(hit);
        }
        else
        {
            StopReflections();
        }
    }

    RaycastHit CastLaser()
    {
        return gameObject.GetComponent<LaserCast>().RecieveLaser(laser, transform.position, transform.forward, laserDamage, laserRange, impactFX, impactLight);
    }

    void StopReflections()
    {
        StoredReflector?.GetComponent<IReflector>()?.StopReflection(laser);
        StoredReflector = null;
    }


    void HandleReflect(RaycastHit hit)
    {
        IReflector reflector = hit.collider.GetComponent<IReflector>();

        

        if (CompareWithStoredReflector(hit))
        {
            //Debug.LogError("Mirror");

            // Get Remaining distance
            float reflectDist = laserRange - hit.distance;

            // call reflect
            reflector.Reflect(reflectDist, laserDamage, impactFX, impactLight, hit, hit.point - transform.position, laser);
        }
        else if (reflector != null && !reflector.AlreadyReflecting() && !CompareWithStoredReflector(hit))
        {
            // Stop the old reflector
            StoredReflector?.GetComponent<IReflector>().StopReflection(laser);
            //StopReflections();

            // store the collider
            StoredReflector = hit.collider.gameObject;

            //Debug.Log((StoredReflector != null) + " " + (hit.collider.gameObject == StoredReflector.gameObject));

            // Get Remaining distance
            float reflectDist = laserRange - hit.distance;

            // call reflect
            reflector.Reflect(reflectDist, laserDamage, impactFX, impactLight, hit, hit.point - transform.position, laser);
        }
        else if (reflector == null && StoredReflector != null)
        {
            StopReflections();
        }
    }

    bool CompareWithStoredReflector(RaycastHit hit)
    {
        if (StoredReflector != null && hit.collider.gameObject == StoredReflector.gameObject)
        {
            return true;
        }

        return false;
    }

    void HandleDamage(RaycastHit hit)
    {
        IDamagable damageable = hit.collider.GetComponent<IDamagable>();

        if (damageable != null)
        {
            damageable.TakeDamage(laserDamage * Time.deltaTime);
        }
    }

    public void Reflect(float remainingDistance, float damage, ParticleSystem ImpactFX, Light ImpactLight, RaycastHit Hit, Vector3 LaserDir, LineRenderer OriginalLaser)
    {
        laserRange = remainingDistance;
        laserDamage = damage;
        impactFX = ImpactFX;
        impactLight = ImpactLight;

        isReflecting = true;
    }

    public void StopReflection(LineRenderer laserToStop)
    {
        laserRange = 0;
        laserDamage = 0;
        impactFX = null;
        impactLight = null;

        StopReflections();

        isReflecting = false;
    }

    public bool AlreadyReflecting()
    {
        if (isReflecting)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
