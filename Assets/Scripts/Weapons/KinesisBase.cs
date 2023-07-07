using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KinesisBase : MonoBehaviour
{
    public float fireRate;
    public float focusCost;
    public abstract void Fire();
    public virtual void EnableOpenRadial()
    {
        InputManager.Instance.Action.OpenRadialWheel.Enable();
    }

    public virtual void DisableOpenRadial()
    {
        InputManager.Instance.Action.OpenRadialWheel.Disable();
    }

    public bool isCasting;

    public abstract void StopFire();
}
