using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentSwap_MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform parentPlatform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;
        other.transform.SetParent(parentPlatform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
            return;

        other.transform.SetParent(null);

        IApplyVelocity applyVelocity = other.GetComponent<IApplyVelocity>();

        applyVelocity?.ApplyVelocity(parentPlatform.gameObject.GetComponent<MovingPlatform>().GetCurrentVelocity());

        //Debug.LogError(parentPlatform.gameObject.GetComponent<MovingPlatform>().GetCurrentVelocity());
    }
}
