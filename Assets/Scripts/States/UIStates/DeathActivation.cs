using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeathActivation : MenuActivation
{
    [SerializeField] Animator deathAnimController;
    [SerializeField] GameObject selectedGameObject;

    public override void Activate()
    {
        PingCurrentEvent();
    }

    public override void Deactivate()
    {
        deathAnimController.SetTrigger("ExitLose");
        GameManager.instance.PlayMenuState();
    }

    public void PingCurrentEvent()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selectedGameObject);
    }

}
