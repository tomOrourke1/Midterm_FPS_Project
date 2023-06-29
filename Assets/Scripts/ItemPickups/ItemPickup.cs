using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] PickupInfo info;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.instance.ShowInfoUI(info.GetSprite(), info.GetName());
            UIManager.instance.uiStateMachine.SetInfo(true);
        }
    }
}
