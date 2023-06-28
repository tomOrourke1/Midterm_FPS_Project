using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialActivation : MenuActivation
{
    public override void Activate()
    {
        GameManager.instance.TimePause();
        Cursor.lockState = CursorLockMode.Confined;
    }

    public override void Deactivate()
    {
        GameManager.instance.TimeUnpause();
        UIManager.instance.GetRadialScript().SelectKinesis();
        Cursor.lockState = CursorLockMode.Locked;
    }
}
