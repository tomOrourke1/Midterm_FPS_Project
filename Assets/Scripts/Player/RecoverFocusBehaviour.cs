using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverFocusBehaviour : MonoBehaviour
{
    [Header("----- Components ------")]
    [SerializeField] TelekinesisController tele;
    [SerializeField] fingerGun gun;
    [SerializeField] PlayerResources playerResources;

    [Header("----- values ------")]
    [SerializeField] float timeToRegen;
    [SerializeField] float amountOverTime;

    float timer;


    private void OnEnable()
    {
        playerResources.Focus.OnResourceDecrease += SetTime;
    }
    private void OnDisable()
    {
        playerResources.Focus.OnResourceDecrease -= SetTime;
    }

    private void Update()
    {
        if(!playerResources.Focus.AtMax() && (Time.time - timer) > timeToRegen)
        {
            playerResources.AddFocus(amountOverTime * Time.deltaTime);
        }

    }

    void SetTime()
    {
        timer = Time.time;
    }

}
