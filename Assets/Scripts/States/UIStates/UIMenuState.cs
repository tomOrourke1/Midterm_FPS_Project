using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuState : UIState
{
    [Header("Menu Objects")]
    [SerializeField] GameObject UIObject;
    [SerializeField] List<MenuActivation> activeMenues;

    [Header("Enable Menu Here")]
    [SerializeField] bool doesActivateHere = true;
    [SerializeField] bool doesDeactivateHere = true;
    
    public override void OnEnter()
    {
        if(doesActivateHere)
            UIObject.SetActive(true);
        foreach(var menu in activeMenues)
        {
            if(menu.doesActivate)
                menu?.Activate();
        }
    }

    public override void OnExit()
    {
        if(doesDeactivateHere)
            UIObject.SetActive(false);
        foreach (var menu in activeMenues)
        {
            if(menu.doesDeactivate)
                menu?.Deactivate();
        }
    }
}
