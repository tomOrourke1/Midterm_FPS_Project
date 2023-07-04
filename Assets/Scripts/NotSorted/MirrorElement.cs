using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorElement
{
    // Laser stats
    float damage;
    float range;

    // Laser position
    Vector3 reflectedDir;
    Vector3 laserInitialPosition;

    // Renderers
    LineRenderer castedLaser;
    LineRenderer originalLaser;

    GameObject StoredReflector;

    public MirrorElement(float Damage, float Range, Vector3 ReflectedDir, Vector3 LaserInitialPosition, LineRenderer CastedLaser, LineRenderer OriginalLaser)
    {
        damage = Damage; 
        range = Range; 

        reflectedDir = ReflectedDir; 
        laserInitialPosition = LaserInitialPosition; 

        castedLaser = CastedLaser;
        originalLaser = OriginalLaser;
    }

    public void updateValues(float Damage, float Range, Vector3 ReflectedDir, Vector3 LaserInitialPosition, LineRenderer CastedLaser, LineRenderer OriginalLaser) 
    {
        damage = Damage;
        range = Range;

        reflectedDir = ReflectedDir;
        laserInitialPosition = LaserInitialPosition;

        castedLaser = CastedLaser;
        originalLaser = OriginalLaser;
    }

    public void StopActiveReflection()
    {
        StoredReflector?.GetComponent<IReflector>()?.StopReflection(castedLaser);
    }

    public float GetDamage() { return damage; }
    public float GetRange() { return range; }

    public Vector3 GetReflectedDir() { return reflectedDir; }
    public Vector3 GetLaserInitialPosition() {  return laserInitialPosition; }

    public LineRenderer GetCastedLaser() {  return castedLaser; }
    public LineRenderer GetOriginalLaser() { return originalLaser; }

    public void SetStoredReflector(GameObject stored) { StoredReflector = stored; }
    public GameObject GetStoredReflector() { return StoredReflector; }
}
