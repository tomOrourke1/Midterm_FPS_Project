using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWaitingScript : UIState
{
    [SerializeField] float waitTime;

    bool exit;

    public override void OnEnter()
    {
        StopAllCoroutines();
        exit = false;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        exit = true;
    }


    public override bool ExitCondition()
    {
        return exit;
    }

}
