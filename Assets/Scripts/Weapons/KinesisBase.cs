using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KinesisBase : MonoBehaviour
{
    public float fireRate;
    public float focusCost;
    public abstract void Fire();

    public bool isCasting;
}
