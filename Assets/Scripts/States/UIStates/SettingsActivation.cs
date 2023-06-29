using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsActivation : MenuActivation
{
    public override void Activate()
    {
        GameManager.instance.PauseMenuState();
    }

    public override void Deactivate()
    {
        GameManager.instance.PlayMenuState();
    }
}
