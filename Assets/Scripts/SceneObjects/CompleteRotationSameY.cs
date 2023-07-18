using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteRotationSameY : MonoBehaviour
{
    [SerializeField] float rate;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0f, Time.deltaTime * rate, 0f);
    }
}
