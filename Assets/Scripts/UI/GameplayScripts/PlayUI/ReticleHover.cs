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
    [SerializeField] Color pickupableObjectColor;

    RaycastHit hit;
    Image reticle;

    int shootDistance;

    // Start is called before the first frame update
    void Start()
    {
        shootDistance = GameManager.instance.GetPlayerObj().GetComponent<fingerGun>().GetShootDist();

        reticle = GetComponentInChildren<Image>();
        reticle.GetComponent<Image>().color = defaultColor;
        reticle.sprite = GameManager.instance.GetSettingsManager().settings.currentRetical;
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
            else if (hit.collider.GetComponent<MoveableObject>() != null && UIManager.instance.GetRadialScript().GetConfirmedKinesis() == 2)
            {
                Debug.Log(UIManager.instance.GetRadialScript().GetConfirmedKinesis());
                reticle.GetComponent<Image>().color = pickupableObjectColor;
            }
            else
            {
                reticle.GetComponent<Image>().color = defaultColor;
            }
        }
        else
        {
            reticle.GetComponent<Image>().color = defaultColor;
        }
    }
}
