using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField] float bouncePower = 10f;


    private void OnCollisionEnter(Collision collision)
    {
        var vel = collision.collider.GetComponent<IApplyVelocity>();
        Debug.Log("Collision: " + collision.gameObject.name);

        if (vel != null)
        {
            Debug.Log("apply Velocity: " + collision.gameObject.name);
            var point = collision.contacts[0].point;
            var dir = point - transform.position;
            dir.Normalize();
            dir *= bouncePower;

            vel.ApplyVelocity(dir);
        }
    }


}
