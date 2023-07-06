using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryChargeManager : MonoBehaviour
{
    [SerializeField] List<GameObject> chargeLevels = new List<GameObject>();

    [SerializeField] Material inateMaterial = null;
    [SerializeField] Material ChargedMaterial = null;

    public void UpdateChargeLevel(float maxCharge, float currCharge)
    {
        /*
         
            0 = 25%
            1 = 50%
            2 = 75%
            3 = 100%

         */

        int chargeLevel = 0;

        if (currCharge >= maxCharge)
        {
            chargeLevel = 4;
        } 
        else if (currCharge >= maxCharge * .75)
        {
            chargeLevel = 3;
        }
        else if (currCharge >= maxCharge * .5)
        {
            chargeLevel = 2;
        }
        else if (currCharge >= maxCharge * .25)
        {
            chargeLevel = 1;
        }

        for (int i = 0; i < chargeLevels.Count; i++)
        {
            if (i + 1 <= chargeLevel)
            {
                chargeLevels[i].GetComponent<MeshRenderer>().material = ChargedMaterial;
            }
            else
            { 
                chargeLevels[i].GetComponent<MeshRenderer>().material = inateMaterial;
            }
        }
    }

}
