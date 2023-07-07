using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatMenu : MonoBehaviour
{
    [SerializeField] SettingsManager settings;

    [SerializeField] Toggle aeroToggle;
    [SerializeField] Toggle teleToggle;
    [SerializeField] Toggle cryoToggle;
    [SerializeField] Toggle electroToggle;
    [SerializeField] Toggle pyroToggle;

    // Start is called before the first frame update
    void Awake()
    {
        aeroToggle.isOn = GameManager.instance.GetEnabledList().AeroEnabled();
        teleToggle.isOn = GameManager.instance.GetEnabledList().TeleEnabled();
        cryoToggle.isOn = GameManager.instance.GetEnabledList().CryoEnabled();
        electroToggle.isOn = GameManager.instance.GetEnabledList().ElectroEnabled();
        pyroToggle.isOn = GameManager.instance.GetEnabledList().PyroEnabled();
    }

    public void ToggleAero()
    {
        GameManager.instance.GetEnabledList().AeroSetActive(aeroToggle.isOn);
    }

    public void ToggleElectro()
    {
        GameManager.instance.GetEnabledList().ElectroSetActive(electroToggle.isOn);
    }

    public void TogglePyro()
    {
        GameManager.instance.GetEnabledList().PyroSetActive(pyroToggle.isOn);
    }

    public void ToggleCryo()
    {
        GameManager.instance.GetEnabledList().CryoSetActive(cryoToggle.isOn);
    }

    public void ToggleTele()
    {
        GameManager.instance.GetEnabledList().TeleSetActive(teleToggle.isOn);
    }
}
