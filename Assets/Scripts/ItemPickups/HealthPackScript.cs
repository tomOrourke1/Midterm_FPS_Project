using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackScript : MonoBehaviour, IEntity
{
    [Header("-----Floating Stuff-----")]
    [SerializeField] float freq;
    [SerializeField] float amp;
    [Header("-----Healing Stuff-----")]
    [Range(0, 100)][SerializeField] int healingAmount;



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

        var heal = other.GetComponent<IHealReciever>();

        if(heal != null)
        {
            heal.AddHealing(healingAmount);
            Destroy(gameObject);
        }

    }

    public void Respawn()
    {
        Destroy(gameObject);
    }
}
