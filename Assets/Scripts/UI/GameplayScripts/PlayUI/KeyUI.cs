using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyUI : MonoBehaviour
{
    [Header("Key Images")]
    [SerializeField] Image key1;
    [SerializeField] Image key2;
    [SerializeField] Image key3;

    int keyCount = 0;

    private void Start()
    {
        ShowKeyUI();
    }

    /// <summary>
    /// Runs the function to update the keys displayed on the screen.
    /// </summary>
    public void ShowKeyUI()
    {
        UpdateKeyUI();
    }

    /// <summary>
    /// Updates the current amount of keys on the screen.
    /// </summary>
    private void UpdateKeyUI()
    {
        keyCount = GameManager.instance.GetPlayerObj().GetComponent<KeyChain>().GetKeys();

        switch (keyCount)
        {
            case 0:
                key1.enabled = false;
                key2.enabled = false;
                key3.enabled = false;
                break;

            case 1:
                key1.enabled = true;
                key2.enabled = false;
                key3.enabled = false;
                break;

            case 2:
                key1.enabled = true;
                key2.enabled = true;
                key3.enabled = false;
                break;

            case 3:
                key1.enabled = true;
                key2.enabled = true;
                key3.enabled = true;
                break;

            default:
                break;
        }
    }
}
