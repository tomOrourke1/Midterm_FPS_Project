using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class UI_Manager : MonoBehaviour
{
    //the pause menu
    public Animator PausePanel;
    public Animator WinPanel;
    public Animator LossPanel;

    public static UI_Manager instance;

    private void Awake()
    {
        instance = this; 
    }

    //Function will remove the pause menu from the screen
    public void DisableBoolAnimator(Animator animator)
    {
        animator.SetBool("IsDisplayed", false);
    }

    //Will bring the pause menu on screen
    public void EnableBoolAnimator(Animator animator)
    {
        animator.SetBool("IsDisplayed", true);
    }
}
