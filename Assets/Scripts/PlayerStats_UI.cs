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

    // So we can find what each stat is easily
    [SerializeField] Player playerScriptRef;

    // Start is called before the first frame update
    void Start()
    {

        
        playerScriptRef = gameManager.instance.playerscript;


        maxFocus = playerScriptRef.GetPlayerMaxHP();
        maxHP = playerScriptRef.GetPlayerMaxHP();
        currentHP = playerScriptRef.GetPlayerCurrentHP();
        currentFocus = playerScriptRef.GetPlayerCurrentFocus();

        // Update when Shield is added to the Player script
        currentShield = maxHP;

        UpdateFocus();
        UpdateShield();
        UpdateHealth();
    }

    // When the damage and HP refilling are added introduce these into those functions. 
    public void UpdateValues()
    {
        if (playerScriptRef == null)
            playerScriptRef = gameManager.instance.playerscript;

        UpdateFocus();
        UpdateShield();
        UpdateHealth();
    }

    public void UpdateFocus()
    {
        focusSlider.fillAmount = playerScriptRef.GetPlayerCurrentFocus() / maxFocus;
    }
    public void UpdateShield()
    {
        shieldSlider.fillAmount = currentShield / maxShield;
    }
    public void UpdateHealth()
    {
        healthSlider.fillAmount = playerScriptRef.GetPlayerCurrentHP() / maxHP;
    }
}
