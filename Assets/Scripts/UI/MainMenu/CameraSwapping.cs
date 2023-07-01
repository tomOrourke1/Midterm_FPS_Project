using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwapping : MonoBehaviour
{
    [Header("Camera Stuff")]
    [SerializeField] Camera[] cameras;
    [SerializeField, Range(5f, 20f)] float minWaitTime;
    [SerializeField, Range(21f, 60f)] float maxWaitTime;

    private bool waiter;
    private float currentWaitingTime;
    private int picked;
    private Camera currentCamera;
    private float changed;
    private bool canChange;
    private float rateFOV;

    private void Start()
    {
        ChangeCam();
    }

    private void Update()
    {
        if (!waiter)
        {
            StartCoroutine(WaitToChange());
        }

        if (currentCamera.fieldOfView != changed && canChange)
        {
            currentCamera.fieldOfView = Mathf.MoveTowards(currentCamera.fieldOfView, changed, rateFOV);
        }
        else if (!canChange)
        {
            StartCoroutine(please());
        }

    }

    private IEnumerator WaitToChange()
    {
        waiter = true;
        currentWaitingTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(currentWaitingTime);

        ChangeCam();

        waiter = false;
    }

    private void ChangeCam()
    {
        foreach (Camera cam in cameras)
        {
            cam.enabled = false;
        }

        picked = Random.Range(0, cameras.Length);
        cameras[picked].enabled = true;
        currentCamera = cameras[picked];
    }

    private IEnumerator please()
    {
        rateFOV = Random.Range(0.01f, 0.1f);
        changed = 60 + Random.Range(-7f, 7f);
        canChange = true;
        yield return new WaitForSeconds(Random.Range(2.5f, 6f));
        canChange = false;
    }

}
