using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITelekinesis
{
    void TakeVelocity(Vector3 velocity);
    Rigidbody GetRigidbody();
    Vector3 GetPosition();
    Vector3 GetVelocity();

}
