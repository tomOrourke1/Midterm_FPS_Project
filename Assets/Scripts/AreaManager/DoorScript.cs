using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour, IEnvironment
{
    [SerializeField] Transform doorPivot;

    [SerializeField] bool locked;
    [SerializeField] float doorSpeed;

    [Header("Lock Color Changing")]
    [SerializeField] MeshRenderer rendererR;
    [SerializeField] Material doorEnabled;
    [SerializeField] Material doorDisabled;

    [SerializeField] DoorSFX doorSFX;

    bool isOpen = false;
    [SerializeField] bool activation;
    float doorValue;

    const float minDoorValue = 0.01f;

    // Added these to store initial door stats
    bool initialDoorOpen;
    bool initialLock;
    float initialDoorValue;

    bool LockClose;

    bool lockSFXPlayed;

    enum doorState
    {
        opening,
        closing,
        open,
        closed
    }

    doorState d;


    private void Start()
    {
        isOpen = doorPivot.localScale.x == minDoorValue;
        doorValue = isOpen ? minDoorValue : 1;
        initialDoorValue = doorValue;

        // Saves the intial values of the door for reset
        initialDoorOpen = isOpen;
        initialLock = locked;

        d = initialDoorOpen ? doorState.open : doorState.closed;

        // Added by Kevin for changing door material color depending on lock status
        rendererR.material = locked ? doorDisabled : doorEnabled;

        LockClose = false;
    }

    private void Activate()
    {
        if(!locked)
        {
            if(activation)
            {
                isOpen = !isOpen;
            }
            activation = true;
            
        }
    }

    public void CloseLockedDoor() { LockClose = true; }

    public void SetLockStatus(bool locked)
    {
        this.locked = locked;

        // Added to set the color of the door depending on lock status
        rendererR.material = locked ? doorDisabled : doorEnabled;

        if (!locked & !lockSFXPlayed)
        {
            doorSFX.PlayDoor_Unlock();
            lockSFXPlayed = true;
        }

    }

    private void Update()
    {
        if(activation && !locked)
        {
            InvertDoorState();
        } else if (LockClose)
        {
            InvertDoorState();
        }
    }

    void InvertDoorState()
    {
        // If door is open, endVal = 1 else endVal = minDoorValue
        var endVal = isOpen ? 1 : minDoorValue;

        // Moves the door towards endVal
        doorValue = Mathf.MoveTowards(doorValue, endVal, Time.deltaTime * doorSpeed);
        doorPivot.localScale = new Vector3(doorValue, 1, 1);

        d = endVal == 1 ? doorState.closing : doorState.opening;

        // Inverts door's state
        if (doorValue == endVal)
        {
            isOpen = !isOpen;
            activation = false;

            d = isOpen ? doorState.open : doorState.closed;

            if (LockClose)
            {
                LockClose = false;
            }
        }
    }

    public void CloseDoor()
    {

        if (isOpen || d == doorState.opening)
        {
            Activate();
            doorSFX.PlayDoor_Close();
        }
    }

    public void OpenDoor()
    {
        if (!isOpen || d == doorState.closing)
        {
            Activate();
            doorSFX.PlayDoor_Open();
        }
    }

    public bool GetOpenStatus()
    {
        return isOpen;
    }

    public bool GetLockedStatus()
    {
        return locked;
    }

    void ResetDoorPos()
    {
        var originalPos = initialDoorOpen ? minDoorValue : 1;

        doorValue = originalPos;
        doorPivot.localScale = new Vector3(initialDoorValue, 1, 1);

        isOpen = initialDoorOpen;
        activation = false;


    }

    public void StartObject()
    {
        if (isOpen != initialDoorOpen)
        {
            ResetDoorPos();
        }

        SetLockStatus(initialLock);

        // Added to reset the original color back
        rendererR.material = locked ? doorDisabled : doorEnabled;
    }

    public void StopObject()
    {
        // Nothing needs to happen here
    }
}
