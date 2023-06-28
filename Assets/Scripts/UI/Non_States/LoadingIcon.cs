using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingIcon : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Image brighten;
    [SerializeField] float rate;

    public void WakeUp()
    {
        brighten.gameObject.SetActive(false);
        icon.fillAmount = 0;
    }

    private void Update()
    {
        if (icon.fillAmount != 1)
        {
            icon.fillAmount = Mathf.MoveTowards(icon.fillAmount, 1f, rate * Time.deltaTime);
        }

        if (icon.fillAmount == 1)
        {
            StartCoroutine(ShutOff());
        }
    }

    IEnumerator ShutOff()
    {
        brighten.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        brighten.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
