using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePause : MenuActivation
{
    public override void Activate()
    {
        GameManager.instance.PauseMenuState();
    }

    public override void Deactivate()
    {

    }
}
