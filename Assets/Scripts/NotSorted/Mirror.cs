using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour, IReflector
{
    bool isReflecting = false;

    float laserDamage = 0;
    float laserRange = 0;

    Vector3 ReflectedDir;

    Vector3 laserInitialPosition;
    Vector3 laserNormal;
    Vector3 laserDir;

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
            //Debug.Log("Update");
            laser.enabled = true;
            ReflectedDir = FindNewDirection();
            //Debug.Log("Dir: " + ReflectedDir);
            RayCast();
        }
        else
        {
            laser.enabled = false;
            //StopReflections();
        }
    }



    void RayCast()
    {
        RaycastHit hit;

        if (Physics.Raycast(laserInitialPosition, ReflectedDir, out hit, laserRange))
        {

            //Debug.Log("Raycast Hit");
            UpdateLaserCast(hit.point);

            IDamagable damageable = hit.collider.GetComponent<IDamagable>();

            if (damageable != null)
            {
                damageable.TakeDamage(laserDamage * Time.deltaTime);
            }

            IReflector reflector = hit.collider.GetComponent<IReflector>();

            if (hit.collider.gameObject == StoredReflector)
            {
                //Debug.Log("Laser");
                // Get Remaining distance
                float reflectDist = laserRange - hit.distance;

                // call reflect
                reflector.Reflect(reflectDist, laserDamage, impactFX, impactLight, hit, hit.point - transform.position);
            }
            else if (reflector != null && !reflector.AlreadyReflecting())
            {
                //Debug.Log("Reflect");

                // Stop the old reflector
                StoredReflector?.GetComponent<IReflector>().StopReflection();

                // store the collider
                StoredReflector = hit.collider.gameObject;

                // Get Remaining distance
                float reflectDist = laserRange - hit.distance;

                // call reflect
                reflector.Reflect(reflectDist, laserDamage, impactFX, impactLight, hit, hit.point - transform.position);

            }
            else if (reflector == null && StoredReflector != null)
            {
                //Debug.Log("Stop Reflections");
                StopReflections();
            }

        }
        else
        {

            //Debug.Log("Raycast Miss");
            DefaultLaserCast();
        }
    }

    void DefaultLaserCast()
    {


        // Stop reflections because there are no colliders
        if (StoredReflector != null)
        {
            StopReflections();
        }

        Vector3 endPoint;
        endPoint = laserInitialPosition;

        Vector3 maxDistPoint = ReflectedDir * laserRange;
        endPoint += maxDistPoint;

        laser.SetPosition(0, laserInitialPosition);
        laser.SetPosition(1, endPoint);

        // FX Playing
        Vector3 dir = laserInitialPosition - endPoint;

        if (impactFX != null)
        {
            impactFX.Play();
            impactFX.transform.position = endPoint;
            impactFX.transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    void UpdateLaserCast(Vector3 hitPoint = new Vector3())
    {

        //Debug.Log("Hit Point: " + hitPoint + "\nStart Pos: " + laserInitialPosition);

        laser.SetPosition(0, laserInitialPosition);
        laser.SetPosition(1, hitPoint);

        Vector3 dir = laserInitialPosition - hitPoint;

        if (impactFX != null)
        {
            impactFX.Play();
            impactFX.transform.position = hitPoint;
            impactFX.transform.rotation = Quaternion.LookRotation(dir);
        }
    }


    Vector3 FindNewDirection()
    {
        Vector3 Cross = Vector3.Cross(laserNormal, -laserDir);

        float Angle = Vector3.SignedAngle(-laserDir, laserNormal, Cross);// (laserNormal, laserDir);

        Quaternion Q = Quaternion.AngleAxis(Angle, Cross);

        Vector3 newDir = Q * laserNormal;

        return newDir;
    }

    public void Reflect(float remainingDistance, float damage, ParticleSystem ImpactFX, Light ImpactLight, RaycastHit Hit, Vector3 LaserDir)
    {
        laserRange = remainingDistance;
        laserDamage = damage;
        impactFX = ImpactFX;
        impactLight = ImpactLight;

        laserInitialPosition = Hit.point;
        laserNormal = Hit.normal;
        laserDir = LaserDir;

        //Debug.Log("Laser Pos: " + laserInitialPosition);

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
