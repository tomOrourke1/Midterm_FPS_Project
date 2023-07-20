using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker instance;


    [SerializeField] Camera cam;

    float shakeIntensity;
    float shakeSpeed;
    float shakeTime;
    bool doesShake;

    float time;

    Vector3 pos;
    float xOffset;
    float yOffset;


    private void Awake()
    {
        if(instance == null)
            instance = this;
    }


    private void Update()
    {

        if(doesShake)
        {
            var xPos = Mathf.Sin(shakeSpeed * Time.time + xOffset) * shakeIntensity;
            var yPos = Mathf.Sin(shakeSpeed * Time.time + yOffset) * shakeIntensity;


            var newPos = pos;
            newPos.x += xPos;
            newPos.y += yPos;

            cam.transform.localPosition = newPos;

            if(time >= shakeTime)
            {
                doesShake = false;
                cam.transform.localPosition = pos;
            }

            time += Time.deltaTime;
        }
    }



    public void StartShake(float intensity, float speed, float time)
    {
        if (doesShake)
            return;

        shakeIntensity = intensity;
        shakeSpeed = speed;
        shakeTime = time;
        doesShake = true;
        this.time = 0;


        xOffset = Random.Range(0f, 10f);
        yOffset = Random.Range(0f, 10f);

        pos = cam.transform.localPosition;
    }

    public void ForceStop()
    {
        cam.transform.localPosition = pos;
        doesShake = false;
    }



}
