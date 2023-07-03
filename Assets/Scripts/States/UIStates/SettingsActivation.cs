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
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selectedGameObject);

    }

    public override void Deactivate()
    {
        GameManager.instance.PlayMenuState();
    }
}
