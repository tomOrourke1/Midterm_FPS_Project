using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetTriggerScript : MonoBehaviour, IEnvironment
{

    [SerializeField] UnityEvent targetEvent;

    [SerializeField] Renderer targetRenderer;
    [SerializeField] Material targeCheckedMaterial;

    [SerializeField] Material initMaterials;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            targetEvent?.Invoke();
            targetRenderer.material = targeCheckedMaterial;

        }
    }

    public void StartObject()
    {

    }

    public void StopObject()
    {
        targetRenderer.material = initMaterials;
    }
}
