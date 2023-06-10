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
    }

}
