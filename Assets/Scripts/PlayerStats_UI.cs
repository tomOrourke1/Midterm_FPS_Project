using UnityEngine;
using UnityEngine.UI;

public class PlayerStats_UI : MonoBehaviour
{
    [Header("Player Stat Fillers")]
    [Tooltip("Focus image to alter.")]
    [SerializeField] Slider focusSlider;
    [Tooltip("Shield image to alter.")]    
    [SerializeField] Slider shieldSlider;
    [Tooltip("Health image to alter.")]
    [SerializeField] Slider healthSlider;

    [Header("Temporary Stats")]
    [SerializeField] public float maxFocus;
    [SerializeField] public float maxShield;
    [SerializeField] public float maxHP;
    [SerializeField] public float currentFocus;
    [SerializeField] public float currentShield;
    [SerializeField] public float currentHP;

    // Start is called before the first frame update
    void Start()
    {
        currentFocus = maxFocus;
        currentShield = maxShield;
        currentHP = maxHP;

        UpdateFocus();
        UpdateShield();
        UpdateHealth();
    }

    public void UpdateFocus()
    {
        focusSlider.value = currentFocus / maxFocus;
    }
    public void UpdateShield()
    {
        shieldSlider.value = currentShield / maxShield;
    }
    public void UpdateHealth()
    {
        healthSlider.value = currentHP / maxHP;
    }
}
