using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyChain : MonoBehaviour
{
    int keys;

    // Start is called before the first frame update
    void Start()
    {
        keys = 0;
    }

    public int GetKeys()
    {
        return keys;
    }

    public void addKeys(int num)
    {
        keys += num;
    }

    public void removeKeys(int num)
    {
        keys -= num;
    }

    public void clear()
    {
        keys = 0;
    }
}
