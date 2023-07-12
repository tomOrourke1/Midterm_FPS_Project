using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ElevatorScript : MonoBehaviour, IInteractable
{
    [Header("Components")]
    [SerializeField, Range(.01f, 5)] float loadingTime = 1.5f;
    [SerializeField, Range(.01f, 5)] float exitingTime = 3.5f;
    
    [Tooltip("The animation curve component to use.")]
    [SerializeField] AnimationCurve curve;

    [Header("Don't Touch")]
    [Tooltip("This is serialized because of main menu. Main menu doesn't have a game manager so we need to have the option of getting it via inspector.")]
    [SerializeField] Image image;

    private void Start()
    {
        if (image == null)
            image = UIManager.instance.GetSceneFader();


        //Debug.Log("FADE IN");
        StopAllCoroutines();
        StartCoroutine(FadeIn());
    }

    public void Interact()
    {
        FadeTo(GameManager.instance.GetNextLevel());
    }

    /// <summary>
    /// The FadeTo() funciton will start a coroutine of FadeOut() with the name of the scene passed into it.
    /// The scene must be a valid scene or else there will be an error thrown.
    /// Make sure to properly spell the scenes name out when typing it in the components.
    /// </summary>
    /// <param name="scene">The name of the scene to switch to.</param>
    public void FadeTo(string scene)
    {
        //Debug.Log(scene);
        StartCoroutine(FadeOut(scene));
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

        if (scene == "")
        {
            //Debug.Log("No scene specified in Game Manager: Heading to Main Menu -->");

            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
    }
}
