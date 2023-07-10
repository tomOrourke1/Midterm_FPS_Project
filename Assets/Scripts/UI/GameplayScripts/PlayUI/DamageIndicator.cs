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
    [SerializeField] float waitToFadeOut;

    float ang;
    int idx;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        foreach (GameObject obj in direcitonalList)
        {
            obj.SetActive(false);
            Image i = obj.GetComponentInChildren<Image>();
            i.color = new UnityEngine.Color(i.color.r, i.color.g, i.color.b, 0f);
        }
    }

    public void UpdateDirectional()
    {
        GameObject curImg = FIFOImage();
        StartCoroutine(FadeOff(curImg));
    }

    public void ReceiveAngle(float a)
    {
        ang = a;
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
        obj.gameObject.SetActive(true);
        Image _i = obj.GetComponentInChildren<Image>();
        _i.color = new UnityEngine.Color(_i.color.r, _i.color.g, _i.color.b, 255f);

        yield return new WaitForSeconds(waitToFadeOut);
        float t = 0.25f;
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
}
