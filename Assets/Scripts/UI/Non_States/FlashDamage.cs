using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashDamage : MonoBehaviour
{    
    [Header("Popup Images")]
    [SerializeField] Image flashDamage;
    [SerializeField] Image flashShield;
    [SerializeField] Image screenCrack;

    // Start is called before the first frame update
    void Start()
    {
        flashDamage.gameObject.SetActive(false);
        flashShield.gameObject.SetActive(false);
        screenCrack.gameObject.SetActive(false);
    }

    public IEnumerator FlashDamageDisplay()
    {
        flashDamage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        flashDamage.gameObject.SetActive(false);
    }

    public IEnumerator FlashShieldDisplay()
    {
        flashShield.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        flashShield.gameObject.SetActive(false);
    }

    public IEnumerator CrackShieldDisplay()
    {
        flashShield.gameObject.SetActive(true);
        screenCrack.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        float fadeOutCrack = 0;
        while (fadeOutCrack <= 0.5f)
        {
            fadeOutCrack += Time.deltaTime;
            screenCrack.color = new Color(0f, 0f, 0f, fadeOutCrack / 0.5f);
        }
        flashShield.gameObject.SetActive(false);
        screenCrack.gameObject.SetActive(false);
        screenCrack.color = new Color(255f, 255f, 255f, 255f);
    }
}
