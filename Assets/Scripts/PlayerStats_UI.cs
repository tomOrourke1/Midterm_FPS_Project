using UnityEngine;
using UnityEngine.UI;

public class PlayerStats_UI : MonoBehaviour
{
    [Header("Player Stat Fillers")]
    [Tooltip("Focus image to alter.")]
    [SerializeField] Image focusSlider;
    [Tooltip("Shield image to alter.")]    
    [SerializeField] Image shieldSlider;
    [Tooltip("Health image to alter.")]
    [SerializeField] Image healthSlider;

    // C# has inherent private protection but defining it just to be safe
    private float maxFocus;
    private float maxShield;
    private float maxHP;
    private float currentHP;
    private float currentFocus;
    private float currentShield;

    void Start()
    {

        maxFocus = gameManager.instance.playerscript.GetPlayerMaxHP();
        maxHP = gameManager.instance.playerscript.GetPlayerMaxHP();
        maxShield = gameManager.instance.playerscript.GetPlayerMaxShield();
        currentHP = gameManager.instance.playerscript.GetPlayerCurrentHP();
        currentFocus = gameManager.instance.playerscript.GetPlayerCurrentFocus();
        currentShield = gameManager.instance.playerscript.GetPlayerCurrentShield();

        UpdateValues();
    }

    // When the damage and HP refilling are added introduce these into those functions. 
    public void UpdateValues()
    {
        UpdateFocus();
        UpdateShield();
        UpdateHealth();
    }

    public void UpdateFocus()
    {
        focusSlider.fillAmount = gameManager.instance.playerscript.GetPlayerCurrentFocus() / maxFocus;
    }
    public void UpdateShield()
    {
        shieldSlider.fillAmount = gameManager.instance.playerscript.GetPlayerCurrentShield() / maxShield;
    }
    public void UpdateHealth()
    {
        healthSlider.fillAmount = gameManager.instance.playerscript.GetPlayerCurrentHP() / maxHP;
    }
}
