using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashDamage : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] Image flashDamage;
    [SerializeField] Image flashShield;
    [SerializeField] Image flashCrack;

    [Header("Images")]
    [SerializeField] AnimationCurve flashSpeedCurve;
    [SerializeField] float showDuration;

    private float tmpFlashHP;
    private float tmpFlashSH;
    private float tmpFlashCR;

    private bool currentlyFlashingDamage;
    private bool currentlyFlashingShield;
    private bool currentlyFlashingCrack;

    // =================================================================
    //          PLEASE DON'T TOUCH ANY OF THE WHILE LOOPS!
    // =================================================================

    // Start is called before the first frame update
    void Start()
    {
        tmpFlashHP = showDuration;
        tmpFlashSH = showDuration;
        tmpFlashCR = showDuration;

        flashDamage.gameObject.SetActive(false);
        flashShield.gameObject.SetActive(false);
        flashCrack.gameObject.SetActive(false);
    }

    public IEnumerator FlashDamageDisplay()
    {
        if (!currentlyFlashingDamage)
        {
            currentlyFlashingDamage = true;

            flashDamage.gameObject.SetActive(true);

            // While the timer is above a 'second'
            while (tmpFlashHP > 0f)
            {
                // Add the Time.deltatime (interval in seconds from last frame to current frame) to the timer
                tmpFlashHP -= Time.deltaTime;
                // Evaluate the curve and then set that the alpha's amount
                float alphaColorFadeIn = flashSpeedCurve.Evaluate(tmpFlashHP);
                // Set the image color component to a new color of an decreased alpha
                flashDamage.color = new Color(flashDamage.color.r, flashDamage.color.g, flashDamage.color.b, alphaColorFadeIn);
                yield return 0;
            }

            tmpFlashHP = showDuration;
            flashDamage.gameObject.SetActive(false);
            currentlyFlashingDamage = false;
        }
        else
        {
            tmpFlashHP += Time.deltaTime;
        }
    }

    public IEnumerator FlashShieldDisplay()
    {
        if (!currentlyFlashingShield)
        {
            currentlyFlashingShield = true;

            flashShield.gameObject.SetActive(true);

            // While the timer is above a 'second'
            while (tmpFlashSH > 0f)
            {
                // Add the Time.deltatime (interval in seconds from last frame to current frame) to the timer
                tmpFlashSH -= Time.deltaTime;
                // Evaluate the curve and then set that the alpha's amount
                float alphaColorFadeIn = flashSpeedCurve.Evaluate(tmpFlashSH);
                // Set the image color component to a new color of an decreased alpha
                flashShield.color = new Color(flashShield.color.r, flashShield.color.g, flashShield.color.b, alphaColorFadeIn);
                yield return 0;
            }

            tmpFlashSH = showDuration;
            flashShield.gameObject.SetActive(false);
            currentlyFlashingShield = false;
        }
        else
        {
            tmpFlashSH += Time.deltaTime;
        }
    }

    public IEnumerator CrackShieldDisplay()
    {
        if (!currentlyFlashingCrack)
        {
            currentlyFlashingCrack = true;

            flashCrack.gameObject.SetActive(true);

            // While the timer is above a 'second'
            while (tmpFlashSH > 0f)
            {
                // Add the Time.deltatime (interval in seconds from last frame to current frame) to the timer
                tmpFlashCR -= Time.deltaTime;
                // Evaluate the curve and then set that the alpha's amount
                float alphaColorFadeIn = flashSpeedCurve.Evaluate(tmpFlashCR);
                // Set the image color component to a new color of an decreased alpha
                flashCrack.color = new Color(flashCrack.color.r, flashShield.color.g, flashDamage.color.b, alphaColorFadeIn);
                yield return 0;
            }

            tmpFlashCR = showDuration;
            flashCrack.gameObject.SetActive(false);
            currentlyFlashingCrack = false;
        }
        else
        {
            tmpFlashCR += Time.deltaTime;
        }
    }

    //private IEnumerator CrackNShow(Image image, float r, float g, float b, float a)
    //{
    //    image.gameObject.SetActive(true);
    //    image.color = new Color(r, g, b, a);

    //    // While the timer is above a 'second'
    //    while (showDuration > 0f)
    //    {
    //        // Add the Time.deltatime (interval in seconds from last frame to current frame) to the timer
    //        showDuration -= Time.deltaTime;
    //        // Evaluate the curve and then set that the alpha's amount
    //        float alphaColorFadeIn = flashSpeedCurve.Evaluate(showDuration);
    //        // Set the image color component to a new color of an decreased alpha
    //        image.color = new Color(r, g, b, alphaColorFadeIn);
    //        yield return 0;
    //    }

    //    showDuration = tempShowDuration;
    //    image.gameObject.SetActive(false);
    //}
}
