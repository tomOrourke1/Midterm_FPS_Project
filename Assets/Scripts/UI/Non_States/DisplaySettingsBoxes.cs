using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplaySettingsBoxes : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField] GameObject audioObj;
    [SerializeField] GameObject audioSelectedObj;
    [SerializeField] GameObject graphicsObj;
    [SerializeField] GameObject graphicsSelectedObj;
    [SerializeField] GameObject gameplayObj;
    [SerializeField] GameObject gameplaySelectedObj;
    [SerializeField] GameObject keybindsObj;
    [SerializeField] GameObject keybindsSelectedObj;

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
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(audioSelectedObj);
    }

    public void DisplayGraphics()
    {
        HideAll();
        graphicsObj.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(graphicsSelectedObj);
    }

    public void DisplayGameplay()
    {
        HideAll();
        gameplayObj.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(gameplaySelectedObj);
    }

    public void DisplayKeybinds()
    {
        HideAll();
        keybindsObj.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(keybindsSelectedObj);
    }
}
