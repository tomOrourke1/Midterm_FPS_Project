using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChargeableBattery : MonoBehaviour, IEnvironment
{
    [Header("Battery")]
    [SerializeField] float MaxCharge;
    [SerializeField] BatteryChargeManager batteryChargeManager;

    [Header("Charge Rate")]
    [SerializeField] float ChargeRate;
    [SerializeField] float ChargeLossRate;

    [Header("Wait")]
    [SerializeField] float waitDuration;

    [Header("Events")]
    [SerializeField] UnityEvent WhenPowered;
    [SerializeField] UnityEvent OnPowerDown;

    float currCharge = 0;
    float waitTime = 0;

    bool charging = false;

    bool Closed = true;

    void Update()
    {
        batteryChargeManager.UpdateChargeLevel(MaxCharge, currCharge);

        if (!charging)
        {
            RemoveCharge();
            if (currCharge <= 0 && !Closed)
            {
                OnPowerDown.Invoke();
                Closed = true;
            }
        }
        else
        {
            waitTime += Time.deltaTime;
            Mathf.Clamp(waitTime, 0, waitDuration);

            if (waitTime >= waitDuration)
            {
                charging = false;

            }

            if (Closed)
            {
                WhenPowered.Invoke();
                Closed = false;
            }
        }
    }

    void RemoveCharge()
    {
        if (currCharge >= 0)
        {
            currCharge -= ChargeLossRate * Time.deltaTime;
            Mathf.Clamp(currCharge, 0, MaxCharge);
        }
    }

    public void AddCharge()
    {
        charging = true;

        if (currCharge < MaxCharge)
        {
            currCharge += ChargeRate * Time.deltaTime;
            Mathf.Clamp(currCharge, 0, MaxCharge);
        }

        waitTime = 0;
    }

    public float GetMaxCharge() { return MaxCharge; }

    public void StopObject()
    {
        
    }

    public void StartObject()
    {
        Closed = true;
        charging = false;
        currCharge = 0;
        waitTime = 0;
    }
}
