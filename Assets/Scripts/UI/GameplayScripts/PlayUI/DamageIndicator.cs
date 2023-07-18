using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public static DamageIndicator Instance;
    [SerializeField] GameObject[] direcitonalList;
    [SerializeField] AnimationCurve curve;
    [SerializeField] float waitToFadeOut = 0.25f;
    [SerializeField] float timeToFadeOut = 0.25f;

    float ang;
    int idx;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        Disabler();
    }

    public void UpdateDirectional()
    {
        GameObject curImg = FIFOImage();
        StartCoroutine(FadeOff(curImg));
    }

    public void ReceiveAngle(float a)
    {
        ang = a;
        ang -= 180;
        UpdateDirectional();
    }


    private GameObject FIFOImage()
    {
        GameObject i = direcitonalList[idx];
        idx++;
        if (idx >= direcitonalList.Length)
            idx = 0;

        return i;
    }

    private IEnumerator FadeOff(GameObject obj)
    {
        // Turn on the game object (pivot point)
        obj.gameObject.SetActive(true);

        // Change the rotation of the pivot so the directional indicator will represent where its coming from
        obj.transform.rotation = Quaternion.Euler(0f, 0f, -ang);

        // Get the image object from the child
        Image _i = obj.GetComponentInChildren<Image>();
        // Set the Color to max (so we can fade it off)
        _i.color = new UnityEngine.Color(_i.color.r, _i.color.g, _i.color.b, 255f);

        // Wait the time to start fading out
        yield return new WaitForSeconds(waitToFadeOut);
        // Sets a temp value to the time that we are fading out
        float t = timeToFadeOut;

        // 
        while (t > 0)
        {
            t -= Time.deltaTime;
            float alphaColorFadeIn = curve.Evaluate(t);
            _i.color = new UnityEngine.Color(_i.color.r, _i.color.g, _i.color.b, alphaColorFadeIn);

            yield return 0;
        }

        obj.gameObject.SetActive(false);
        _i.color = new UnityEngine.Color(_i.color.r, _i.color.g, _i.color.b, 0f);
    }

    public void Disabler()
    {
        foreach (GameObject obj in direcitonalList)
        {
            obj.SetActive(false);
            Image i = obj.GetComponentInChildren<Image>();
            i.color = new UnityEngine.Color(i.color.r, i.color.g, i.color.b, 0f);
        }
    }
}
