using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseActivation : MenuActivation
{
    [SerializeField] Animator pauseAnimController;

    [SerializeField] float waitTime;

    bool isWaiting = false;

    public override void Activate()
    {
        pauseAnimController.SetBool("ExitPause", false);
        StopAllCoroutines();
        isWaiting = false;
    }

    public override void Deactivate()
    {
        pauseAnimController.SetBool("ExitPause", true);
        if (!isWaiting)
            StartCoroutine(WaitToDisplayPlay());
    }

    private IEnumerator WaitToDisplayPlay()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        UIManager.instance.uiStateMachine.SetPlay(true);
        isWaiting = false;
    }

}
