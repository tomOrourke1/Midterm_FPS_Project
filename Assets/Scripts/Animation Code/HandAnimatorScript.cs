using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimatorScript : MonoBehaviour
{


    [SerializeField] Animator rightHandAnimator;
    [SerializeField] Animator leftHandAnimator;



    public void PlayShootAnim()
    {
        rightHandAnimator.SetTrigger("OnShoot");
    }


    public void PlayTelePull()
    {
        leftHandAnimator.SetBool("Telekinesis", true);
    }
    public void PlayTelePush()
    {
        leftHandAnimator.SetBool("Telekinesis", false);

    }



    public void PlayCryoHold()
    {
        leftHandAnimator.SetBool("Cryo", true);
    }
    public void PLayCryoThrow()
    {
        leftHandAnimator.SetBool("Cryo", false) ;
    }



    public void StartElectro()
    {
        leftHandAnimator.SetBool("Electro", true);
    }
    public void StopElectro()
    {
        leftHandAnimator.SetBool("Electro", false);
    }


    public void StartPyro()
    {
        leftHandAnimator.SetBool("Fire", true);
    }

    public void StopPyro()
    {
        leftHandAnimator.SetBool("Fire", false);

    }

    public void Melee()
    {
        rightHandAnimator.SetTrigger("OnKnife");
    }

}
