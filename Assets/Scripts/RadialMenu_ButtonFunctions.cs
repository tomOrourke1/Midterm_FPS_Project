using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RadialMenu_ButtonFunctions : MonoBehaviour
{
    [Header("Components")]
    [Tooltip("Text box for displaying what weapon is selected.")]
    [SerializeField] TextMeshProUGUI textBox;

    public void SelectFingerGun()
    {
        textBox.text = "Finger Gun";
    }
    public void SelectPyrokinesis()
    {
        textBox.SetText("Pyrokinesis");
    }
    public void SelectCyrokinesis()
    {
        textBox.SetText("Cryokinesis");
    }
    public void SelectElectrokinesis()
    {
        textBox.SetText("Electrokinesis");
    }
    public void SelectAerokinesis()
    {
        textBox.SetText("Aerokinesis");
    }
}
