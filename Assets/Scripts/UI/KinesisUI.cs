using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
struct PickupImages
{
    [SerializeField] Sprite displayImage;
    [SerializeField] string kinesisName;

    public Sprite GetSprite()
    {
        return displayImage;
    }

    public string GetName()
    {
        return kinesisName;
    }
}

public class KinesisUI : MonoBehaviour
{
    [SerializeField] KinesisPickup pickupComponents;
    [SerializeField] PickupImages aero;
    [SerializeField] PickupImages electro;
    [SerializeField] PickupImages tele;
    [SerializeField] PickupImages pyro;
    [SerializeField] PickupImages cryo;

    private Sprite _sprite;
    private string _name;

    private void Start()
    {
        switch (pickupComponents.pickupSelect)
        {
            case KinesisSelect.aerokinesis:
                _sprite = aero.GetSprite();
                _name = aero.GetName();
                break;

            case KinesisSelect.electrokinesis:
                _sprite = electro.GetSprite();
                _name = electro.GetName();
                break;

            case KinesisSelect.telekinesis:
                _sprite = tele.GetSprite();
                _name = tele.GetName();
                break;

            case KinesisSelect.pyrokinesis:
                _sprite = pyro.GetSprite();
                _name = pyro.GetName();
                break;

            case KinesisSelect.cryokinesis:
                _sprite = cryo.GetSprite();
                _name = cryo.GetName();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.instance.ShowInfoUI(_sprite, _name);
        }
    }
}
