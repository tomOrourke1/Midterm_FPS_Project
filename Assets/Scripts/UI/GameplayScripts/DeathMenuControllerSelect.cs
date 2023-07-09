using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeathMenuControllerSelect : MonoBehaviour
{
    [SerializeField] GameObject retryLinesObj;
    [SerializeField] GameObject mainmenuLinesObj;


    public void DisplayRetryLines()
    {
        retryLinesObj.SetActive(true);
        mainmenuLinesObj.SetActive(false);
    }

    public void DisplayMainMenuLines()
    {
        retryLinesObj.SetActive(false);
        mainmenuLinesObj.SetActive(true);
    }
}
