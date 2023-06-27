using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuState : UIState
{

    [SerializeField] GameObject UIObject;
    [SerializeField] MenuActivation activateMenu;

    public override void OnEnter()
    {
        UIObject.SetActive(true);
        activateMenu?.Activate();
    }


    public override void OnExit()
    {
        UIObject.SetActive(false);
    }
}
