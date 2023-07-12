using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffPlayerFocusRgen : MonoBehaviour, IEnvironment
{
    public void StartObject()
    {
        GameManager.instance.GetPlayerObj().GetComponent<RecoverFocusBehaviour>().enabled = false;
    }

    public void StopObject()
    {
        GameManager.instance.GetPlayerObj().GetComponent<RecoverFocusBehaviour>().enabled = true;
        
    }


}
