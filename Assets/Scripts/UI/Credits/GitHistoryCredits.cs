using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GitHistoryCredits : MonoBehaviour
{
    float scrollTime;
    [SerializeField] CallNext script;
    [SerializeField] float scrollMultiplier;
    [SerializeField] CreditsRunner cRScript;

    RectTransform uiTransform;
    private float yTmp;
    private bool runFinal;

    private void Start()
    {
        uiTransform = GetComponent<RectTransform>();
        scrollTime = cRScript.GetScrollSpeed();
        uiTransform.position.Set(uiTransform.position.x, -uiTransform.rect.height - Screen.height, uiTransform.position.z);
        yTmp = Mathf.Abs(uiTransform.position.y) + Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Y TEMP" + yTmp);
        //Debug.Log("SCROLL" + Screen.height / 180f + "DELTA TIME" + Time.deltaTime + "MULTIPLIER" + scrollMultiplier);
        float tmp = Mathf.MoveTowards(gameObject.transform.position.y, yTmp, scrollTime * Time.deltaTime * scrollMultiplier);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, tmp, gameObject.transform.position.z);
        //Debug.Log(tmp);

        if (!runFinal && tmp >= yTmp)
        {
            runFinal = true;
            StartCoroutine(script.Next());
        }
    }

    public float GetSpeed()
    {
        return scrollTime;
    }

    public void SetSpeed(float a)
    {
        scrollTime = a;
    }
}
