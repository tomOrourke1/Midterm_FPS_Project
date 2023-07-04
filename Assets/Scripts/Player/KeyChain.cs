using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyChain : MonoBehaviour
{
    [SerializeField] int keys;
    [SerializeField] int maxKeys;

    // Start is called before the first frame update
    void Start()
    {
        keys = 0;
        addKeys(0);
    }

    public int GetKeys()
    {
        return keys;
    }

    public void addKeys(int num)
    {
        keys += num;
        UIManager.instance.GetKeyUI().ShowKeyUI();
    }

    public void removeKeys(int num)
    {
        keys -= num;
        UIManager.instance.GetKeyUI().ShowKeyUI();
    }

    public void Clear()
    {
        keys = 0;
        UIManager.instance.GetKeyUI().ShowKeyUI();
    }

    public int GetMaxKeys()
    {
        return maxKeys;
    }
}
