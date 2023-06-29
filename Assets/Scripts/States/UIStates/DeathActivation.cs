using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathActivation : MenuActivation
{
    [SerializeField] Animator deathAnimController;

    public override void Activate()
    {

    }

    public override void Deactivate()
    {
        deathAnimController.SetTrigger("ExitLose");
        GameManager.instance.PlayMenuState();
    }

}
