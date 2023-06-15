using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIngerGunParticlePlayer : MonoBehaviour
{

    [SerializeField] ParticleSystem fireParticles;
    [SerializeField] ParticleSystem secondaryFireParticles;
    [SerializeField] Transform spawnPos;
    


    public void FireParticles()
    {
        Instantiate(fireParticles, spawnPos.position, spawnPos.rotation, spawnPos);
        Instantiate(secondaryFireParticles, spawnPos.position, spawnPos.rotation, spawnPos);
    }




}
