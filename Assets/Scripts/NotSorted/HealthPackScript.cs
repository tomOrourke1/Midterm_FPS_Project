using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackScript : MonoBehaviour
{
    [Header("-----Floating Stuff-----")]
    [SerializeField] float freq;
    [SerializeField] float amp;
    [Header("-----Healing Stuff-----")]
    [Range(-100, 0)][SerializeField] int healingAmount;



    Vector3 startingPos;


    private void Start()
    {
        startingPos = transform.position;
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, startingPos.y + Mathf.Sin(Time.time * freq) * amp, transform.position.z);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerscript.TakeDamage(healingAmount);
            gameManager.instance.pStatsUI.UpdateValues();
            Destroy(gameObject);
        }
    }
}
