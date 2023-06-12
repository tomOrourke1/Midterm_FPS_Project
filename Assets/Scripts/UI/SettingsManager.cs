using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] AudioMixer masterMix;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider fovSlider;
    [SerializeField] Slider sensitivitySlider;
    [SerializeField] Toggle invertYToggle;

    private void Awake()
    {
        volumeSlider.value = GetCurrenAudio();
        fovSlider.value = GetCurrentFOV();
        sensitivitySlider.value = GetCurrentSens();
        invertYToggle.isOn = GetCurrentInvY();
    }

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

    public AudioMixer ReturnAudioMixer()
    {
        return masterMix;
    }

    private float GetCurrentFOV()
    {
        return GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().fieldOfView;
    }

    private float GetCurrenAudio()
    {
        masterMix.GetFloat("MasterVolume", out float audioVol);
        return audioVol;
    }

    private float GetCurrentSens()
    {
        return GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().GetSensitivity();
    }
    private bool GetCurrentInvY()
    {
        return GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().GetInvert();
    }
}
