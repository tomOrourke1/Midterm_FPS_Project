using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] AudioMixer masterMix;

    public void SetVolume(float vol)
    {
        masterMix.SetFloat("MasterVolume", vol);
    }

    public void SetCameraFOV(float fov)
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().fieldOfView = fov;
    }

    public void SetMouseSens(float sens)
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().SetSensitivity(sens);
    }

    public void SetCameraInvertY(bool invert)
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().SetInvert(invert);
    }
}
