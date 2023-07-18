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
    [SerializeField] MeshRenderer rend;

    // Kevins things he added when reworking the model + fancy things
    [Header("Don't Touch Below")]
    [SerializeField] TextMeshProUGUI textCountUI;
    [SerializeField] GameObject remainingUI;
    [SerializeField] GameObject completedUI;

    [Tooltip("This is the maximum amount of times that you can interract with the terminal")]
    [SerializeField, Range(1,5)] int InteractLimit;

    int InteractLimitInit;
    bool ActivationLock = false;

    private void Start()
    {
        textCountUI.text = InteractLimit.ToString();
        InteractLimitInit = InteractLimit;
    }

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
        //else
        //{
        //    Debug.LogError("No kevin go cry in a corner.");
        //}

        if (InteractLimit == 0)
        {
            ActivationLock = true;

            // Change material
            rend.material = UnlockedMaterial;

        }
        
        UIUpdate();
    }

    public void StartObject()
    {
        //Debug.Log("Hello?");
        ActivationLock = false;
        rend.material = LockedMaterial;
        UIUpdate();
    }

    public void StopObject()
    {
        rend.material = LockedMaterial;

        InteractLimit = InteractLimitInit;

        //Debug.Log("Hello?");
        //Debug.Log(GetComponent<MeshRenderer>().material == LockedMaterial);
        Deactivate.Invoke();
    }

    private void UIUpdate()
    {
        if (InteractLimit == 0)
        {
            completedUI.SetActive(true);
            remainingUI.SetActive(false);
            textCountUI.text = " ";
        }
        else if (InteractLimit > 0)
        {
            completedUI.SetActive(false);
            remainingUI.SetActive(true);
            textCountUI.text = InteractLimit.ToString();
        }
    }
}
