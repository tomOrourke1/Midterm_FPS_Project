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

    [Header("Icons")]
    [Tooltip("The health icon to enable when the shield is empty.")]
    [SerializeField] Image healthIcon;
    [Tooltip("The shield icon to enable when the shield is empty.")]
    [SerializeField] Image shieldIcon;
    [SerializeField] StatVisualizer sliderScript;

    [Header("Kinesis Icon")]
    [SerializeField] Image kinesisIcon;

    // C# has inherent private protection but defining it just to be safe
    PlayerResources instance;

    void Start()
    {
        instance = GameManager.instance.GetPlayerResources();
        if (kinesisIcon.sprite == null)
            kinesisIcon.enabled = false;
    }

    // When the damage and HP refilling are added introduce these into those functions. 
    public void UpdateValues()
    {
        if (instance == null)
        {
            instance = GameManager.instance.GetPlayerResources();
        }

        focusSlider.fillAmount = instance.Focus.GetPercent();
        shieldSlider.fillAmount = instance.Shield.GetPercent();
        healthSlider.fillAmount = instance.Health.GetPercent();

        sliderScript.UpdateSliderBools();
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
    /// Returns the health slider that updates for the current hp amount;
    /// </summary>
    /// <returns></returns>
    public Image GetRealHealthSlider()
    {
        return healthSlider;
    }

    /// <summary>
    /// Returns the shield slider that updates for the current shield amount;
    /// </summary>
    /// <returns></returns>
    public Image GetRealShieldSlider()
    {
        return shieldSlider;
    }

    /// <summary>
    /// Returns the focus slider that updates for the current focus amount;
    /// </summary>
    /// <returns></returns>
    public Image GetRealFocusSlider()
    {
        return focusSlider;
    }

    public void SetKinesisIcon(Sprite refImage)
    {
        kinesisIcon.sprite = refImage;
        kinesisIcon.enabled = true;
    }
}
