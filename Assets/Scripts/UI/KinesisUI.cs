using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KinesisUI : MonoBehaviour
{
    [SerializeField] Sprite displayImage;
    [SerializeField] string kinesisName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.instance.ShowInfoUI(displayImage, kinesisName);
        }
    }
}
