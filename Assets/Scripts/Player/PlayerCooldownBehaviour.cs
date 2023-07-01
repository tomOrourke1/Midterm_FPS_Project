using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCooldownBehaviour : MonoBehaviour
{




    // this needs to store cooldowns and charges
    // that way we can expend charges and have cooldowns work



    [SerializeField] float chargeTime;



    float time;

    private void Start()
    {
        time = chargeTime;
    }

    private void Update()
    {
        if(time <= chargeTime)
        {
            time += Time.deltaTime;
        }
    }




    // this will need to return weather or not it is activatable.
    public bool CanActivate()
    {

        var active = time >= chargeTime;

        if(active)
        {
            time = 0;
        }

        return active;
    }


    public void ResetCooldown()
    {
        time = chargeTime;
    }





   
}
