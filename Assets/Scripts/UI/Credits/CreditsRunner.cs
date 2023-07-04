using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsRunner : MonoBehaviour
{
    [SerializeField, Range(.01f, 5)] float loadingTime = 1.5f;
    [SerializeField, Range(.01f, 5)] float exitingTime = 3.5f;
    [SerializeField] Image image;
    [SerializeField] AnimationCurve curve;

    [SerializeField] GameObject teamObj;

    private bool notDoublePerformingCheck = false;
    private bool forceEnd = false;
    
    public void ForceEndCredits()
    {
        forceEnd = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        if ((RunKey() && !notDoublePerformingCheck) || forceEnd)
        {
            notDoublePerformingCheck = true;
            StartCoroutine(FadeOut("MainMenu"));
        }
    }

    private bool RunKey()
    {
        
        return InputManager.Instance.Action.any.WasPressedThisFrame();
    }

    /// <summary>
    /// The FadeIn() function will create a timer, as time moves along the
    /// 'image' component provided earlier will transition to being transparent
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeIn()
    {
        // Create a float storing the timer
        float timerFadeIn = loadingTime;

        // While the timer is above a 'second'
        while (timerFadeIn > 0f)
        {
            // Add the Time.deltatime (interval in seconds from last frame to current frame) to the timer
            timerFadeIn -= Time.deltaTime;
            // Evaluate the curve and then set that the alpha's amount
            float alphaColorFadeIn = curve.Evaluate(timerFadeIn);
            // Set the image color component to a new color of an decreased alpha
            image.color = new Color(0f, 0f, 0f, alphaColorFadeIn);
            yield return 0;
        }
        teamObj.SetActive(true);
        image.GameObject().SetActive(false);
    }

    /// <summary>
    /// The FadeOut() function will create a timer, as time moves along the
    /// 'image' component provided earlier will transition to being fully colored
    /// as the alpha channel will have values added to it.
    /// </summary> 
    /// <param name="scene">The scene to load after</param>
    /// <returns></returns>
    IEnumerator FadeOut(string scene)
    {
        image.GameObject().SetActive(true);
        // Create a float storing the timer
        float timerFadeOut = 0f;

        // While the timer is below a 'second'
        while (timerFadeOut < exitingTime)
        {
            // Add the Time.deltatime (interval in seconds from last frame to current frame) to the timer
            timerFadeOut += Time.deltaTime;
            // Evaluate the curve and then set that the alpha's amount
            float alphaColorFadeOut = curve.Evaluate(timerFadeOut);
            // Set the image color component to a new color of an increased alpha
            image.color = new Color(0f, 0f, 0f, alphaColorFadeOut);
            yield return 0;
        }
        // After fading the scene out transition to the scene we want to load


        // DO NOT TOUCH THIS
        // THIS LOADS THE MAIN MENU AND ONLY THE MAIN MENU AFTER CREDITS ARE ROLLING
        // CREDITS ONLY ROLL WHEN EITHER INTHE MAIN MENU OR WHEN THE PLAYER BEATS THE GAME
        // BEATING THE GAME, THEY WILL GO BACK TO THE MAIN MENU.
        // --------------------------------------------------------------------------
        SceneManager.LoadScene(scene);
        // --------------------------------------------------------------------------
    }
}
