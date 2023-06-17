using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandAnimEvents : MonoBehaviour
{

    [SerializeField] MeleeScript melee;



    public void Melee()
    {
        melee.SetCanknife();
    }

}
