using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReflector
{
    public bool AlreadyReflecting();

    public void Reflect(float remainingDistance, float damage, ParticleSystem ImpactFX, Light ImpactLight, RaycastHit hit, Vector3 LaserDir, LineRenderer originalLaser);
    public void StopReflection(LineRenderer laserToStop);
}
