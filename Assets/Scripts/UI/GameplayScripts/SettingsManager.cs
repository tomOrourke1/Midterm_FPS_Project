using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Settings Scriptable Object")]
    public SettingsObject settings;

    [SerializeField] BypassAudioApplyCancel bpScript;

    [Header("Audio Sliders")]
    [SerializeField] AudioMixer masterMix;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] GameObject audioApplyButton;

    [Header("Game Sliders")]
    [SerializeField] Slider fovSlider;
    [SerializeField] Slider mouseSensSlider;
    [SerializeField] Slider controllerSensSlider;
    [SerializeField] Toggle invertYToggle;
    [SerializeField] Toggle aimAssistToggle;
    [SerializeField] Slider aimAssistSlider;
    [SerializeField] GameObject gameplayApplyButton;

    [Header("Graphics")]
    [SerializeField] GameObject hitmarkerParentObj;
    [SerializeField] Toggle hitmarkerToggle;
    [SerializeField] Image reticleImage;
    [SerializeField] RectTransform highlighterOutline;
    [SerializeField] GameObject graphicsApplyButton;

    [SerializeField] GameObject backButton;

    // Temp data containers
    private float tempMaster;
    private float tempSFX;
    private float tempMusic;
    private float tempFOV;
    private float tempMouseSens;
    private float tempControllerSens;
    private float tempAimAssistValue;
    private bool tempInvY;
    private bool tempHitmarkerEnabled;
    private bool tempAimAssistEnabled;
    private Sprite tempSprite;

    private bool playingTestAudio;

    [SerializeField] Transform hlStorePos;

    private void Start()
    {
        Subscriber();
    }

    private void Awake()
    {
        highlighterOutline.position = hlStorePos.position;
        UpdateSliders();
    }


    /// <summary>
    /// Only called in Game Manager, don't use anywhere else.
    /// </summary>
    public void ForceStartValues()
    {
        masterMix.SetFloat("MasterVolume", Mathf.Log10(settings.masterVol) * 20f);
        masterMix.SetFloat("SFXVolume", Mathf.Log10(settings.sfxVol) * 20f);
        masterMix.SetFloat("MusicVolume", Mathf.Log10(settings.musicVol) * 20f);

        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            Camera.main.fieldOfView = settings.fieldOfView;
            Camera.main.GetComponent<CameraController>().SetSensitivity(settings.mouseSensitivity);
            Camera.main.GetComponent<CameraController>().SetInvert(settings.invertY);
        }

        // Add the code thats in ChangeCameraSensitivty() in here because this is using settings obj values not the other ones

        hitmarkerParentObj.SetActive(settings.hitmarkerEnabled);
        reticleImage.sprite = settings.currentRetical;

        // Aim assist force start values go here.
        // Calls the value that needs it, like the Main Camera needs the current sensitivity so we force kick it in there

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
        GetFOVControlScript().SetOrigFov(settings.fieldOfView);
    }


    public void ApplySettings()
    {
        SaveToSettingsObj();
        UpdateObjectsToValues();
    }

    public void CancelSettingsObj()
    {
        SetOriginalValues();
        UpdateSliders();
        KeepKinesis();

        EventSystem.current.SetSelectedGameObject(backButton);
        bpScript.PlayCancel();
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
        tempAimAssistEnabled = settings.aimAssistEnabled;
        tempAimAssistValue = settings.aimAssistValue;

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

        // Aim Assist
        aimAssistToggle.isOn = tempAimAssistEnabled;
        aimAssistSlider.value = tempAimAssistValue;

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

        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            // Gameplay
            Camera.main.fieldOfView = tempFOV;
            Camera.main.GetComponent<CameraController>().SetSensitivity(tempMouseSens);
            Camera.main.GetComponent<CameraController>().SetInvert(tempInvY);
            ChangeControllerSensitivity();
        }

        // Aim assist force start values go here.
        // Calls the value that needs it, like the Main Camera needs the current sensitivity so we force kick it in there

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

    /// <summary>
    /// Used in Settings > Audio > Apply
    /// </summary>
    public void ApplyAudioButton()
    {
        settings.masterVol = tempMaster;
        settings.sfxVol = tempSFX;
        settings.musicVol = tempMusic;

        masterMix.SetFloat("MasterVolume", Mathf.Log10(tempMaster) * 20f);
        masterMix.SetFloat("SFXVolume", Mathf.Log10(tempSFX) * 20f);
        masterMix.SetFloat("MusicVolume", Mathf.Log10(tempMusic) * 20f);

        EventSystem.current.SetSelectedGameObject(audioApplyButton);
        bpScript.PlayApply();
    }

    /// <summary>
    /// Used in Settings > Graphics > Apply
    /// </summary>
    public void ApplyGraphicsButton()
    {
        hitmarkerParentObj.SetActive(tempHitmarkerEnabled);
        reticleImage.sprite = tempSprite;

        settings.hitmarkerEnabled = tempHitmarkerEnabled;
        settings.currentRetical = tempSprite;

        EventSystem.current.SetSelectedGameObject(graphicsApplyButton);
        bpScript.PlayApply();
    }

    /// <summary>
    /// Used in Settings > Gameplay > Apply
    /// </summary>
    public void ApplyGameplayButton()
    {
        settings.fieldOfView = tempFOV;
        settings.mouseSensitivity = tempMouseSens;
        settings.controllerSensitivty = tempControllerSens;
        settings.invertY = tempInvY;

        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            Camera.main.fieldOfView = tempFOV;
            Camera.main.GetComponent<CameraController>().SetSensitivity(tempMouseSens);
            Camera.main.GetComponent<CameraController>().SetInvert(tempInvY);
            ChangeControllerSensitivity();
        }

        settings.aimAssistEnabled = tempAimAssistEnabled;
        settings.aimAssistValue = tempAimAssistValue;

        EventSystem.current.SetSelectedGameObject(gameplayApplyButton);

        bpScript.PlayApply();
    }

    public void ChangeControllerSensitivity()
    {
        // change controller sensitivity in here
    }

    // ================================================================================
    //         SUBSCRIPTION BASED EVENTS BELOW - DON'T TOUCH PLEASE AND THANK YOU
    // ================================================================================

    private void Subscriber()
    {
        aimAssistSlider.interactable = settings.aimAssistEnabled ? true : false;
        
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        controllerSensSlider.onValueChanged.AddListener(SetControllerSens);
        mouseSensSlider.onValueChanged.AddListener(SetMouseSens);
        fovSlider.onValueChanged.AddListener(SetCameraFOV);

        hitmarkerToggle.onValueChanged.AddListener(SetHitmarkerEnabled);
        invertYToggle.onValueChanged.AddListener(SetCameraInvertY);

        aimAssistToggle.onValueChanged.AddListener(SetAimAssistEnabled);
        aimAssistSlider.onValueChanged.AddListener(SetAimAssistValue);
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

    public void SetAimAssistEnabled(bool value)
    {
        aimAssistSlider.interactable = value ? true : false;
        tempAimAssistEnabled = value;
    }

    public void SetAimAssistValue(float value)
    {
        if (tempAimAssistEnabled)
            tempAimAssistValue = value;
    }

    private FOVController GetFOVControlScript()
    {
        return GameManager.instance.GetPlayerScript()?.GetFov();
    }
}
