using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectScript : MonoBehaviour
{

    [Header("Level Names - In Order\nCORRECT ORDER PLEASE")]
    [SerializeField] string[] levels;

    [SerializeField] ElevatorScript eScript;


    public void Selection(int chosen)
    {
        if (chosen <= levels.Length)
        {
            eScript.FadeTo(levels[chosen]);

        }
    }
}
