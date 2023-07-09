using System.Collections;
using UnityEngine;

[System.Serializable]
public enum KinesisSelect
{
    aerokinesis,
    electrokinesis,
    telekinesis,
    pyrokinesis,
    cryokinesis
}

[System.Serializable]
public struct OrbMaterials
{
    [Header("Material Properties")]
    [Tooltip("The material for the central model.")]
    [SerializeField] Material center;
    [Tooltip("The material for the outside model.")]
    [SerializeField] Material outside;
    [Tooltip("The material for the outermost model.")]
    [SerializeField] Material outermost;

    public Material GetCore()
    {
        return center;
    }

    public Material GetInner()
    {
        return outside;
    }

    public Material GetOuter()
    {
        return outermost;
    }
}

public class KinesisPickup : MonoBehaviour
{
    [Header("Kinesis")]
    public KinesisSelect pickupSelect;
    [SerializeField] bool active = true;
    [SerializeField] ShrinkAndDelete shrinkScript;
    [SerializeField] PickupSFX sfxScript;

    [Header("Renderers")]
    [SerializeField] MeshRenderer core;
    [SerializeField] MeshRenderer inner;
    [SerializeField] MeshRenderer outer;
    [SerializeField] ParticleSystem rings;
    [SerializeField] ParticleSystem trails;
    [SerializeField] ParticleSystem beam;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !DoesPlayerHaveKinesis(pickupSelect))
        {
            sfxScript.Play_OneShot();
            shrinkScript.Shrink(); 
            StartCoroutine(WaitToEnable());
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
                core.material = aerokinesisMaterials.GetCore();
                inner.material = aerokinesisMaterials.GetInner();
                outer.material = aerokinesisMaterials.GetOuter();

                ChangeParticleColors();
                break;

            case KinesisSelect.electrokinesis:
                core.material = electrokinesisMaterials.GetCore();
                inner.material = electrokinesisMaterials.GetInner();
                outer.material = electrokinesisMaterials.GetOuter();

                ChangeParticleColors();
                break;

            case KinesisSelect.telekinesis:
                core.material = telekinesisMaterials.GetCore();
                inner.material = telekinesisMaterials.GetInner();
                outer.material = telekinesisMaterials.GetOuter();

                ChangeParticleColors();
                break;

            case KinesisSelect.pyrokinesis:
                core.material = pyrokinesisMaterials.GetCore();
                inner.material = pyrokinesisMaterials.GetInner();
                outer.material = pyrokinesisMaterials.GetOuter();

                ChangeParticleColors();
                break;

            case KinesisSelect.cryokinesis:
                core.material = cryokinesisMaterials.GetCore();
                inner.material = cryokinesisMaterials.GetInner();
                outer.material = cryokinesisMaterials.GetOuter();

                ChangeParticleColors();
                break;
        }
    }

    [System.Obsolete]
    private void ChangeParticleColors()
    {
        rings.startColor = core.material.color;
        trails.startColor = core.material.color;
        beam.startColor = core.material.color;
    }

    public bool DoesPlayerHaveKinesis(KinesisSelect selected)
    {
        switch (selected)
        {
            case KinesisSelect.aerokinesis:
                return GameManager.instance.GetEnabledList().AeroEnabled();
         
            case KinesisSelect.electrokinesis:
                return GameManager.instance.GetEnabledList().ElectroEnabled();

            case KinesisSelect.telekinesis:
                return GameManager.instance.GetEnabledList().TeleEnabled();

            case KinesisSelect.pyrokinesis:
                return GameManager.instance.GetEnabledList().PyroEnabled();

            case KinesisSelect.cryokinesis:
                return GameManager.instance.GetEnabledList().CryoEnabled();
        }
        return false;
    }

    private IEnumerator WaitToEnable()
    {
        yield return new WaitForEndOfFrame();
        EnableKinesis(pickupSelect, active);
    }
}
