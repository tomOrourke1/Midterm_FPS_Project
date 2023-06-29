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
            RayCast();
        }
        else
        {
            laser.enabled = false;
        }
    }

    void RayCast()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, laserRange))
        {
            UpdateLaserCast(hit.point);

            IDamagable damageable = hit.collider.GetComponent<IDamagable>();

            if (damageable != null)
            {
                damageable.TakeDamage(laserDamage * Time.deltaTime);
            }

            IReflector reflector = hit.collider.GetComponent<IReflector>();

            //Debug.Log("Collider: " + hit.collider + "\nReflecor: " + StoredReflector + "\nC = R: " + (hit.collider == StoredReflector));

            if (hit.collider.gameObject == StoredReflector)
            {
                //Debug.Log("Box");
                // Get Remaining distance
                float reflectDist = laserRange - hit.distance;

                // call reflect
                reflector.Reflect(reflectDist, laserDamage, impactFX, impactLight, hit, (hit.point - laser.GetPosition(0)).normalized);
            }
            else if (reflector != null && !reflector.AlreadyReflecting())
            {

                // Stop the old reflector
                StoredReflector?.GetComponent<IReflector>().StopReflection();

                // store the collider
                StoredReflector = hit.collider.gameObject;

                Debug.Log(StoredReflector);
                // Get Remaining distance
                float reflectDist = laserRange - hit.distance;

                // call reflect
                reflector.Reflect(reflectDist, laserDamage, impactFX, impactLight, hit, (hit.point - laser.GetPosition(0)).normalized);

            }
            else if (reflector == null)
            {
                StopReflections();
            }
        }
        else
        {
            DefaultLaserCast();
        }
    }

    void DefaultLaserCast()
    {
        // Stop reflections because there are no colliders
        StopReflections();

        Vector3 endPoint;
        endPoint = transform.position;

        Vector3 maxDistPoint = transform.forward * laserRange;
        endPoint += maxDistPoint;

        laser.SetPosition(0, transform.position);
        laser.SetPosition(1, endPoint);

        // FX Playing
        Vector3 dir = transform.position - endPoint;

        if (impactFX != null)
        {
            impactFX.Play();
            impactFX.transform.position = endPoint;
            impactFX.transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    void UpdateLaserCast(Vector3 hitPoint = new Vector3())
    {
        laser.SetPosition(0, transform.position);
        laser.SetPosition(1, hitPoint);

        Vector3 dir = transform.position - hitPoint;

        if (impactFX != null)
        {
            impactFX.Play();
            impactFX.transform.position = hitPoint;
            impactFX.transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    public void Reflect(float remainingDistance, float damage, ParticleSystem ImpactFX, Light ImpactLight, RaycastHit Hit, Vector3 LaserDir)
    {
        laserRange = remainingDistance;
        laserDamage = damage;
        impactFX = ImpactFX;
        impactLight = ImpactLight;

        isReflecting = true;
    }

    public void StopReflection()
    {
        laserRange = 0;
        laserDamage = 0;
        impactFX = null;
        impactLight = null;

        StopReflections();

        isReflecting = false;
    }

    void StopReflections()
    {
        StoredReflector?.GetComponent<IReflector>().StopReflection();
        StoredReflector = null;
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
