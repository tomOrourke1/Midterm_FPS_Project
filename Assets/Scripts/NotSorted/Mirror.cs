using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour, IReflector
{
    [SerializeField] LineRenderer baseLaser;
    ParticleSystem impactFX;
    Light impactLight;

    Dictionary<LineRenderer, MirrorElement> mirrorElements = new Dictionary<LineRenderer, MirrorElement>();

    bool isReflecting = false;

    LineRenderer originalLaser;


    void startReflection()
    {
        if (mirrorElements.Count > 0)
        {
            isReflecting = true;
        }
        else
        {
            isReflecting = false;
        }

        if (isReflecting)
        {
            //CastLaser();
            HitHandlers(CastLaser(mirrorElements[originalLaser]));
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
            if (mirrorElements.Count == mirrorElements.Count)
            {
                mirrorElements[originalLaser].GetStoredReflector().GetComponent<IReflector>().StopReflection(originalLaser);
            }
        }
    }

    RaycastHit CastLaser(MirrorElement mirrorElement)
    {
        return gameObject.GetComponent<LaserCast>().RecieveLaser(mirrorElements[originalLaser].GetCastedLaser(), mirrorElements[originalLaser].GetLaserInitialPosition(), mirrorElements[originalLaser].GetReflectedDir(), mirrorElements[originalLaser].GetDamage(), mirrorElements[originalLaser].GetRange(), impactFX, impactLight);
    }


    void HandleReflect(RaycastHit hit)
    {
        IReflector reflector = hit.collider.GetComponent<IReflector>();
        if (CompareWithStoredReflector(hit))
        {
            // Get Remaining distance
            float reflectDist = mirrorElements[originalLaser].GetRange() - hit.distance;

            // call reflect
            reflector.Reflect(reflectDist, mirrorElements[originalLaser].GetDamage(), impactFX, impactLight, hit, hit.point - transform.position, mirrorElements[originalLaser].GetCastedLaser());
        }
        else if (reflector != null && !reflector.AlreadyReflecting() && !CompareWithStoredReflector(hit))
        {
            if (mirrorElements[originalLaser].GetStoredReflector() != null)
            {
                // Stop the old reflector
                mirrorElements[originalLaser].GetStoredReflector().GetComponent<IReflector>().StopReflection(originalLaser);
            }

            // store the collider
            mirrorElements[originalLaser].SetStoredReflector(hit.collider.gameObject);

            // Get Remaining distance
            float reflectDist = mirrorElements[originalLaser].GetRange() - hit.distance;

            // call reflect
            reflector.Reflect(reflectDist, mirrorElements[originalLaser].GetDamage(), impactFX, impactLight, hit, hit.point - transform.position, mirrorElements[originalLaser].GetCastedLaser());
        }
        else if (reflector == null && mirrorElements[originalLaser].GetStoredReflector() != null)
        {
            StopReflections();
        }
    }

    void HandleDamage(RaycastHit hit)
    {
        IDamagable damageable = hit.collider.GetComponent<IDamagable>();

        if (damageable != null)
        {
            damageable.TakeDamage(mirrorElements[originalLaser].GetDamage() * Time.deltaTime);
        }
    }



    Vector3 FindNewDirection(Vector3 laserNormal, Vector3 laserDir)
    {
        Vector3 Cross = Vector3.Cross(laserNormal, -laserDir);

        float Angle = Vector3.SignedAngle(-laserDir, laserNormal, Cross);// (laserNormal, laserDir);

        Quaternion Q = Quaternion.AngleAxis(Angle, Cross);

        Vector3 newDir = Q * laserNormal;

        return newDir;
    }

    public void Reflect(float remainingDistance, float damage, ParticleSystem ImpactFX, Light ImpactLight, RaycastHit Hit, Vector3 LaserDir, LineRenderer OriginalLaser)
    {
        originalLaser = OriginalLaser;

        if (!mirrorElements.ContainsKey(originalLaser))
        {
            MirrorElement newElement = new MirrorElement(damage, remainingDistance, FindNewDirection(Hit.normal, LaserDir), Hit.point, Instantiate(baseLaser, gameObject.transform), OriginalLaser);

            impactFX = ImpactFX;
            impactLight = ImpactLight;

            mirrorElements.Add(originalLaser, newElement);
        }
        else
        {
            mirrorElements[originalLaser]?.updateValues(damage, remainingDistance, FindNewDirection(Hit.normal, LaserDir), Hit.point, mirrorElements[originalLaser].GetCastedLaser(), OriginalLaser);
        }

        startReflection();
    }

    // This Function should be called by the object that is causing the reflection whenever it is no longer causing a reflection
    public void StopReflection(LineRenderer laserToStop)
    {
        if (mirrorElements.ContainsKey(laserToStop))
        {

            // If Mirror's laser is hitting something reflectable, tell it to stop
            StopReflections();

            // Remove the line renderer object
            Destroy(mirrorElements[laserToStop].GetCastedLaser().gameObject);

            // Remove this index from the dictionary
            mirrorElements.Remove(laserToStop);
        }
    }

    // This function stops reflectors hit by the mirror's lasers from reflecting
    void StopReflections()
    {
        mirrorElements[originalLaser]?.StopActiveReflection();
    }

    bool CompareWithStoredReflector(RaycastHit hit)
    {
        if (mirrorElements[originalLaser].GetStoredReflector() != null && hit.collider.gameObject == mirrorElements[originalLaser].GetStoredReflector())
        {
            return true;
        }

        return false;
    }

    public bool AlreadyReflecting()
    {
        if (mirrorElements?.Count > 8)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
