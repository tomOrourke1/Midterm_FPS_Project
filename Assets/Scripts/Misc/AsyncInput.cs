using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsyncInput
{
    private bool activated;

    /// <summary>
    /// Async stuff.
    /// </summary>
    /// <returns></returns>
    public bool GetInput()
    {
        if(activated)
        {
            activated = false;
            return true;
        }    
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Sets the bool value to true
    /// </summary>
    public void SetInput()
    {
        activated = true;
    }

    /// <summary>
    /// Sets the bool value to the value passed in.
    /// </summary>
    /// <param name="value">The state at which to change the input bool to.</param>
    public void SetInput(bool value)
    {
        activated = value;
    }
}
