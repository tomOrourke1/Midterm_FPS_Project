using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseActivation : MenuActivation
{
    [SerializeField] Animator pauseAnimController;
    [SerializeField] GameObject selectedGameObject;

    public override void Activate()
    {
        pauseAnimController.SetBool("ExitPause", false);
        GameManager.instance.PauseMenuState();

        PingCurrentEvent();
    }

    public override void Deactivate()
    {
        pauseAnimController.SetBool("ExitPause", true);
        GameManager.instance.PlayMenuState();
        StartCoroutine(UIManager.instance.WaitCloseConfirm());
    }

    public void PingCurrentEvent()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selectedGameObject);
    }
}
