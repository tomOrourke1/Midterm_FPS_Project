using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineActivate : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] GameObject displayUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            obj.SetActive(true);
            displayUI.SetActive(false);
            InputManager.Instance.Input.Disable();
        }
    }
}
