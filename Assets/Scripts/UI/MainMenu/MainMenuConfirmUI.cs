using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuConfirmUI : MonoBehaviour
{
    [SerializeField] GameObject layout;
    [SerializeField] GameObject confirmButton;
    [SerializeField] GameObject denyButton;
    [SerializeField] GameObject denyFollowedButton;
    [SerializeField] GameObject confirmMenu;

    public void OpenConfirm()
    {
        layout.SetActive(false);
        confirmMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(confirmButton.gameObject);
    }

    public void CloseConfirm()
    {
        confirmMenu.SetActive(false);
        layout.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(denyFollowedButton);
    }

}
