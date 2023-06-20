using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialKillEntities : MonoBehaviour, IEnvironment
{

    [SerializeField] Transform parent;

    public void KillEntities()
    {
        var childs = parent.GetComponentsInChildren<IEntity>();
        var objs = parent.GetComponentsInChildren<IEnvironment>();
        foreach (var child in childs)
        {
            child.Respawn();
        }
        foreach (var obj in objs)
        {
            obj.StopObject();
        }
    }

    public void ResetObject()
    {
    }

    public void StartObject()
    {
    }

    public void StopObject()
    {
        KillEntities();
    }
}
