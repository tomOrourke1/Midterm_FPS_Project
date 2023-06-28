using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseActivation : MenuActivation
{
    [SerializeField] Animator pauseAnimController;

    public override void Activate()
    {
        pauseAnimController.SetBool("ExitPause", false);
        GameManager.instance.PauseMenuState();
    }

    public override void Deactivate()
    {
        pauseAnimController.SetBool("ExitPause", true);
        GameManager.instance.PlayMenuState();
    }

}
