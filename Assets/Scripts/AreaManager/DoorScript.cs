using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour, IDoorActivator, IEnvironment
{
    [SerializeField] Transform doorPivot;

    [SerializeField] bool locked;
    [SerializeField] float doorSpeed;
    [SerializeField] MeshRenderer renderer;
    [SerializeField] Material doorEnabled;
    [SerializeField] Material doorDisabled;

    bool isOpen = false;
    [SerializeField] bool activation;
    float doorValue;

    const float minDoorValue = 0.01f;

    // Added these to store initial door stats
    bool initialDoorOpen;
    bool initialLock;

    bool LockClose;

    private void Start()
    {
        // Saves the intial values of the door for reset
        initialDoorOpen = isOpen;
        initialLock = locked;

        isOpen = doorPivot.localScale.x == 0;
        doorValue = isOpen ? minDoorValue : 1;

        // Added by Kevin for changing door material color depending on lock status
        renderer.material = locked ? doorDisabled : doorEnabled;

        LockClose = false;
    }

    public void Activate()
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

        // Inverts door's state
        if (doorValue == endVal)
        {
            isOpen = !isOpen;
            activation = false;

            if (LockClose)
            {
                LockClose = false;
            }
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
        doorPivot.localScale = new Vector3(doorValue, 1, 1);

        isOpen = initialDoorOpen;
        activation = false;
    }

    // This is a function tied to IEnvironment meant to be used to reset a room
    public void ResetObject()
    {
        if (isOpen != initialDoorOpen)
        {
            ResetDoorPos();
        }

        SetLockStatus(initialLock);

        // Added to reset the original color back
        renderer.material = locked ? doorDisabled : doorEnabled;
    }

    public void StartObject()
    {
        // Nothing needs to happen here
    }

    public void StopObject()
    {
        // Nothing needs to happen here
    }
}
