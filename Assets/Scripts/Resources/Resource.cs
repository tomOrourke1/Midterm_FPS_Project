using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Resource
{

    public event System.Action OnResourceDepleted;
    public event System.Action OnResourceDecrease;
    public event System.Action OnResourceIncrease;

    [SerializeField] float maxValue;

    float curValue;

    public float CurrentValue => curValue;
    public float MaxValue => maxValue;


    bool depleated = false;

    public void Decrease(float amount)
    {
        if (depleated)
            return;

        curValue = Mathf.Max(curValue - amount, 0);
        OnResourceDecrease?.Invoke();
        if(curValue == 0)
        {
            OnResourceDepleted?.Invoke();
            depleated = true;
        }

    }

    public void Increase(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
        OnResourceIncrease?.Invoke();
        depleated = false;
    }

    public void FillToMax()
    {
        curValue = maxValue;
        depleated = false;
    }

    public bool SpendResource(float amount)
    {
        bool validOperation = curValue >= amount;
        if (validOperation)
        {
            Decrease(amount);
        }
        return validOperation;
    }


    public void SetMaxValue(float amount)
    {
        maxValue = Mathf.Max(amount, 1);
        curValue = maxValue < curValue ? maxValue : curValue;
    }

    public float GetPercent()
    {
        return curValue / maxValue;
    }

    public bool AtMax()
    {
        return curValue == maxValue;
    }

    public bool AtMin()
    {
        return curValue == 0;
    }
}
