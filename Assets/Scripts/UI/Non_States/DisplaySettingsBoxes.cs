using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySettingsBoxes : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField] GameObject audioObj;
    [SerializeField] GameObject graphicsObj;
    [SerializeField] GameObject gameplayObj;
    [SerializeField] GameObject keybindsObj;

    private void HideAll()
    {
        audioObj.SetActive(false);
        graphicsObj.SetActive(false);
        keybindsObj.SetActive(false);
        gameplayObj.SetActive(false);
    }

    public void DisplayAudio()
    {
        HideAll();
        audioObj.SetActive(true);
    }

    public void DisplayGraphics()
    {
        HideAll();
        graphicsObj.SetActive(true);
    }

    public void DisplayGameplay()
    {
        HideAll();
        gameplayObj.SetActive(true);
    }

    public void DisplayKeybinds()
    {
        HideAll();
        keybindsObj.SetActive(true);
    }
}
