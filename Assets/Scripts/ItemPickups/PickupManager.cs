using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField] ShrinkAndDelete shrinkScript;
    [SerializeField] PickupSFX sfxScript;
    [SerializeField] InteractFix interact;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sfxScript.Play_OneShot();
            shrinkScript.Shrink();
            interact?.EnableInteract();
        }
    }
}
