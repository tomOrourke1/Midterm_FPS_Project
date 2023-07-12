using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveFocusField : MonoBehaviour
{

    [SerializeField] float amount = 5;


    private void OnTriggerStay(Collider other)
    {


        if (other.CompareTag("Player"))
        {
            GameManager.instance.GetPlayerResources().Focus.Decrease(amount * Time.deltaTime);
            UIManager.instance.GetPlayerStats().UpdateValues();
        }

        
    }


}
