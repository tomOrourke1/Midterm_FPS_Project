using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UIElements;

public class Laser : MonoBehaviour, IEnvironment
{
    [Header("----- Laser Stats -----")]
    [SerializeField] float maxDistance;
    [SerializeField] float Damage;
    [SerializeField] bool LaserOn;
    [SerializeField] float initialDelay;

    [Header("----- Timed Lasers -----")]
    [SerializeField] bool TimedLasers;
    [SerializeField] float LaserUpTime;
    [SerializeField] float LaserDownTime;

    [Header("FX")]
    [SerializeField] LineRenderer laser;
    [SerializeField] ParticleSystem impactFX;
    [SerializeField] Light impactLight;

    // Reflection
    GameObject StoredReflector;


    float startTime;

    float initDelayAmount;

    // Added these to store initial laser stats
    bool initialLaserOn;

    bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        initDelayAmount = initialDelay;
        laser = GetComponent<LineRenderer>();
        initialLaserOn = LaserOn;
    }

    // Update is called once per frame
    void Update()
    {

        if (initialDelay <= 0)
        {
            if (!started)
            {
                started = true;
                startTime = Time.time;
            }

            if (LaserOn)
            {
                laser.enabled = true;
                HitHandlers(CastLaser());

                HandleLaserTimer(LaserUpTime);
            }
            else
            {
                laser.enabled = false;
                StopReflections();
                HandleLaserTimer(LaserDownTime);
            }
        }
        else
        {
            initialDelay -= Time.deltaTime;
        }
    }


    void HandleLaserTimer(float time)
    {
        if (TimedLasers)
        {
            if (Time.time - startTime >= time)
            {
                startTime = Time.time;
                LaserOn = !LaserOn;
            }
        }


        // if the time.time - start time >= currentTimeAgainst
        // set time. time
        // swap bool
    }

    private RaycastHit CastLaser()
    {
        return gameObject.GetComponent<LaserCast>().RecieveLaser(laser, transform.position, transform.up, Damage, maxDistance, impactFX, impactLight);
    }

    public bool GetLaserEnabled() { return LaserOn; }

    public void SetLaserEnabled(bool enabled) { LaserOn = enabled; }

    public void StartObject()
    {
        this.enabled = true;
        initialDelay = initDelayAmount;
        LaserOn = initialLaserOn;
        laser = GetComponent<LineRenderer>();
        laser.enabled = false;
        started = false;

        //DefaultLaserCast();
        CastLaser();
    }

    public void StopObject() { this.enabled = false; }

    bool CompareWithStoredReflector(RaycastHit hit)
    {
        if (StoredReflector != null && hit.collider.gameObject == StoredReflector.gameObject)
        {
            return true;
        }

        return false;
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

    void HandleReflect(RaycastHit hit)
    {
        IReflector reflector = hit.collider.GetComponent<IReflector>();

        //if (reflector == null && StoredReflector != null)
        //{
        //    // Laser hit nothing, Laser was Hitting something
        //    StopReflections();
        //}
        //else if (reflector == null && StoredReflector == null)
        //{
        //    // laser hit nothing and laser was hitting nothing
        //    StopReflections();
        //}
        //else if (reflector != null && StoredReflector != null)
        //{
        //    // Laser hit something and Laser was Hitting something

        //    // Stop the old reflector
        //    StoredReflector?.GetComponent<IReflector>().StopReflection(laser);
        //    // store the collider
        //    StoredReflector = hit.collider.gameObject;
        //    // Get Remaining distance
        //    float reflectDist = maxDistance - hit.distance;
        //    // call reflect
        //    reflector.Reflect(reflectDist, Damage, impactFX, impactLight, hit, hit.point - transform.position, laser);
        //}
        //else if (reflector != null && StoredReflector == null)
        //{
        //    // Laser hit something and laser wasn't hitting something

        //    // store the collider
        //    StoredReflector = hit.collider.gameObject;
        //    // Get Remaining distance
        //    float reflectDist = maxDistance - hit.distance;
        //    // call reflect
        //    reflector.Reflect(reflectDist, Damage, impactFX, impactLight, hit, hit.point - transform.position, laser);
        //}


        if (CompareWithStoredReflector(hit))
        {
            //Debug.Log("Laser");
            // Get Remaining distance
            float reflectDist = maxDistance - hit.distance;
            // call reflect
            reflector.Reflect(reflectDist, Damage, impactFX, impactLight, hit, hit.point - transform.position, laser);
        }
        else if (reflector != null && !reflector.AlreadyReflecting() && !CompareWithStoredReflector(hit))
        {
            // Stop the old reflector
            StoredReflector?.GetComponent<IReflector>().StopReflection(laser);
            // store the collider
            StoredReflector = hit.collider.gameObject;
            // Get Remaining distance
            float reflectDist = maxDistance - hit.distance;
            // call reflect
            reflector.Reflect(reflectDist, Damage, impactFX, impactLight, hit, hit.point - transform.position, laser);
        }
        else if (reflector == null && StoredReflector != null)
        {
            StopReflections();
        }
    }

    void HandleDamage(RaycastHit hit)
    {
        IDamagable damageable = hit.collider.GetComponent<IDamagable>();

        if (damageable != null)
        {
            damageable.TakeDamage(Damage * Time.deltaTime);
        }
    }

    void StopReflections()
    {
        StoredReflector?.GetComponent<IReflector>().StopReflection(laser);
        StoredReflector = null;
    }

}
