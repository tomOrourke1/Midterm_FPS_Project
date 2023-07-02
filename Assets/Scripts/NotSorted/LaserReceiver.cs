using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserReceiver : MonoBehaviour, IReflector
{
    [SerializeField] UnityEvent On;
    [SerializeField] UnityEvent Off;

    bool isActive = false;
    // Start is called before the first frame update


    public bool AlreadyReflecting()
    {
        return isActive;
    }

    public void Reflect(float remainingDistance, float damage, ParticleSystem ImpactFX, Light ImpactLight, RaycastHit hit, Vector3 LaserDir, LineRenderer originalLaser)
    {
        isActive = true;
        On?.Invoke();
    }

    public void StopReflection(LineRenderer laserToStop)
    {
        isActive = false;
        Off?.Invoke();
    }


}
