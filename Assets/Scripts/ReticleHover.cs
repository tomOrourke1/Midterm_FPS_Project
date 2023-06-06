using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ReticleHover : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Color defaultColor;
    [SerializeField] Color enemyTargetedColor;
    [SerializeField] int shootDistance;

    RaycastHit hit;
    Image reticle;


    // Start is called before the first frame update
    void Start()
    {
        reticle = GetComponentInChildren<Image>();
        reticle.GetComponent<Image>().color = defaultColor;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDistance))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                reticle.GetComponent<Image>().color = enemyTargetedColor;
            }
            else
            {
                reticle.GetComponent<Image>().color = defaultColor;
            }
        }
    }
}
