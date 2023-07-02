using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfographicActivation : MenuActivation
{
    [SerializeField] GameObject selectedGameObject;

    public override void Activate()
    {
        EventSystem.current.SetSelectedGameObject(selectedGameObject);
    }

    public override void Deactivate()
    {

    }
}
