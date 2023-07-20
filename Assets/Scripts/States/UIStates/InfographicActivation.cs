using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfographicActivation : MenuActivation
{
    [SerializeField] GameObject selectedGameObject;

    public override void Activate()
    {
        PingCurrentEvent();
    }

    public override void Deactivate()
    {

    }

    public void PingCurrentEvent()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selectedGameObject);
    }
}
