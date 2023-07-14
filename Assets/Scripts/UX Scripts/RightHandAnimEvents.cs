using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandAnimEvents : MonoBehaviour
{

    [SerializeField] MeleeScript melee;
    [SerializeField] KnifeSFX sfx;


    public void Melee()
    {
        melee.SetCanknife();
    }

    public void PlaySFX_Melee()
    {
        sfx.Play_OneShot();
    }

}
