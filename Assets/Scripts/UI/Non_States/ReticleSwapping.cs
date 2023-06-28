using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleSwapping : MonoBehaviour
{
    [SerializeField] Image currentReticle;
    [SerializeField] RectTransform highlighter;
    [SerializeField] GameObject hitmarkerObj;

    public void SwapImage(Image img)
    {
        highlighter = (RectTransform)gameObject.transform;
        currentReticle = img;
    }
}
