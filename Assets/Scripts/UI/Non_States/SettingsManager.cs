using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Settings Scriptable Object")]
    public SettingsObject settings;

    [Header("Audio Sliders")]
    [SerializeField] AudioMixer masterMix;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;

    [Header("Game Sliders")]
    [SerializeField] Slider fovSlider;
    [SerializeField] Slider mouseSensSlider;
    [SerializeField] Slider controllerSensSlider;
    [SerializeField] Toggle invertYToggle;

    [Header("Graphics")]
    [SerializeField] GameObject hitmarkerParentObj;
    [SerializeField] Toggle hitmarkerToggle;
    [SerializeField] Image reticleImage;
    [SerializeField] RectTransform highlighterOutline;

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

    [SerializeField] Transform hlStorePos;

    private void Awake()
    {
        highlighterOutline.position = hlStorePos.position;
        SetOriginalValues();
        UpdateSliders();
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
        hitmarkerParentObj.SetActive(tempHitmarkerEnabled);
        reticleImage.sprite = tempSprite;

        KeepKinesis();
        
        GameManager.instance.GetPlayerScript().SetOrigFov(tempFOV);
    }

    public void CancelSettingsObj()
    {
        SetOriginalValues();
        UpdateSliders();
        KeepKinesis();
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

    private void SetOriginalValues()
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
        Debug.Log(settings.currentRetical);
        tempSprite = settings.currentRetical;

        KeepKinesis();
    }

    private void UpdateSliders()
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
        hitmarkerParentObj.SetActive(tempHitmarkerEnabled);
        reticleImage.sprite = tempSprite;
    }
    
    public void UpdateObjectsToValues()
    {
        // Audio
        masterMix.SetFloat("MasterVolume", tempMaster);
        masterMix.SetFloat("SFXVolume", tempSFX);
        masterMix.SetFloat("MusicVolume", tempMusic);

        // Gameplay
        Camera.main.fieldOfView = tempFOV;
        Camera.main.GetComponent<CameraController>().SetSensitivity(tempMouseSens);
        // change controller sensitivty here
        Camera.main.GetComponent<CameraController>().SetInvert(tempInvY);

        // Graphics
        reticleImage.sprite = tempSprite;
        hitmarkerParentObj.SetActive(tempHitmarkerEnabled);
    }

    public void SetReticleImage(Image img)
    {
        highlighterOutline.position = img.gameObject.transform.position;
        hlStorePos.position = highlighterOutline.position;
        tempSprite = img.sprite;
    }

    private void KeepKinesis()
    {
        // Kinesis Bools
        settings.aeroOn = GameManager.instance.GetEnabledList().AeroEnabled();
        settings.pyroOn = GameManager.instance.GetEnabledList().PyroEnabled();
        settings.cryoOn = GameManager.instance.GetEnabledList().CryoEnabled();
        settings.electroOn = GameManager.instance.GetEnabledList().ElectroEnabled();
        settings.teleOn = GameManager.instance.GetEnabledList().TeleEnabled();
    }
}
