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
struct OrbMaterials
{
    [Header("Material Properties")]
    [SerializeField] Material Core;
    [SerializeField] Material Inner;
    [SerializeField] Material Outer;

    public Material GetCore()
    {
        return Core;
    }

    public Material GetInner()
    {
        return Inner;
    }

    public Material GetOuter()
    {
        return Outer;
    }
}

public class KinesisPickup : MonoBehaviour
{
    [Header("Kinesis")]
    public KinesisSelect pickupSelect;
    [SerializeField] bool active = true;

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
}
