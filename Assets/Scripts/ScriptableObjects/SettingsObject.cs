using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings Object", menuName = "Settings")]
public class SettingsObject : ScriptableObject
{
    // Audios
    public float masterVol;
    public float sfxVol;
    public float musicVol;
    
    public float fieldOfView;
    public float mouseSensitivity;
    public float controllerSensitivty;
    public bool invertY;

    // Graphics
    public Sprite currentRetical;
    public bool hitmarkerEnabled;

    // Kinesis Trackers
    public bool pyroOn;
    public bool aeroOn;
    public bool cryoOn;
    public bool electroOn;
    public bool teleOn;

    // Scene tracker
    public string currentScene;
}
