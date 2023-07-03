using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVelocityReciever : MonoBehaviour, IApplyVelocity
{

    [Header("--- Componenets ---")]
    [SerializeField] CharacterController controller;

    [Header("---- Values ----")]
    [SerializeField] float decreaseAmount;


    Vector3 velToApply;
    bool applyVel;

    private void Update()
    {
        if (applyVel)
        {
            controller.Move(velToApply * Time.deltaTime);

            velToApply = Vector3.Lerp(velToApply, Vector3.zero, decreaseAmount * Time.deltaTime);

            if(velToApply.magnitude == 0)
            {
                applyVel = false;
            }
        }
    }

    public void ApplyVelocity(Vector3 velocity)
    {
        applyVel = true;
        velToApply += velocity;
    }


}
