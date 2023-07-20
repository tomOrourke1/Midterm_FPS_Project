using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsActivation : MenuActivation
{
    [SerializeField] GameObject selectedGameObject;

    public override void Activate()
    {
        GameManager.instance.PauseMenuState();
        PingCurrentEvent();
    }

    public override void Deactivate()
    {
        GameManager.instance.PlayMenuState();
    }
    public void PingCurrentEvent()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selectedGameObject);
    }
}
