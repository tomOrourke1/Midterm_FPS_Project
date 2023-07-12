using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineActivate : MonoBehaviour
{
    [SerializeField] GameObject obj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            obj.SetActive(true);
        }
    }
}
