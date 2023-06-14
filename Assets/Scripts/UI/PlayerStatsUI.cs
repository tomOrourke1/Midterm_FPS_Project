using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [Header("Player Stat Fillers")]
    [Tooltip("Focus image to alter.")]
    [SerializeField] Image focusSlider;
    [Tooltip("Shield image to alter.")]    
    [SerializeField] Image shieldSlider;
    [Tooltip("Health image to alter.")]
    [SerializeField] Image healthSlider;
    [Tooltip("The health icon to enable when the shield is empty.")]
    [SerializeField] Image healthIcon;
    [Tooltip("The shield icon to enable when the shield is empty.")]
    [SerializeField] Image shieldIcon;

    // C# has inherent private protection but defining it just to be safe
    PlayerResources instance;

    void Start()
    {
        instance = GameManager.instance.GetPlayerResources();
        UpdateValues();
    }

    // When the damage and HP refilling are added introduce these into those functions. 
    public void UpdateValues()
    {
        if(instance == null)
        {
            instance = GameManager.instance.GetPlayerResources();
        }

        focusSlider.fillAmount = instance.Focus.GetPercent();
        shieldSlider.fillAmount = instance.Shield.GetPercent();
        healthSlider.fillAmount = instance.Health.GetPercent();

        if (instance.Shield.CurrentValue > 0)
        {
            EnableShieldIcon();
            DisableHealthIcon();
        }
        else if (instance.Health.CurrentValue > 0 && instance.Shield.CurrentValue == 0)
        {
            EnableHealthIcon();
            DisableShieldIcon();
        }
    }

    /// <summary>
    /// Enables the health icon in the player stast.
    /// </summary>
    private void EnableHealthIcon()
    {
        healthIcon.enabled = true;
    }

    /// <summary>
    /// Disables the health icon in the player stast.
    /// </summary>
    private void DisableHealthIcon()
    {
        healthIcon.enabled = false;
    }

    /// <summary>
    /// Enables the shield icon in the player stast.
    /// </summary>
    private void EnableShieldIcon()
    {
        shieldIcon.enabled = true;
    }

    /// <summary>
    /// Disables the shield icon in the player stast.
    /// </summary>
    private void DisableShieldIcon()
    {
        shieldIcon.enabled = false;
    }
}
