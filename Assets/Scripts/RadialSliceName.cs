using UnityEngine;

public class RadialSliceName : MonoBehaviour
{
    public static RadialSliceName instance;
    public string weaponName;

    private void Awake()
    {
        instance = this;
    }
}
