using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsyncInput
{



    private bool activated;



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


    public void SetInput()
    {
        activated = true;
    }


    public void SetInput(bool value)
    {
        activated = value;
    }




}
