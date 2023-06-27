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
 //       StopObject();
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
                RayCast();

                HandleLaserTimer(LaserUpTime);
            } 
            else
            {
                laser.enabled = false;
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

    void RayCast()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.up, out hit, maxDistance))
        {
            UpdateLaserCast(hit.point);

            IDamagable damageable = hit.collider.GetComponent<IDamagable>();

            if (damageable != null)
            {
                damageable.TakeDamage(Damage * Time.deltaTime);
            }
        }
        else
        {
            DefaultLaserCast();
        }
    }

    void DefaultLaserCast()
    {
        Vector3 endPoint;
        endPoint = transform.position;

        Vector3 maxDistPoint = transform.up * maxDistance;
        endPoint += maxDistPoint;

        laser.SetPosition(0, transform.position);
        laser.SetPosition(1, endPoint);

        // FX Playing
        Vector3 dir = transform.position - endPoint;
        impactFX.Play();
        impactFX.transform.position = endPoint;
        impactFX.transform.rotation = Quaternion.LookRotation(dir);
    }

    void UpdateLaserCast(Vector3 hitPoint = new Vector3())
    {
        laser.SetPosition(0, transform.position);
        laser.SetPosition(1, hitPoint);

        Vector3 dir = transform.position - hitPoint;
        impactFX.Play();
        impactFX.transform.position = hitPoint;
        impactFX.transform.rotation = Quaternion.LookRotation(dir);
    }

    public bool GetLaserEnabled() { return LaserOn; }

    public void SetLaserEnabled(bool enabled) {  LaserOn = enabled; }

    public void StartObject()
    {
        this.enabled = true;
        initialDelay = initDelayAmount;
        LaserOn = initialLaserOn;
        laser = GetComponent<LineRenderer>();
        laser.enabled = false;
        started = false;

        DefaultLaserCast();
    }

    public void StopObject()
    {
        
        this.enabled = false;
    }
}
