using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings Object", menuName = "Settings")]
public class SettingsObject : ScriptableObject
{
    public float volume;
    public float fieldOfView;
    public float sensitivity;
    public bool invertY;
}
