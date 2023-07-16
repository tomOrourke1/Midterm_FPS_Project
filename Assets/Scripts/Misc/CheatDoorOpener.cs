using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheatDoorOpener : MonoBehaviour
{



    private void Update()
    {
        if(Keyboard.current != null && Keyboard.current.tKey.wasPressedThisFrame)
        {
            var cam = Camera.main;
            if(cam != null )
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
