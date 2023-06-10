using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFocusReciever
{
    bool SpendFocus(float amt);
    void AddFocus(float amt);
}
