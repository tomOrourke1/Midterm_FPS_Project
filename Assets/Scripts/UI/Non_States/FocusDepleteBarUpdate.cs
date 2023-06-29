using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusDepleteBarUpdate : MonoBehaviour
{
    [SerializeField] GameObject depleteBar;

    public void ShowDeplete()
    {
        depleteBar.SetActive(true);
        StartCoroutine(TurnOff());
    }

    private IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(0.15f);
        depleteBar.SetActive(false);
    }
}
