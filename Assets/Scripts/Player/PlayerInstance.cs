using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] PlayerResources resources;

    public Player Player => player;
    public PlayerResources Resources => resources;
}