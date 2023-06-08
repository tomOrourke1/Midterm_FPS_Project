using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVoidDamage : MonoBehaviour, IVoidDamage
{
    [SerializeField] Player player;

    public void FallIntoTheVoid()
    {
        player.TakeDamage((int) player.GetPlayerMaxHP() + (int) player.GetPlayerMaxShield());
    }
}
