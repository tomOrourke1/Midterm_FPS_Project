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
    //[SerializeField] float HitRate;
    [SerializeField] bool LaserOn;
    [SerializeField] float initialDelay;


    [Header("----- Timed Lasers -----")]
    [SerializeField] bool TimedLasers;
    [SerializeField] float LaserUpTime;
    [SerializeField] float LaserDownTime;

    LineRenderer laser;
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
            //laser.enabled = false;
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
    }

    void UpdateLaserCast(Vector3 hitPoint = new Vector3())
    {
        laser.SetPosition(0, transform.position);
        laser.SetPosition(1, hitPoint);
    }

    public bool GetLaserEnabled() { return LaserOn; }

    public void SetLaserEnabled(bool enabled) {  LaserOn = enabled; }

    // This is a function tied to IEnvironment meant to be used to reset a room
    public void ResetObject()
    {
        // gameObject.SetActive(true);
        this.enabled = true;
        initialDelay = initDelayAmount;
        LaserOn = initialLaserOn;
        laser = GetComponent<LineRenderer>();
        laser.enabled = false;
        started = false;

        DefaultLaserCast();
    }

    public void StartObject()
    {
        //Debug.Log("Laser Start");
       // gameObject.SetActive(true);
        this.enabled = true;
        laser = GetComponent<LineRenderer>();
        laser.enabled = false;
        started = false;

        DefaultLaserCast();
        
        //if (gameObject != null)
        //{
        //    Debug.Log("Laser Enabled: " + gameObject.activeSelf);
        //}
    }

    public void StopObject()
    {
        //gameObject.SetActive(false);
        this.enabled = false;
    }
}
