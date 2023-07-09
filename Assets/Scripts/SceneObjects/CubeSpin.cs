using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeSpin : MonoBehaviour
{
    [SerializeField] float rotRate;

    private bool flipX, flipY, flipZ;
    private bool runningIENUM;
    // Update is called once per frame
    void Update()
    {
        float rotX = flipX ? rotRate : -rotRate;
        float rotY = flipY ? rotRate : -rotRate;
        float rotZ = flipZ ? rotRate : -rotRate;

        if (!runningIENUM)
        {
            StartCoroutine(flipDir());
        }

        transform.Rotate(rotX, rotY, rotZ);
    }

    private IEnumerator flipDir()
    {
        runningIENUM = true;
        yield return new WaitForSeconds(1f);

        flipX = Random.Range(0f, 1f) > 0.5f ? true : false;
        flipY = Random.Range(0f, 1f) > 0.5f ? true : false;
        flipZ = Random.Range(0f, 1f) > 0.5f ? true : false;

        runningIENUM = false;
    }
}
