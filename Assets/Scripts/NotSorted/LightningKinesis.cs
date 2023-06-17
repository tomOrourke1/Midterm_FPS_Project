using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.HID;

public class LightningKinesis : KinesisBase
{

    [SerializeField] float maxDistance;
    [SerializeField] float Damage;
    [SerializeField] GameObject attackPoint;
    //LineRenderer lightning;


    public UnityEvent OnElectroStart;
    public UnityEvent OnElectroStop;

    [SerializeField] GameObject lightningParticles;
    [SerializeField] GameObject focusParticles;

    [SerializeField] ParticleSystem lightningHit1;
    [SerializeField] ParticleSystem lightningHit2;
    [SerializeField] float spawnDelay;
    bool spawningHits;

    bool doesLightning;

    // Start is called before the first frame update
    void Start()
    {
        //lightning = attackPoint.GetComponent<LineRenderer>();
        UpdateLightningNoLook();
        //lightning.enabled = true;
    }

   
    public override void Fire()
    {
        
        if (Input.GetKey(KeyCode.Mouse1) && GameManager.instance.GetPlayerResources().SpendFocus(focusCost * Time.deltaTime))
        {
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                OnElectroStart?.Invoke();
                focusParticles.SetActive(true); 
            }

            if(doesLightning)
            {
                LookCast();
                //lightning.enabled = true;
                lightningParticles.SetActive(true);

            }

        }
        else if(/*lightning.enabled*/ lightningParticles.activeInHierarchy && Input.GetKey(KeyCode.Mouse1) && !GameManager.instance.GetPlayerResources().SpendFocus(focusCost * Time.deltaTime))
        {
            //lightning.enabled = false;
            lightningParticles.SetActive(false);
            focusParticles.SetActive(false);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1)) 
        {
            //lightning.enabled = false; 
            lightningParticles.SetActive(false);
            OnElectroStop?.Invoke();
            doesLightning = false;
            focusParticles.SetActive(false);

        }
    }
    void LookCast()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {

            LaserCast(hit.point);
        
        }
        else
        {
            UpdateLightningNoLook();
        }
    }
    void LaserCast(Vector3 target) 
    {
        RaycastHit hit;
        if (Physics.Raycast(attackPoint.transform.position, target - attackPoint.transform.position, out hit, maxDistance))
        {
            IDamagable damageable = hit.collider.GetComponent<IDamagable>();


            if(!spawningHits)
            {
                StartCoroutine(spawnHit(hit.point));
            }

            if (damageable != null)
            {
                damageable.TakeDamage(Damage * Time.deltaTime);
            }
           
            if(hit.point != null)
            {
                UpdateLightning(hit.point);
            }
            
        }
        else {

            UpdateLightningNoPoint(target-attackPoint.transform.position); 
        }
    }

    IEnumerator spawnHit(Vector3 point)
    {
        spawningHits = true;
        Instantiate(lightningHit1, point, Quaternion.identity);
        Instantiate(lightningHit2, point, Quaternion.identity);
        yield return new WaitForSeconds(spawnDelay);
        spawningHits = false;
    }



    void UpdateLightningNoLook()
    {
        Vector3 endPoint;
        endPoint = transform.position;

        Vector3 maxDistPoint = Camera.main.transform.forward * maxDistance;
        endPoint += maxDistPoint;

        //lightning.SetPosition(0, attackPoint.transform.position);
        //lightning.SetPosition(1, endPoint);
    }
    void UpdateLightning(Vector3 hitPoint = new Vector3())
    {
        //lightning.SetPosition(0, attackPoint.transform.position);
        //lightning.SetPosition(1, hitPoint);
    }
   void  UpdateLightningNoPoint(Vector3 dir) 
    {
        Vector3 hitPoint = (dir.normalized * maxDistance) + attackPoint.transform.position;

       // lightning.SetPosition(0, attackPoint.transform.position);
       // lightning.SetPosition(1, hitPoint);
    }
    



    public void StartLightning()
    {
        doesLightning = true;
    }

}
