using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour, IDoorActivator
{


    [SerializeField] Transform doorPivot;


    [SerializeField] bool locked;
    [SerializeField] float doorSpeed;

    bool isOpen = false;
   [SerializeField] bool activation;
    float doorValue;

    const float minDoorValue = 0.01f;

    private void Start()
    {
        isOpen = doorPivot.localScale.x == 0;
        doorValue = isOpen ? minDoorValue : 1;
    }

    public void Activate()
    {
        if(!locked)
        {
            activation = true;
        }
    }

    public void SetLockStatus(bool locked)
    {
        this.locked = locked;
    }

    private void Update()
    {
        if(activation && !locked)
        {
            var endVal = isOpen ? 1 : minDoorValue;

            doorValue = Mathf.MoveTowards(doorValue, endVal, Time.deltaTime * doorSpeed);
            doorPivot.localScale = new Vector3(doorValue, 1, 1);

            if(doorValue == endVal)
            {
                isOpen = !isOpen;
                activation = false;
            }

        }
    }


}
