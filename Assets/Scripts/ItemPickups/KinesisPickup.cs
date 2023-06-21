using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public enum KinesisSelect
{
    aerokinesis,
    electrokinesis,
    telekinesis,
    pyrokinesis,
    cryokinesis
}

public class KinesisPickup : MonoBehaviour
{
    [Header("Kinesis")]
    [SerializeField] private KinesisSelect pickupSelect;
    [SerializeField] bool active = true;

    [Header("Renderers")]
    [SerializeField] MeshRenderer core;
    [SerializeField] MeshRenderer inner;
    [SerializeField] MeshRenderer outer;
    [SerializeField] ParticleSystem rings;
    [SerializeField] ParticleSystem trails;
    [SerializeField] ParticleSystem beam;

    [Header("Bobbing Stats")]
    [SerializeField] float freq;
    [SerializeField] float amp;

    [Header("Aero Colors")]
    [SerializeField] Material aeroCore;
    [SerializeField] Material aeroInner;
    [SerializeField] Material aeroOuter;

    [Header("Electro Colors")]
    [SerializeField] Material electroCore;
    [SerializeField] Material electroInner;
    [SerializeField] Material electroOuter;

    [Header("Tele Colors")]
    [SerializeField] Material teleCore;
    [SerializeField] Material teleInner;
    [SerializeField] Material teleOuter;

    [Header("Pyro Colors")]
    [SerializeField] Material pyroCore;
    [SerializeField] Material pyroInner;
    [SerializeField] Material pyroOuter;

    [Header("Cryo Colors")]
    [SerializeField] Material cryoCore;
    [SerializeField] Material cryoInner;
    [SerializeField] Material cryoOuter;

    Vector3 startingPos;

    [System.Obsolete]
    private void Start()
    {
        startingPos = transform.position;
        SetMatColor(pickupSelect);
    }

    private void OnBecameVisible()
    {
        gameObject.SetActive(true);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, startingPos.y + Mathf.Sin(Time.time * freq) * amp, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnableKinesis(pickupSelect, active);

            Destroy(gameObject);
        }
    }

    private void EnableKinesis(KinesisSelect selected, bool b)
    {
        switch (selected)
        {
            case KinesisSelect.aerokinesis:
                GameManager.instance.GetEnabledList().AeroSetActive(b);
                break;

            case KinesisSelect.electrokinesis:
                GameManager.instance.GetEnabledList().ElectroSetActive(b);
                break;

            case KinesisSelect.telekinesis:
                GameManager.instance.GetEnabledList().TeleSetActive(b);
                break;

            case KinesisSelect.pyrokinesis:
                GameManager.instance.GetEnabledList().PyroSetActive(b);
                break;

            case KinesisSelect.cryokinesis:
                GameManager.instance.GetEnabledList().CryoSetActive(b);
                break;
        }
    }

    [System.Obsolete]
    private void SetMatColor(KinesisSelect picked)
    {
        switch (picked)
        {
            case KinesisSelect.aerokinesis:
                core.material = aeroCore;
                inner.material = aeroInner;
                outer.material = aeroOuter;

                rings.startColor = aeroCore.color;
                trails.startColor = aeroCore.color;
                beam.startColor = aeroCore.color;
                break;

            case KinesisSelect.electrokinesis:
                core.material = electroCore;
                inner.material = electroInner;
                outer.material = electroOuter;

                rings.startColor = electroCore.color;
                trails.startColor = electroCore.color;
                beam.startColor = electroCore.color;
                break;

            case KinesisSelect.telekinesis:
                core.material = teleCore;
                inner.material = teleInner;
                outer.material = teleOuter;

                rings.startColor = teleCore.color;
                trails.startColor = teleCore.color;
                beam.startColor = teleCore.color;
                break;

            case KinesisSelect.pyrokinesis:
                core.material = pyroCore;
                inner.material = pyroInner;
                outer.material = pyroOuter;

                rings.startColor = pyroCore.color;
                trails.startColor = pyroCore.color;
                beam.startColor = pyroCore.color;
                break;

            case KinesisSelect.cryokinesis:
                core.material = cryoCore;
                inner.material = cryoInner;
                outer.material = cryoOuter;

                rings.startColor = cryoCore.color;
                trails.startColor = cryoCore.color;
                beam.startColor = cryoCore.color;
                break;
        }
    }
}
