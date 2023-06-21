using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour, IEntity
{
    [Header("-----Floating Stuff-----")]
    [SerializeField] float freq;
    [SerializeField] float amp;
    [Header("-----Healing Stuff-----")]
    [Range(0, 100)][SerializeField] int shieldAmount;



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

        var shield = other.GetComponent<IShieldReceiver>();

        if (shield != null)
        {
            shield.AddShield(shieldAmount);
            Destroy(gameObject);
        }

    }

    public void Respawn()
    {
        Destroy(gameObject);
    }

}
