using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnvironment
{
    /*
        Reset will...
    
            Move moving platforms to their start states

            Start any enabled lasers

            Reset Doors to their original state
    */

    public void ResetObject();
}
