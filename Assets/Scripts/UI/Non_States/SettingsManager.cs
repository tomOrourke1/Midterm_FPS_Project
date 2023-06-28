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

    [Header("Audio Sliders")]
    [SerializeField] AudioMixer masterMix;
    [SerializeField] AudioMixer sfxMix;
    [SerializeField] AudioMixer musicMix;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;
    
    [Header("Game Sliders")]
    [SerializeField] Slider fovSlider;
    [SerializeField] Slider mouseSensSlider;
    [SerializeField] Slider controllerSensSlider;
    [SerializeField] Toggle invertYToggle;

    [Header("Graphics")]
    [SerializeField] GameObject hitmarkerObj;
    [SerializeField] Toggle hitmarkerToggle;
    [SerializeField] Image reticleImage;

    // Temp data containers
    float tempMaster;
    float tempSFX;
    float tempMusic;
    float tempFOV;
    float tempMouseSens;
    float tempControllerSens;
    bool tempInvY;
    bool tempHitmarkerEnabled;
    Sprite tempSprite;

    private void Awake()
    {
        UpdateSlidersToTempValues();
    }

    public void SaveToSettingsObj()
    {
        // Audio 
        settings.masterVol = tempMaster;
        settings.sfxVol = tempSFX;
        settings.musicVol = tempMusic;

        // Gameplay
        settings.fieldOfView = tempFOV;
        settings.mouseSensitivity = tempMouseSens;
        settings.controllerSensitivty = tempControllerSens;
        settings.invertY = tempInvY;

        // Graphics
        hitmarkerObj.SetActive(tempHitmarkerEnabled);
        reticleImage.sprite = tempSprite;

        GameManager.instance.GetPlayerScript().SetOrigFov(tempFOV);
    }

    public void UpdateObjectsToValues()
    {
        // Audio
        masterMix.SetFloat("MasterVolume", tempMaster);
        sfxMix.SetFloat("SFXVolume", tempSFX);
        musicMix.SetFloat("MusicVolume", tempMusic);

        // Gameplay
        Camera.main.fieldOfView = tempFOV;
        Camera.main.GetComponent<CameraController>().SetSensitivity(tempMouseSens);
        // change controller sensitivty here
        Camera.main.GetComponent<CameraController>().SetInvert(tempInvY);

        // Graphics
        reticleImage.sprite = tempSprite;
        hitmarkerObj.SetActive(tempHitmarkerEnabled);
    }

    public void CancelSettingsObj()
    {
        RevertTempValuesToPrevious();
        UpdateSlidersToTempValues();
    }

    public void SetMasterVolume()
    {
        tempMaster = masterSlider.value;
    }

    public void SetSFXVolume()
    {
        tempSFX = sfxSlider.value;
    }

    public void SetMusicVolume()
    {
        tempMusic = musicSlider.value;
    }

    public void SetCameraFOV()
    {
        tempFOV = fovSlider.value;
    }

    public void SetMouseSens()
    {
        tempMouseSens = mouseSensSlider.value;
    }

    public void SetControllerSens()
    {
        tempControllerSens = controllerSensSlider.value;
    }

    public void SetCameraInvertY()
    {
        tempInvY = invertYToggle.isOn;
    }

    public void SetHitmarkerEnabled()
    {
        tempHitmarkerEnabled = hitmarkerToggle.isOn;
    }

    private void RevertTempValuesToPrevious()
    {
        // Temp Audio Values
        tempMaster = settings.masterVol;
        tempSFX = settings.sfxVol;
        tempMusic = settings.musicVol;

        // Temp Gameplay Values
        tempFOV = settings.fieldOfView;
        tempMouseSens = settings.mouseSensitivity;
        tempControllerSens = settings.controllerSensitivty;
        tempInvY = settings.invertY;

        // Graphics
        tempHitmarkerEnabled = settings.hitmarkerEnabled;
        tempSprite = settings.currentRetical;
    }

    private void UpdateSlidersToTempValues()
    {
        // Audio Slider updates
        masterSlider.value = tempMaster;
        sfxSlider.value = tempSFX;
        musicSlider.value = tempMusic;

        // Gameplay Slider updates
        fovSlider.value = tempFOV;
        mouseSensSlider.value = tempMouseSens;
        controllerSensSlider.value = tempControllerSens;
        invertYToggle.isOn = tempInvY;

        // Graphics
        hitmarkerObj.SetActive(tempHitmarkerEnabled);
        reticleImage.sprite = tempSprite;
    }
}
