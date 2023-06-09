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


    public void PlayAeroHold()
    {
        leftHandAnimator.SetBool("Aero", true);
    }
    public void PLayAeroThrow()
    {
        leftHandAnimator.SetBool("Aero", false);
    }

    public void StopAll()
    {
        leftHandAnimator.SetBool("Fire", false);
        leftHandAnimator.SetBool("Electro", false);
        leftHandAnimator.SetBool("Cryo", false);
        leftHandAnimator.SetBool("Telekinesis", false);
        leftHandAnimator.SetBool("Aero", false);

        leftHandAnimator.Play("LeftHandIdle");
        rightHandAnimator.Play("RightHandIdle");
    }

    public void StopAllLleftHand()
    {
        leftHandAnimator.SetBool("Fire", false);
        leftHandAnimator.SetBool("Electro", false);
        leftHandAnimator.SetBool("Cryo", false);
        leftHandAnimator.SetBool("Telekinesis", false);
        leftHandAnimator.SetBool("Aero", false);

        leftHandAnimator.Play("LeftHandIdle");
    }

    public void StopAllRightHand()
    {
        rightHandAnimator.Play("RightHandIdle");
    }

}
