using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayActivation : MenuActivation
{

    public override void Activate()
    {
        GameManager.instance.PlayMenuState();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public override void Deactivate()
    {

    }
}
