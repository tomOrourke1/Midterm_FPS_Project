using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] float rateToMuffle;

    private bool muffeling;
    private bool unmuffeling;

    float cutoffVal;
    float cutoffCurr;

    private void Start()
    {
        unmuffeling = true;
    }

    private void Update()
    {
        if (muffeling)
        {
            mixer.GetFloat("Cutoff", out cutoffCurr);

            cutoffVal = Mathf.Lerp(cutoffCurr, 500f, rateToMuffle);
            mixer.SetFloat("Cutoff", cutoffVal);

            if (cutoffVal <= 500f)
            {
                muffeling = false;
            }
        }

        if (unmuffeling)
        {
            mixer.GetFloat("Cutoff", out cutoffCurr);

            cutoffVal = Mathf.Lerp(cutoffCurr, 1500f, rateToMuffle);
            mixer.SetFloat("Cutoff", cutoffVal);

            if (cutoffVal >= 1500)
            {
                unmuffeling = false;
            }
        }
    }

    public void RunMuffler()
    {

        muffeling = true;
        unmuffeling = false;
    }

    public void RunUnMuffler()
    {
        muffeling = false;
        unmuffeling = true;
    }
}
