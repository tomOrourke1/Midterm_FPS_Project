using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GitHistoryCredits : MonoBehaviour
{
    [SerializeField] float scrollTime;
    [SerializeField] CallNext script;
    private float yTmp;
    private bool runFinal;

    private void Start()
    {
        yTmp = Mathf.Abs(gameObject.transform.position.y) + 1000;
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
