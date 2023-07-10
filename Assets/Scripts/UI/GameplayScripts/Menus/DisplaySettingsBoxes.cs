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
    [SerializeField] GameObject controllerObj;
    [SerializeField] GameObject controllerSelectedObj;

    private GameObject lastOpened;

    private void HideAll()
    {
        audioObj.SetActive(false);
        graphicsObj.SetActive(false);
        keybindsObj.SetActive(false);
        gameplayObj.SetActive(false);
        controllerObj.SetActive(false);
    }

    private void Awake()
    {
        HideAll();

        if (lastOpened == null)
        {
            DisplayGameplay();
        }
        else
        {
            lastOpened.SetActive(true);
        }

    }

    public void DisplayAudio()
    {
        HideAll();

        lastOpened = audioObj;
        
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(audioSelectedObj);

        lastOpened.SetActive(true);
    }

    public void DisplayGraphics()
    {
        HideAll();

        lastOpened = graphicsObj;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(graphicsSelectedObj);

        lastOpened.SetActive(true);
    }

    public void DisplayGameplay()
    {
        HideAll();

        lastOpened = gameplayObj;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(gameplaySelectedObj);

        lastOpened.SetActive(true);
    }

    public void DisplayKeybinds()
    {
        HideAll();

        lastOpened = keybindsObj;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(keybindsSelectedObj);

        lastOpened.SetActive(true);
    }

    public void DisplayController()
    {
        HideAll();

        lastOpened = controllerObj;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controllerObj);

        lastOpened.SetActive(true);
    }
}
