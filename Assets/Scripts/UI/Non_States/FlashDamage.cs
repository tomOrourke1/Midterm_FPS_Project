using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashDamage : MonoBehaviour
{    
    [Header("Images")]
    [SerializeField] Image flashDamage;
    [SerializeField] Image flashShield;
    [SerializeField] Image screenCrack;

    [Header("Images")]
    [SerializeField] AnimationCurve flashSpeedCurve;
    [SerializeField] float showDuration;

    private float tempShowDuration;

    // =================================================================
    //          PLEASE DON'T TOUCH ANY OF THE WHILE LOOPS!
    // =================================================================

    // Start is called before the first frame update
    void Start()
    {
        tempShowDuration = showDuration;

        flashDamage.gameObject.SetActive(false);
        flashShield.gameObject.SetActive(false);
        screenCrack.gameObject.SetActive(false);
    }

    public IEnumerator FlashDamageDisplay()
    {
        StartCoroutine(CrackNShow(flashDamage, flashDamage.color.r, flashDamage.color.g, flashDamage.color.b, flashDamage.color.a));
        yield return 0;
    }

    public IEnumerator FlashShieldDisplay()
    {
        StartCoroutine(CrackNShow(flashShield, flashShield.color.r, flashShield.color.g, flashShield.color.b, flashShield.color.a));;
        yield return 0;
    }

    public IEnumerator CrackShieldDisplay()
    {
        StartCoroutine(CrackNShow(screenCrack, screenCrack.color.r, screenCrack.color.g, screenCrack.color.b, screenCrack.color.a));
        yield return 0;


        //flashShield.gameObject.SetActive(true);
        //screenCrack.gameObject.SetActive(true);
        //yield return new WaitForSeconds(0.2f);

        //float fadeOutCrack = 0;
        //while (fadeOutCrack <= 0.5f)
        //{
        //    fadeOutCrack += Time.deltaTime;
        //    screenCrack.color = new Color(0f, 0f, 0f, fadeOutCrack / 0.5f);
        //}
        //flashShield.gameObject.SetActive(false);
        //screenCrack.gameObject.SetActive(false);
        //screenCrack.color = new Color(255f, 255f, 255f, 255f);
    }

    private IEnumerator CrackNShow(Image image, float r, float g, float b, float a)
    {
        image.gameObject.SetActive(true);
        image.color = new Color(r, g, b, a);

        // While the timer is above a 'second'
        while (showDuration > 0f)
        {
            // Add the Time.deltatime (interval in seconds from last frame to current frame) to the timer
            showDuration -= Time.deltaTime;
            // Evaluate the curve and then set that the alpha's amount
            float alphaColorFadeIn = flashSpeedCurve.Evaluate(showDuration);
            // Set the image color component to a new color of an decreased alpha
            image.color = new Color(r, g, b, alphaColorFadeIn);
            yield return 0;
        }

        showDuration = tempShowDuration;
        image.gameObject.SetActive(false);
    }
}
