using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class CallNext : MonoBehaviour
{
    [SerializeField] GameObject nextObj;
    [SerializeField] CreditsRunner runner;

    public IEnumerator Next()
    {
        yield return new WaitForSeconds(0.5f);

        if (nextObj != null )
        {
            nextObj.SetActive(true);
        }
        else
        {
            runner?.ForceEndCredits();
        }

        gameObject.SetActive(false);
    }
}
