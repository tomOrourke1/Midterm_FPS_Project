using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyTimeManager : MonoBehaviour
{


    public delegate void funcc();
    public event funcc func;

    Dictionary<string, (float, funcc, Coroutine)> timers;

    private void Start()
    {
        timers = new();
    }


    public void AddTimer(string key, float value, funcc func)
    {
        if (timers.ContainsKey(key))
            return;

        var c = StartCoroutine(Timer(key));

        timers.Add(key, (value,func, c));
    }

    IEnumerator Timer(string key)
    {
        if(timers.ContainsKey(key))
        {
            var keys = timers[key];
            yield return new WaitForSeconds(keys.Item1);
            keys.Item2();
            timers.Remove(key);
        }
    }

    public void StopTimer(string key)
    {
        if(timers.ContainsKey(key))
        {
            var c = timers[key];

            StopCoroutine(c.Item3);
        }
    }

    public void StopAllTimers()
    {
        StopAllCoroutines();
    }





}
