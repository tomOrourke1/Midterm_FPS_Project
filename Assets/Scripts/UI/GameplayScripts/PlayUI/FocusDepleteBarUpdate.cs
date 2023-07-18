using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusDepleteBarUpdate : MonoBehaviour
{
    [SerializeField] GameObject depleteBar;
    [SerializeField] float waitTime;

    float timeStart;
    bool running;

    private void Update()
    {
        if (running)
        {
            if(Time.time - timeStart >= waitTime)
            {
                TurnOff();
            }
        }
    }

    public void ShowDeplete()
    {
        timeStart = Time.time;
        running = true;
        depleteBar.SetActive(true);
    }

    private void TurnOff()
    {
        depleteBar.SetActive(false);
        running = false;
    }
}
