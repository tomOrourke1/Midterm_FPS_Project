using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    private bool running;
    [SerializeField] Image image;
    [SerializeField] AnimationCurve curve;

    public void Ping()
    {
        if (!running)
        StartCoroutine(FadeOut());
    }

    /// <summary>
    /// The FadeOut() function will create a timer, as time moves along the
    /// 'image' component provided earlier will transition to being fully colored
    /// as the alpha channel will have values added to it.
    /// </summary> 
    /// <param name="scene">The scene to load after</param>
    /// <returns></returns>
    IEnumerator FadeOut()
    {
        image.color = new Color(0f, 0f, 0f, 0f);
        image.gameObject.SetActive(true);

        // Create a float storing the timer
        float timerFadeOut = 0f;

        // While the timer is below a 'second'
        while (timerFadeOut < 1.5f)
        {
            // Add the Time.deltatime (interval in seconds from last frame to current frame) to the timer
            timerFadeOut += Time.unscaledDeltaTime;
            // Evaluate the curve and then set that the alpha's amount
            float alphaColorFadeOut = curve.Evaluate(timerFadeOut);
            // Set the image color component to a new color of an increased alpha
            image.color = new Color(0f, 0f, 0f, alphaColorFadeOut);
            yield return 0;
        }

        Application.Quit();
    }
}
