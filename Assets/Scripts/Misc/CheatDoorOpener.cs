using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheatDoorOpener : MonoBehaviour
{
    [System.Serializable]
    enum Keyyy
    {
        t,
        period
    }

    [SerializeField] Keyyy k;
    
    private void Update()
    {



        if(Keyboard.current != null)
        {
            bool p = false;

            switch (k)
            {
                case Keyyy.t:
                    p = Keyboard.current.tKey.wasPressedThisFrame;
                    break;
                case Keyyy.period:
                    p = Keyboard.current.numpadPeriodKey.wasPressedThisFrame;
                    break;
                default:
                    break;
            }


            var cam = Camera.main;
            if(cam != null && p)
            {
                var doHit = Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit,  10f);
                if(doHit)
                {
                    var interact = hit.collider.GetComponentInParent<DoorScript>();
                    if(interact != null )
                    {
                        interact.SetLockStatus(false);
                        interact.OpenDoor();
                    }
                }
            }
        }   
    }



}
