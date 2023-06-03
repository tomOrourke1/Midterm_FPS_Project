using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public void DisableBoolAnimator(Animator animator)
    {
        animator.SetBool("IsDisplayed", false);
    }
    public void EnableBoolAnimator(Animator animator)
    {
        animator.SetBool("IsDisplayed", true);
    }
}
