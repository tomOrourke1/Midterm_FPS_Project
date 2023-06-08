using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverFocusBehaviour : MonoBehaviour
{
    [Header("----- Components ------")]
    [SerializeField] TelekinesisController tele;
    [SerializeField] fingerGun gun;
    [SerializeField] Player player;

    [Header("----- values ------")]
    [SerializeField] float timeToRegen;
    [SerializeField] float amountOverTime;

    float timer;


    private void Start()
    {
        gameManager.instance.playerscript.OnFocusUpdate += SetTime;
    }

    private void Update()
    {
        if (!gameManager.instance.playerscript.AtMaxFocus() && (Time.time - timer) > timeToRegen )
        {
            gameManager.instance.playerscript.AddFocus(amountOverTime * Time.deltaTime);
            gameManager.instance.pStatsUI.UpdateFocus();
        }
    }

    void SetTime(float amt)
    {
        if(amt < 0 )
        {
            timer = Time.time;
        }
    }

}
