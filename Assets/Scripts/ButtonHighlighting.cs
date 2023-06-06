using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlighting : MonoBehaviour
{
    //the Menu Button affected
    public Button button;

    //The color it will be when hovered over
    public Color highlightColor;

    //The color it will have when not being hovered over
    private Color normalColor;

    //storing the colors in a block allows us to edit them
    private ColorBlock cb;
    // Start is called before the first frame update
    void Start()
    {
        cb = button.colors;
        normalColor = cb.selectedColor;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void WhenHovering()
    {
        cb.selectedColor = highlightColor;
        button.colors = cb;
    }
    public void WhenNotHovering() 
    {
        cb.selectedColor = normalColor;
        button.colors = cb;

    }
}
