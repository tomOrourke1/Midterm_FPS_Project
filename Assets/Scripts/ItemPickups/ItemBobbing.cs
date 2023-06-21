using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBobbing : MonoBehaviour
{
    [Header("Values")]
    [SerializeField, Range(0, 5)] float bobRate = 1;
    [SerializeField, Range(0,2)] float amplitude = 0.25f;
    
    Vector3 startingPos;
    float tempNum;

    private void Start()
    {
        startingPos = transform.position;
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, startingPos.y + Mathf.Sin(Time.time * bobRate) * amplitude, transform.position.z);
    }
}
