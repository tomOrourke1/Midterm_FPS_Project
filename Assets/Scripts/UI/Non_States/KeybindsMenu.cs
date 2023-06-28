using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindsMenu : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] GameObject mainObject;
    [SerializeField] GameObject movementMenu;
    [SerializeField] GameObject abilityMenu;
    [SerializeField] GameObject miscMenu;

    public void MovementKeybinds()
    {
        TurnOffAllMenus();
        movementMenu.SetActive(true);
    }

    public void AbilityKeybinds()
    {
        TurnOffAllMenus();
        abilityMenu.SetActive(true);
    }

    public void MiscKeyBinds()
    {
        TurnOffAllMenus();
        miscMenu.SetActive(true);
    }

    private void TurnOffAllMenus()
    {
        miscMenu.SetActive(false);
        abilityMenu.SetActive(false);
        movementMenu.SetActive(false);
    }
}
