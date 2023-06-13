using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UIElements;

public class Laser : MonoBehaviour
{
    [Header("----- Laser Stats -----")]
    [SerializeField] float maxDistance;
    [SerializeField] float Damage;
    [SerializeField] float HitRate;
    [SerializeField] bool LaserOn;

    [Header("----- Timed Lasers -----")]
    [SerializeField] bool TimedLasers;
    [SerializeField] float LaserUpTime;
    [SerializeField] float LaserDownTime;



    LineRenderer laser;
    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        laser = GetComponent<LineRenderer>();

        ResetLaser();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (LaserOn)
        {
            laser.enabled = true;
            RayCast();

            HandleLaser(LaserUpTime);
        } 
        else
        {
            laser.enabled = false;
            HandleLaser(LaserDownTime);
        }

    }


    void HandleLaser(float time)
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
            UpdateLaser(hit.point);

            IDamagable damageable = hit.collider.GetComponent<IDamagable>();

            if (damageable != null)
            {
                damageable.TakeDamage(Damage * Time.deltaTime);
            }
        }
        else
        {
            ResetLaser();
        }
    }

    void ResetLaser()
    {
        Vector3 endPoint;
        endPoint = transform.position;

        Vector3 maxDistPoint = new Vector3(0, maxDistance, 0);
        endPoint += maxDistPoint;

        laser.SetPosition(0, transform.position);
        laser.SetPosition(1, endPoint);
    }

    void UpdateLaser(Vector3 hitPoint = new Vector3())
    {
        laser.SetPosition(0, transform.position);
        laser.SetPosition(1, hitPoint);
    }

    public bool GetLaserEnabled() { return LaserOn; }

    public void SetLaserEnabled(bool enabled) {  LaserOn = enabled; }
}
