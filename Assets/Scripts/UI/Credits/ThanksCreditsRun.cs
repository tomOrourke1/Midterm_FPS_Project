using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThanksCreditsRun : MonoBehaviour
{
    float scrollTime;
    [SerializeField] CallNext script;
    [SerializeField] Image image;
    
    RectTransform uiTransform;
    private float yTmp;
    private bool runFinal;

    private void Start()
    {
        uiTransform = GetComponent<RectTransform>();
        scrollTime = Screen.height / 180f;

        image.gameObject.SetActive(false);
        uiTransform.position.Set(uiTransform.position.x, -uiTransform.rect.height - Screen.height, uiTransform.position.z);
        yTmp = Mathf.Abs(uiTransform.position.y) + Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        float tmp = Mathf.MoveTowards(gameObject.transform.position.y, yTmp, scrollTime);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, tmp, gameObject.transform.position.z);
        
        if (!runFinal && tmp >= yTmp)
        {
            runFinal = true;
            StartCoroutine(script.Next());
        }
    }
}
