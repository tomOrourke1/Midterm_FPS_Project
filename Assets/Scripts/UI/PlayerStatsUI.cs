using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [Header("Player Stat Sliders")]
    [Tooltip("Focus image to alter.")]
    [SerializeField] Image focusSlider;
    [Tooltip("Shield image to alter.")]
    [SerializeField] Image shieldSlider;
    [Tooltip("Health image to alter.")]
    [SerializeField] Image healthSlider;

    [Header("Faux Sliders")]
    [Tooltip("Faux Focus image to alter.")]
    [SerializeField] Image fauxFocusSlider;
    [Tooltip("Faux Shield image to alter.")]
    [SerializeField] Image fauxShieldSlider;
    [Tooltip("Faux Health image to alter.")]
    [SerializeField] Image fauxHealthSlider;

    [Header("Icons")]
    [Tooltip("The health icon to enable when the shield is empty.")]
    [SerializeField] Image healthIcon;
    [Tooltip("The shield icon to enable when the shield is empty.")]
    [SerializeField] Image shieldIcon;

    [Header("Timers")]
    [SerializeField] float reducingSpeed;
    private float targetHealth, targetFocus, targetShield;


    // C# has inherent private protection but defining it just to be safe
    PlayerResources instance;

    void Start()
    {
        instance = GameManager.instance.GetPlayerResources();
        UpdateValues();
    }

    // Updates the sliders constantly. There can be a different way to check this. LMK - Kev
    private void Update()
    {
        UpdateSliders();
    }

    // When the damage and HP refilling are added introduce these into those functions. 
    public void UpdateValues()
    {
        if (instance == null)
        {
            instance = GameManager.instance.GetPlayerResources();
        }

        targetFocus = instance.Focus.GetPercent();
        targetShield = instance.Shield.GetPercent();
        targetHealth = instance.Health.GetPercent();

        focusSlider.fillAmount = instance.Focus.GetPercent();
        shieldSlider.fillAmount = instance.Shield.GetPercent();
        healthSlider.fillAmount = instance.Health.GetPercent();

        UpdateIcons();
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

    /// <summary>
    /// Updates the icons below the stat bars on the UI.
    /// </summary>
    private void UpdateIcons()
    {
        if (instance.Shield.CurrentValue > 0)
        {
            EnableShieldIcon();
            DisableHealthIcon();
        }
        else if (instance.Health.CurrentValue > 0 && instance.Shield.CurrentValue == 0)
        {
            DisableShieldIcon();
            EnableHealthIcon();
        }
    }

    /// <summary>
    /// Updates the faux sliders to match them with the real sliders.
    /// </summary>
    private void UpdateSliders()
    {
        fauxFocusSlider.fillAmount = Mathf.MoveTowards(fauxFocusSlider.fillAmount, targetFocus, reducingSpeed * Time.deltaTime);
        fauxHealthSlider.fillAmount = Mathf.MoveTowards(fauxHealthSlider.fillAmount, targetHealth, reducingSpeed * Time.deltaTime);
        fauxShieldSlider.fillAmount = Mathf.MoveTowards(fauxShieldSlider.fillAmount, targetShield, reducingSpeed * Time.deltaTime);
    }
}
