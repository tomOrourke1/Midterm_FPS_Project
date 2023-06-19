using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Settings Scriptable Object")]
    public SettingsObject settings;

    [Header("Sliders")]
    [SerializeField] AudioMixer masterMix;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider fovSlider;
    [SerializeField] Slider sensitivitySlider;
    [SerializeField] Toggle invertYToggle;

    float tempVol;
    float tempFOV;
    float tempSens;
    bool tempInvY;

    private void Awake()
    {
        tempVol = settings.volume;
        tempFOV = settings.fieldOfView;
        tempSens = settings.sensitivity;
        tempInvY = settings.invertY;

        volumeSlider.value = tempVol;
        fovSlider.value = tempFOV;
        sensitivitySlider.value = tempSens;
        invertYToggle.isOn = tempInvY;
    }

    public void SaveToSettingsObj()
    {
        settings.volume = tempVol;
        settings.fieldOfView = tempFOV;
        settings.sensitivity = tempSens;
        settings.invertY = tempInvY;

        GameManager.instance.GetPlayerScript().SetOrigFov(tempFOV);
    }

    public void UpdateObjectsToValues()
    {
        masterMix.SetFloat("MasterVolume", tempVol);
        Camera.main.fieldOfView = tempFOV;
        Camera.main.GetComponent<CameraController>().SetSensitivity(tempSens);
        Camera.main.GetComponent<CameraController>().SetInvert(tempInvY);
    }

    public void CancelSettingsObj()
    {
        tempVol = settings.volume;
        tempFOV = settings.fieldOfView;
        tempSens = settings.sensitivity;
        tempInvY = settings.invertY;
        
        volumeSlider.value = tempVol;
        fovSlider.value = tempFOV;
        sensitivitySlider.value = tempSens;
        invertYToggle.isOn = tempInvY;
    }

    public void SetVolume(float vol)
    {
        tempVol = vol;
    }

    public void SetCameraFOV(float fov)
    {
        tempFOV = fov;
    }

    public void SetMouseSens(float sens)
    {
        tempSens = sens;
    }

    public void SetCameraInvertY(bool invY)
    {
        tempInvY = invY;
    }
}
