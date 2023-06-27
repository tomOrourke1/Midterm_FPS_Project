using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuState : UIState
{
    [Header("Menu Objects")]
    [SerializeField] GameObject UIObject;
    [SerializeField] MenuActivation activateMenu;

    [Header("Enable Menu Here")]
    [SerializeField] bool doesActivateHere = true;
    [SerializeField] bool doesDeactivateHere = true;
    public override void OnEnter()
    {
        if(doesActivateHere)
            UIObject.SetActive(true);
        activateMenu?.Activate();
    }


    public override void OnExit()
    {
        if(doesDeactivateHere)
            UIObject.SetActive(false);
        activateMenu?.Deactivate();
    }
}
