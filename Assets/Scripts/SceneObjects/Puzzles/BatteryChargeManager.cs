using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryChargeManager : MonoBehaviour
{
    [SerializeField] GameObject Powered;
    [SerializeField] GameObject unPowered;

    public void UpdateChargeLevel(float maxCharge, float currCharge)
    {
        if (currCharge <= 0)
        {
            Powered.SetActive(false);
        }
        else
        {
            Powered.SetActive(true);
        }

        float y_size = unPowered.transform.localScale.y * (currCharge / maxCharge);

        float x_size = Powered.transform.localScale.x;
        float z_size = Powered.transform.localScale.z;

        Powered.transform.localScale = new Vector3(x_size, y_size, z_size);
    }

}
