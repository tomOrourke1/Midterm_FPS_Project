using System.Collections;
using System.Collections.Generic;
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

    [Header("Bobbing Stats")]
    [SerializeField] float freq;
    [SerializeField] float amp;

    [Header("Colors")]
    [SerializeField] Material aeroMat;
    [SerializeField] Material electroMat;
    [SerializeField] Material teleMat;
    [SerializeField] Material pyroMat;
    [SerializeField] Material cryoMat;

    Vector3 startingPos;

    private void Start()
    {
        startingPos = transform.position;
        SetMatColor(pickupSelect);
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

    private void SetMatColor(KinesisSelect picked)
    {
        switch (picked)
        {
            case KinesisSelect.aerokinesis:
                gameObject.GetComponent<MeshRenderer>().material = aeroMat;
                break;

            case KinesisSelect.electrokinesis:
                gameObject.GetComponent<MeshRenderer>().material = electroMat;
                break;

            case KinesisSelect.telekinesis:
                gameObject.GetComponent<MeshRenderer>().material = teleMat;
                break;

            case KinesisSelect.pyrokinesis:
                gameObject.GetComponent<MeshRenderer>().material = pyroMat;
                break;

            case KinesisSelect.cryokinesis:
                gameObject.GetComponent<MeshRenderer>().material = cryoMat;
                break;
        }
    }
}
