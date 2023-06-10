using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource : MonoBehaviour
{

    public event System.Action OnResourceDepleted;

    [SerializeField] float maxValue;

    float curValue;

    public float CurrentValue => curValue;
    public float MaxValue => maxValue;


    public void Decrease(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0);
        if(curValue == 0)
        {
            OnResourceDepleted?.Invoke();
        }

    }

    public void Increase(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    public void FillToMax()
    {
        curValue = maxValue;
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
}
