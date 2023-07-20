using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsRunner : MonoBehaviour
{
    [Header("Team Members Fade In/Out")]
    [SerializeField, Range(.01f, 5)] float loadingTime = 1.5f;
    [SerializeField, Range(.01f, 5)] float exitingTime = 3.5f;
    [SerializeField] UnityEngine.UI.Image image;
    [SerializeField] AnimationCurve curve;

    [Header("Main Objects for Credits")]
    [SerializeField] GameObject teamObj;
    [SerializeField] GameObject assetsObj;
    [SerializeField] GameObject gitObj;
    [SerializeField] GameObject miniFadeOut;

    [SerializeField] float speedMulitplier = 15f;

    // Speed holders
    private float normalSpeed_git;
    private float alteredSpeed_git;
    private float currentSpeed_git;
    private float scrollSpeed;

    // Height holders
    private float teamObjDesiredHeight;
    private float assetsObjDesiredHeight;
    private float gitObjDesiredHeight;

    private Image miniFadeImage;
    private RectTransform rectTransform_TeamObj;
    private RectTransform rectTransform_AssetsObj;
    private RectTransform rectTransform_GitObj;

    private bool onlyLeaveSceneOnce = false;
    private bool forceEnd = false;
    private bool teamFadeOut = false;

    public void ForceEndCredits()
    {
        forceEnd = true;
    }

    private void Awake()
    {
        // There is literally no game manager in the Credits Scene. This is explicit, do not try me.
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        SetupObjects();
        SetupValues();
    }

    // Start is called before the first frame update
    void Start()
    {
       // InputManager.Instance.Input.Enable();
        StartCoroutine(StartFadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        InputHandler();

        if (teamObj.activeInHierarchy)
        {
            HandleObj(teamObj, rectTransform_TeamObj, teamObjDesiredHeight, scrollSpeed * Time.unscaledDeltaTime);
            HandleTransitions(teamObj);
        }
        else if (assetsObj.activeInHierarchy)
        {
            HandleObj(assetsObj, rectTransform_AssetsObj, assetsObjDesiredHeight, scrollSpeed * Time.unscaledDeltaTime);
            HandleTransitions(assetsObj);
        }
        else if (gitObj.activeInHierarchy)
        {
            HandleObj(gitObj, rectTransform_GitObj, gitObjDesiredHeight, currentSpeed_git * Time.unscaledDeltaTime);
            HandleTransitions(gitObj);
        }

    }

    private int LeaveCreditsScene()
    {
        if (InputManager.Instance.Action.Jump.IsPressed())
        {
            return 1;
        }

        if (InputManager.Instance.Action.any.WasPressedThisFrame())
        {
            return 2;
        }

        return 0;
    }

    /// <summary>
    /// The FadeIn() function will create a timer, as time moves along the
    /// 'image' component provided earlier will transition to being transparent
    /// </summary>
    /// <returns></returns>
    IEnumerator StartFadeIn()
    {
        
        // Create a float storing the timer
        float timerFadeIn = loadingTime;

        // While the timer is above a 'second'
        while (timerFadeIn > 0f)
        {
            // Add the Time.deltatime (interval in seconds from last frame to current frame) to the timer
            timerFadeIn -= Time.unscaledDeltaTime;
            // Evaluate the curve and then set that the alpha's amount
            float alphaColorFadeIn = curve.Evaluate(timerFadeIn);
            // Set the image color component to a new color of an decreased alpha
            image.color = new Color(0f, 0f, 0f, alphaColorFadeIn);
            yield return 0;
        }
        
        // Turn on the first object, which is the team object.
        teamObj.SetActive(true);
    }

    /// <summary>
    /// The FadeOut() function will create a timer, as time moves along the
    /// 'image' component provided earlier will transition to being fully colored
    /// as the alpha channel will have values added to it.
    /// </summary> 
    /// <param name="scene">The scene to load after</param>
    /// <returns></returns>
    public IEnumerator FadeOut(string scene)
    {
        image.GameObject().SetActive(true);
        // Create a float storing the timer
        float timerFadeOut = 0f;

        // While the timer is below a 'second'
        while (timerFadeOut < exitingTime)
        {
            // Add the Time.deltatime (interval in seconds from last frame to current frame) to the timer
            timerFadeOut += Time.unscaledDeltaTime;
            // Evaluate the curve and then set that the alpha's amount
            float alphaColorFadeOut = curve.Evaluate(timerFadeOut);
            // Set the image color component to a new color of an increased alpha
            image.color = new Color(0f, 0f, 0f, alphaColorFadeOut);
            yield return 0;
        }
        // After fading the scene out transition to the scene we want to load
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        // DO NOT TOUCH THIS
        // THIS LOADS THE MAIN MENU AND ONLY THE MAIN MENU AFTER CREDITS ARE ROLLING
        // CREDITS ONLY ROLL WHEN EITHER INTHE MAIN MENU OR WHEN THE PLAYER BEATS THE GAME
        // BEATING THE GAME, THEY WILL GO BACK TO THE MAIN MENU.
        // --------------------------------------------------------------------------
        SceneManager.LoadScene(scene);
        // --------------------------------------------------------------------------
    }

    public float GetScrollSpeed()
    {
        return Screen.height / 180f * speedMulitplier;
    }

    private void SetupObjects()
    {
        teamObj.SetActive(false);
        assetsObj.SetActive(false);
        gitObj.SetActive(false);
        miniFadeOut.SetActive(false);

        rectTransform_TeamObj = teamObj.GetComponent<RectTransform>();
        rectTransform_AssetsObj = assetsObj.GetComponent<RectTransform>();
        rectTransform_GitObj = gitObj.GetComponent<RectTransform>();

        miniFadeImage = miniFadeOut.GetComponent<Image>();
    }

    private void SetupValues()
    {
        // Normal speed
        scrollSpeed = GetScrollSpeed();

        // Speeds for Git Object
        normalSpeed_git = scrollSpeed;
        alteredSpeed_git = scrollSpeed * 5f;
        currentSpeed_git = normalSpeed_git;

        // Setup height vals
        teamObjDesiredHeight = Screen.height / 2f;
        assetsObjDesiredHeight = Mathf.Abs(assetsObj.transform.position.y) + Screen.height;
        gitObjDesiredHeight = Mathf.Abs(gitObj.transform.position.y) + Screen.height;

    }

    private void InputHandler()
    {
        if ((LeaveCreditsScene() == 2 && !onlyLeaveSceneOnce) || forceEnd)
        {
            onlyLeaveSceneOnce = true;
            StartCoroutine(FadeOut("MainMenu"));
        }
        else if (LeaveCreditsScene() == 1)
        {
            currentSpeed_git = alteredSpeed_git;
        }
        else
        {
            currentSpeed_git = normalSpeed_git;
        }
    }

    private void HandleObj(GameObject obj, RectTransform rectTrans, float height, float speed)
    {
        float tmp = Mathf.MoveTowards(obj.transform.position.y, height, speed);
        obj.transform.position = new Vector3(obj.transform.position.x, tmp, obj.transform.position.z);
    }

    private void HandleTransitions(GameObject currentObj)
    {
        if (currentObj == teamObj)
        {
            if (currentObj.transform.position.y >= teamObjDesiredHeight && !teamFadeOut)
            {
                StartCoroutine(FadeMiniObj());
            }
        }
        else if (currentObj == assetsObj)
        {
            if (currentObj.transform.position.y >= assetsObjDesiredHeight)
            {
                assetsObj.SetActive(false);
                gitObj.SetActive(true);
            }
        }
        else if (currentObj == gitObj)
        {
            if (currentObj.transform.position.y >= gitObjDesiredHeight)
            {
                gitObj.SetActive(false);
                forceEnd = true;
            }
        }
    }

    IEnumerator FadeMiniObj()
    {
        teamFadeOut = true;
        yield return new WaitForSeconds(1f);
        miniFadeImage.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
        miniFadeImage.GameObject().SetActive(true);
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
            miniFadeImage.color = new Color(0f, 0f, 0f, alphaColorFadeOut);
            yield return 0;
        }

        teamObj.gameObject.SetActive(false);
        assetsObj.gameObject.SetActive(true);
    }
}
