using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UnlockTerminal : MonoBehaviour, IInteractable, IEnvironment
{
    [SerializeField] UnityEvent Activate;
    [SerializeField] UnityEvent Deactivate;

    [SerializeField] GameObject Text;

    [SerializeField] Material LockedMaterial;
    [SerializeField] Material UnlockedMaterial;

    public void Interact()
    {
        if (GameManager.instance.GetKeyChain().GetKeys() >= 1)
        {
            // Activate
            Activate.Invoke();

            // Remove display text  
            Text.SetActive(false);

            // Change material
            GetComponent<MeshRenderer>().material = UnlockedMaterial;

            // Remove a key
            GameManager.instance.GetKeyChain().removeKeys(1);
        } 
        else
        {
            Debug.LogError("No kevin go cry in a corner.");
        }
    }

    public void StartObject()
    {
        //Debug.Log("Hello?");
        GetComponent<MeshRenderer>().material = LockedMaterial;
    }

    public void StopObject()
    {
        GetComponent<MeshRenderer>().material = LockedMaterial;

        //Debug.Log("Hello?");
        //Debug.Log(GetComponent<MeshRenderer>().material == LockedMaterial);
        Deactivate.Invoke();
    }
}
