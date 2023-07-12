using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UnlockTerminal : MonoBehaviour, IInteractable, IEnvironment
{
    [SerializeField] UnityEvent Activate;
    [SerializeField] UnityEvent Deactivate;

    [SerializeField] Material LockedMaterial;
    [SerializeField] Material UnlockedMaterial;

    [Tooltip("This is the maximum amount of times that you can interract with the terminal")]
    [SerializeField, Range(1,5)] int InteractLimit;

    bool ActivationLock = false;

    public void Interact()
    {
        if (GameManager.instance.GetKeyChain().GetKeys() >= 1 && !ActivationLock)
        {
            // Activate
            Activate.Invoke();

            // Remove a key
            GameManager.instance.GetKeyChain().removeKeys(1);

            InteractLimit--;
        } 
        else
        {
//            Debug.LogError("No kevin go cry in a corner.");
        }

        if (InteractLimit == 0)
        {
            ActivationLock = true;

            // Change material
            GetComponent<MeshRenderer>().material = UnlockedMaterial;
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
