using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeamMembersCreditsRun : MonoBehaviour
{
    float scrollTime;
    [SerializeField] float fadeTime;
    [SerializeField] Image image;
    [SerializeField] CallNext script;
    [SerializeField] AnimationCurve curve;
    
    RectTransform uiTransform;
    private bool runFinal;
    private float endPosY = Screen.height/2f;

    private void Start()
    {
        uiTransform = GetComponent<RectTransform>();
        scrollTime = Screen.height / 180f;
        
        uiTransform.position.Set(uiTransform.position.x, -uiTransform.rect.height - Screen.height, uiTransform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float tmp = Mathf.MoveTowards(gameObject.transform.position.y, endPosY, scrollTime);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, tmp, gameObject.transform.position.z);

        if (!runFinal && tmp >= endPosY)
        {
            runFinal = true;
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1f);
        image.GameObject().SetActive(true);
        // Create a float storing the timer
        float timerFadeOut = 0f;

        // While the timer is below a 'second'
        while (timerFadeOut < fadeTime)
        {
            // Add the Time.deltatime (interval in seconds from last frame to current frame) to the timer
            timerFadeOut += Time.deltaTime;
            // Evaluate the curve and then set that the alpha's amount
            float alphaColorFadeOut = curve.Evaluate(timerFadeOut);
            // Set the image color component to a new color of an increased alpha
            image.color = new Color(0f, 0f, 0f, alphaColorFadeOut);
            yield return 0;
        }
        StartCoroutine(script.Next());
    }
}
