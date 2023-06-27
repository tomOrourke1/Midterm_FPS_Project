using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIState : MonoBehaviour, IState
{
    public virtual void OnEnter()
    {
    }

    public virtual void OnExit()
    {
    }

    public virtual void Tick()
    {
    }

    public virtual bool ExitCondition()
    {
        return false;
    }


}
