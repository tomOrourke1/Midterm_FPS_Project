using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisruptorField : MonoBehaviour
{
    [Header("Material Renderers")]
    [SerializeField] Disrupter pickupScript;
    [SerializeField] MeshRenderer core;
    [SerializeField] MeshRenderer inner;
    [SerializeField] MeshRenderer outer;

    [Header("Aero Colors")]
    [SerializeField] OrbMaterials aerokinesisMaterials;

    [Header("Electro Colors")]
    [SerializeField] OrbMaterials electrokinesisMaterials;

    [Header("Tele Colors")]
    [SerializeField] OrbMaterials telekinesisMaterials;

    [Header("Pyro Colors")]
    [SerializeField] OrbMaterials pyrokinesisMaterials;

    [Header("Cryo Colors")]
    [SerializeField] OrbMaterials cryokinesisMaterials;


    [System.Obsolete]
    private void Start()
    {
        SetMatColor(pickupScript.select);
    }

    [System.Obsolete]
    private void SetMatColor(KinesisSelect picked)
    {
        switch (picked)
        {
            case KinesisSelect.aerokinesis:
                core.material = aerokinesisMaterials.GetCore();
                inner.material = aerokinesisMaterials.GetInner();
                outer.material = aerokinesisMaterials.GetOuter();
                break;

            case KinesisSelect.electrokinesis:
                core.material = electrokinesisMaterials.GetCore();
                inner.material = electrokinesisMaterials.GetInner();
                outer.material = electrokinesisMaterials.GetOuter();
                break;

            case KinesisSelect.telekinesis:
                core.material = telekinesisMaterials.GetCore();
                inner.material = telekinesisMaterials.GetInner();
                outer.material = telekinesisMaterials.GetOuter();
                break;

            case KinesisSelect.pyrokinesis:
                core.material = pyrokinesisMaterials.GetCore();
                inner.material = pyrokinesisMaterials.GetInner();
                outer.material = pyrokinesisMaterials.GetOuter();
                break;

            case KinesisSelect.cryokinesis:
                core.material = cryokinesisMaterials.GetCore();
                inner.material = cryokinesisMaterials.GetInner();
                outer.material = cryokinesisMaterials.GetOuter();
                break;
        }
    }
}
