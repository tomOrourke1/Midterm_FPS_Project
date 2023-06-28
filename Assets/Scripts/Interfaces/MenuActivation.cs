using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class MenuActivation : MonoBehaviour
{
    [Header("Does do activations")]
    [SerializeField] public bool doesActivate = true;
    [SerializeField] public bool doesDeactivate = true;

    /// <summary>
    /// Needs to be included ~ Tom
    /// </summary>
    public abstract void Activate();
    public abstract void Deactivate();
}
