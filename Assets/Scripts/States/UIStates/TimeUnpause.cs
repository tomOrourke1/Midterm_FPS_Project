using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUnpause : MenuActivation
{
    public override void Activate()
    {
        GameManager.instance.PlayMenuState();
        UIManager.instance.Unpaused();
    }

    public override void Deactivate()
    {
    }
}
