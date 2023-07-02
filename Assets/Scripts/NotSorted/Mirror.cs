using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour, IReflector
{
    [SerializeField] LineRenderer baseLaser;

    bool isReflecting = false;

    List<float> laserDamage = new List<float>();
    List<float> laserRange = new List<float>();

    int laserID = 0;

    List<Vector3> ReflectedDir = new List<Vector3>();

    List<Vector3> laserInitialPosition = new List<Vector3>();
    List<Vector3> laserNormal = new List<Vector3>();
    List<Vector3> laserDir = new List<Vector3>();

    List<LineRenderer> lasers = new List<LineRenderer>();
    List<LineRenderer> originalLasers = new List<LineRenderer>();

    ParticleSystem impactFX;
    Light impactLight;

    int numRemoved = 0;

    List<GameObject> StoredReflector = new List<GameObject>();

    // This will store the index that each Reflector ties to
    List<int> indexCounter = new List<int>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (lasers.Count > 0)
        {
            isReflecting = true;
        }
        else
        {
            isReflecting = false;
        }

        if (isReflecting)
        {
            numRemoved = 0;

            for (laserID = 0; laserID < lasers.Count; laserID++)
            {
                lasers[laserID].enabled = true;

                //CastLaser();
                HitHandlers(CastLaser());
            }
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
            if (StoredReflector.Count == lasers.Count)
            {
                StopReflections(indexCounter.IndexOf(laserID));
            }
        }
    }

    RaycastHit CastLaser()
    {
        return gameObject.GetComponent<LaserCast>().RecieveLaser(lasers[laserID], laserInitialPosition[laserID], ReflectedDir[laserID], laserDamage[laserID], laserRange[laserID], impactFX, impactLight);
    }


    void HandleReflect(RaycastHit hit)
    {
        IReflector reflector = hit.collider.GetComponent<IReflector>();
        if (CompareWithStoredReflector(hit))
        {
            // Get Remaining distance
            float reflectDist = laserRange[laserID] - hit.distance;

            // call reflect
            reflector.Reflect(reflectDist, laserDamage[laserID], impactFX, impactLight, hit, hit.point - transform.position, lasers[laserID]);
        }
        else if (reflector != null && !reflector.AlreadyReflecting() && !CompareWithStoredReflector(hit))
        {
            if (indexCounter.Count > 0 && indexCounter.Contains(laserID))
            {
                int index = indexCounter.IndexOf(laserID);

                // Stop the old reflector
                StopReflections(indexCounter.IndexOf(laserID));
                //StoredReflector[index]?.GetComponent<IReflector>().StopReflection(lasers[laserID]);
            }

            // store the collider
            StoredReflector.Add(hit.collider.gameObject);

            indexCounter.Add(laserID);

            // Get Remaining distance
            float reflectDist = laserRange[laserID] - hit.distance;

            // call reflect
            reflector.Reflect(reflectDist, laserDamage[laserID], impactFX, impactLight, hit, hit.point - transform.position, lasers[laserID]);
        }
        else if (reflector == null && indexCounter.Contains(laserID) && StoredReflector[indexCounter.IndexOf(laserID)] != null)
        {
            StopReflections(indexCounter.IndexOf(laserID));
        }
    }

    void HandleDamage(RaycastHit hit)
    {
        IDamagable damageable = hit.collider.GetComponent<IDamagable>();

        if (damageable != null)
        {
            damageable.TakeDamage(laserDamage[laserID] * Time.deltaTime);
        }
    }



    Vector3 FindNewDirection(int id)
    {
        Vector3 Cross = Vector3.Cross(laserNormal[id], -laserDir[id]);

        float Angle = Vector3.SignedAngle(-laserDir[id], laserNormal[id], Cross);// (laserNormal, laserDir);

        Quaternion Q = Quaternion.AngleAxis(Angle, Cross);

        Vector3 newDir = Q * laserNormal[id];

        return newDir;
    }

    public void Reflect(float remainingDistance, float damage, ParticleSystem ImpactFX, Light ImpactLight, RaycastHit Hit, Vector3 LaserDir, LineRenderer originalLaser)
    {
        //Debug.Log(Hit.point);

        if (!originalLasers.Contains(originalLaser))
        {
            laserRange.Add(remainingDistance);
            laserDamage.Add(damage);

            impactFX = ImpactFX;
            impactLight = ImpactLight;

            laserInitialPosition.Add(Hit.point);
            laserNormal.Add(Hit.normal);
            laserDir.Add(LaserDir);


            originalLasers.Add(originalLaser);

            lasers.Add(Instantiate(baseLaser, gameObject.transform));

            ReflectedDir.Add(FindNewDirection(lasers.Count - 1));
        }
        else
        {
            int id = originalLasers.IndexOf(originalLaser);

            //StoredReflector[id] = null;

            laserRange[id] = remainingDistance;
            laserDamage[id] = damage;

            laserInitialPosition[id] = Hit.point;
            laserNormal[id] = Hit.normal;
            laserDir[id] = LaserDir;

            ReflectedDir[id] = FindNewDirection(id);
        }


        //Debug.Log("Laser Pos: " + laserInitialPosition);

    }

    public void StopReflection(LineRenderer laserToStop)
    {
        int id = originalLasers.IndexOf(laserToStop) - numRemoved;

        Debug.Log("Index: " + originalLasers.IndexOf(laserToStop) + "\nRemoved: " + numRemoved + "\nID: " + id);

        //laserID++;

        laserRange.RemoveAt(id);
        laserDamage.RemoveAt(id);

        ReflectedDir.RemoveAt(id);
        laserInitialPosition.RemoveAt(id);
        laserNormal.RemoveAt(id);
        laserDir.RemoveAt(id);

        if (indexCounter.Contains(id))
        {
            StopReflections(indexCounter.IndexOf(id));
        }

        Destroy(lasers[id].gameObject);

        lasers.RemoveAt(id);
        originalLasers.RemoveAt(id);

        numRemoved += 1;
    }

    void StopReflections(int id)
    {
        if (StoredReflector.Count > 0)
        {
            StoredReflector[id]?.GetComponent<IReflector>()?.StopReflection(lasers[id]);
            StoredReflector?.RemoveAt(id);
            indexCounter.RemoveAt(id);
        }
    }

    bool CompareWithStoredReflector(RaycastHit hit)
    {

        if (indexCounter.Contains(laserID) && hit.collider.gameObject == StoredReflector[indexCounter.IndexOf(laserID)])
        {
            return true;
        }

        return false;
    }

    public bool AlreadyReflecting()
    {
        if (lasers.Count > 8)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
