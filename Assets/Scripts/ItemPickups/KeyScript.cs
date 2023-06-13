using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    [SerializeField] float freq;
    [SerializeField] float amp;
    
    Vector3 startingPos;


    private void Start()
    {
        startingPos = transform.position;
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, startingPos.y + Mathf.Sin(Time.time*freq) * amp, transform.position.z);
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.SetKeyCounter(1);
            Destroy(gameObject);
        }
    }
}
