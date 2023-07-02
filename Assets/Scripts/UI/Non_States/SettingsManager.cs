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
        Subscriber();
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

        GameManager.instance.GetPlayerScript().GetFov().SetOrigFov(tempFOV);// .SetOrigFov(tempFOV);
    }

    public void CancelSettingsObj()
    {
        SetOriginalValues();
        UpdateSliders();
        KeepKinesis();
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
        masterMix.SetFloat("MasterVolume", Mathf.Log10(tempMaster) * 20f);
        masterMix.SetFloat("SFXVolume", Mathf.Log10(tempSFX) * 20f);
        masterMix.SetFloat("MusicVolume", Mathf.Log10(tempMusic) * 20f);

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

    // ================================================================================
    //         SUBSCRIPTION BASED EVENTS BELOW - DON'T TOUCH PLEASE AND THANK YOU
    // ================================================================================

    private void Subscriber()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        controllerSensSlider.onValueChanged.AddListener(SetControllerSens);
        mouseSensSlider.onValueChanged.AddListener(SetMouseSens);
        fovSlider.onValueChanged.AddListener(SetCameraFOV);

        hitmarkerToggle.onValueChanged.AddListener(SetHitmarkerEnabled);
        invertYToggle.onValueChanged.AddListener(SetCameraInvertY);
    }

    public void SetMasterVolume(float value)
    {
        tempMaster = value;
    }

    public void SetSFXVolume(float value)
    {
        tempSFX = value;
    }

    public void SetMusicVolume(float value)
    {
        tempMusic = value;
    }

    public void SetCameraFOV(float value)
    {
        tempFOV = value;
    }

    public void SetMouseSens(float value)
    {
        tempMouseSens = value;
    }

    public void SetControllerSens(float value)
    {
        tempControllerSens = value;
    }

    public void SetCameraInvertY(bool value)
    {
        tempInvY = value;
    }

    public void SetHitmarkerEnabled(bool value)
    {
        tempHitmarkerEnabled = value;
    }

}
