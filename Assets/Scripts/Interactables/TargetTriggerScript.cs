using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetTriggerScript : MonoBehaviour, IEnvironment
{

    [SerializeField] UnityEvent targetEvent;

    [SerializeField] Renderer targetRenderer;
    [SerializeField] Material targeCheckedMaterial;

    Material initMaterials;

    private void OnTriggerEnter(Collider other)
    {
        targetEvent?.Invoke();
        targetRenderer.material = targeCheckedMaterial;
    }

    public void StartObject()
    {
        initMaterials = targetRenderer.material;
    }

    public void StopObject()
    {
    }

    public void ResetObject()
    {
        targetRenderer.material = initMaterials;
    }
}
