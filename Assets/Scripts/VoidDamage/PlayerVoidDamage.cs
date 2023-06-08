using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVoidDamage : MonoBehaviour, IVoidDamage
{

    public void FallIntoTheVoid()
    {
        gameManager.instance.LoseGame();
    }
}
