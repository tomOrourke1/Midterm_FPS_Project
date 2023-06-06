using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDoorActivator
{

    void Activate();
    void SetLockStatus(bool locked);
}
