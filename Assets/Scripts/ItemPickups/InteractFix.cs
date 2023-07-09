using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractFix : MonoBehaviour
{
    public void EnableInteract()
    {
        InputManager.Instance.canInteract = true;
    }
}
