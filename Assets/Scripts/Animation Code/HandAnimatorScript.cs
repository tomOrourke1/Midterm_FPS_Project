using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimatorScript : MonoBehaviour
{


    [SerializeField] Animator rightHandAnimator;




    public void PlayShootAnim()
    {
        rightHandAnimator.SetTrigger("OnShoot");
    }


}
