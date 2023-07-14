using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class StatVisualizer : MonoBehaviour
{
    [Header("Timers")]
    [SerializeField] float reducingTime = 0.1f;
    [SerializeField] float speedUpRate = 0.2f;
    [SerializeField] float waitTime = 2f;

    [Header("Faux Sliders")]
    [Tooltip("Faux Focus image to alter.")]
    [SerializeField] Image fauxFocusSlider;
    [Tooltip("Faux Shield image to alter.")]
    [SerializeField] Image fauxShieldSlider;
    [Tooltip("Faux Health image to alter.")]
    [SerializeField] Image fauxHealthSlider;
    
    [Header("Real Sliders")]
    [Tooltip("Real Focus image to alter.")]
    [SerializeField] Image realFocusSlider;
    [Tooltip("Real Health image to alter.")]
    [SerializeField] Image realHealthSlider;
    [Tooltip("Real Shield image to alter.")]
    [SerializeField] Image realShieldSlider;

    bool runFocusUpdate, runHealthUpdate, runShieldUpdate;
    float currentFocusTimer, currentShieldTimer, currentHealthTimer;
    float breakShieldTimer;

    private void Start()
    {
        runFocusUpdate = runHealthUpdate = runShieldUpdate = false;
        currentFocusTimer = currentShieldTimer = currentHealthTimer = waitTime;
        breakShieldTimer = reducingTime;
    }

    // Updates the sliders constantly. There can be a different way to check this. LMK - Kev
    private void Update()
    {
        UpdateSliderBools();
        UpdateFocus();
        UpdateShield();
        UpdateHealth();
    }

    public void UpdateSliderBools()
    {
        if (fauxFocusSlider.fillAmount > realFocusSlider.fillAmount)
        {
            runFocusUpdate = true;
        }
        else if (fauxFocusSlider.fillAmount <= realFocusSlider.fillAmount)
        {
            fauxFocusSlider.fillAmount = realFocusSlider.fillAmount;
            currentFocusTimer = waitTime;
        }

        if (fauxHealthSlider.fillAmount > realHealthSlider.fillAmount)
        {
            runHealthUpdate = true;
        }
        else if (fauxHealthSlider.fillAmount <= realHealthSlider.fillAmount)
        {
            fauxHealthSlider.fillAmount = realHealthSlider.fillAmount;
            currentHealthTimer = waitTime;
        }

        if (fauxShieldSlider.fillAmount > realShieldSlider.fillAmount)
        {
            runShieldUpdate = true;
        }
        else if (fauxShieldSlider.fillAmount <= realShieldSlider.fillAmount)
        {
            fauxShieldSlider.fillAmount = realShieldSlider.fillAmount;
            currentShieldTimer = waitTime;
            breakShieldTimer = reducingTime;
        }
    }

    private void UpdateFocus()
    {
        if (runFocusUpdate)
        {
            if (currentFocusTimer > 0)
            {
                currentFocusTimer -= Time.deltaTime;
            }
            else
            {
                currentFocusTimer = 0;
                runFocusUpdate = false;
                fauxFocusSlider.fillAmount = Mathf.MoveTowards(fauxFocusSlider.fillAmount, realFocusSlider.fillAmount, reducingTime * Time.deltaTime);
            }
        }
    }

    private void UpdateHealth()
    {
        if (runHealthUpdate)
        {
            if (currentHealthTimer > 0)
            {
                currentHealthTimer -= Time.deltaTime;
            }
            else
            {
                currentHealthTimer = 0;
                runHealthUpdate = false;
                fauxHealthSlider.fillAmount = Mathf.MoveTowards(fauxHealthSlider.fillAmount, realHealthSlider.fillAmount, reducingTime * Time.deltaTime);
            }
        }
    }

    private void UpdateShield()
    {
        if (runShieldUpdate)
        {
            if (currentShieldTimer > 0)
            {
                currentShieldTimer -= Time.deltaTime;
            }
            else
            {
                currentShieldTimer = 0;
                runShieldUpdate = false;
                fauxShieldSlider.fillAmount = Mathf.MoveTowards(fauxShieldSlider.fillAmount, realShieldSlider.fillAmount, breakShieldTimer * Time.deltaTime);
            }
        }
    }

    

    public void UpdtateShieldDepleationTimer()
    {
        breakShieldTimer = speedUpRate;
    }
}
