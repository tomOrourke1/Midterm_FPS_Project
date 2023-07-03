using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField] float bouncePower = 10f;



    private void OnTriggerEnter(Collider other)
    {
        var vel = other.GetComponent<IApplyVelocity>(); 
        if(vel != null)
        {
            var point = other.transform.position;
            var dir = point - transform.position;
            dir.Normalize();
            dir *= bouncePower;
            vel.ApplyVelocity(dir);
        }
    }
}
