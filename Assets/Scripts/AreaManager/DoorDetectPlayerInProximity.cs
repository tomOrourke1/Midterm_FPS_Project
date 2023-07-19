using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DoorDetectPlayerInProximity : MonoBehaviour, IEnvironment
{
    [SerializeField] DoorScript door;
    [SerializeField] BoxCollider boxCol;

    AreaManager manager;

    int count;
    bool transitionMode = false;


    int initialCount;

    int lastCount;

    bool Closing;
    bool Opening;

    private void Awake()
    {
        door = transform.parent.GetComponent<DoorScript>();
        Closing = false;
        Opening = false;
    }

    private void Start()
    {
        initialCount = GetOverlap().Length;
    }

    private void Update()
    {

        //if (!door.GetOpenStatus() && !Closing)
        //    return;

        if (!door.GetOpenStatus())
        {
            Closing = false;
        }
        else
        {
            Opening = false;
        }

        var h = GetOverlap();
        count = h.Length - initialCount;

        bool playerHere = false;

        foreach (var element in h)
        {
            if (blackList(element))
            {
                count--;
            }
            else if (element.CompareTag("Player"))
            {
                playerHere = true;
            }
        }

        if (transitionMode && playerHere)
        {
            GameManager.instance.SetCurrentRoomManager(manager);

            transitionMode = false;
        } else if (transitionMode && !playerHere)
        {
            return;
        }

        if (count > 0 && !Opening)
        {
            if (transitionMode)
            {
                
            }

            door.OpenDoor();
            Opening = true;
        }

        if (count <= 0 && lastCount != 0 && !Closing)
        {
            door.CloseDoor();
            Closing = true;
        }


        lastCount = count;
        //count = initialCount;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.isTrigger || (transitionMode && !other.CompareTag("Player")))
    //        return;

    //    if (transitionMode)
    //    {
    //        GameManager.instance.SetCurrentRoomManager(manager);

    //        transitionMode = false;
    //    }

    //    if (blackList(other))
    //    {
    //        return;
    //    }

    //    // This function has been remove in favor of Key Terminals

    //    //if (door.GetLockedStatus())
    //    //{
    //    //    bool player = other.CompareTag("Player");
    //    //    if (player)
    //    //    {
    //    //        if (GameManager.instance.GetKeyChain().GetKeys() > 0)
    //    //        {
    //    //            door.SetLockStatus(false);
    //    //            GameManager.instance.GetKeyChain().removeKeys(1);
    //    //        }
    //    //    }
    //    //}

    //    if (count == 0)
    //    {
    //        door.OpenDoor();
    //    }

    //    count++;
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (blackList(other) || other.isTrigger)
    //    {
    //        return;
    //    }

    //    count--;
    //    count = Mathf.Max(count, 0);
    //    if (count == 0)
    //    {
    //        door.CloseDoor();
    //    }
    //}

    Collider[] GetOverlap()
    {

        var mult = VecMult(transform.localScale, boxCol.center);
        var pos = transform.position + (transform.forward * mult.z + transform.right * mult.x + transform.up * mult.y);

        var size = VecMult(transform.localScale, boxCol.size);
        return Physics.OverlapBox(pos, size / 2, transform.localRotation);
    }

    Vector3 VecMult(Vector3 v, Vector3 v2)
    {
        Vector3 value;
        value.x = v.x * v2.x;
        value.y = v.y * v2.y;
        value.z = v.z * v2.z;
        return value;
    }

    public void EnterTransitionMode(AreaManager manager)
    {
        transitionMode = true;
        this.manager = manager;
    }

    public void StartObject()
    {

        count = 0;
    }

    public void StopObject()
    {

    }

    bool blackList(Collider other)
    {
        return other.CompareTag("Enemy Bullet") || other.CompareTag("PlayerSpecial") || other.CompareTag("Enemy");
    }
}
