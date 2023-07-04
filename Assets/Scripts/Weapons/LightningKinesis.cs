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
    public UnityEvent OnElectroForceStop;

    [SerializeField] GameObject lightningParticles;
    [SerializeField] GameObject focusParticles;

    [SerializeField] ParticleSystem lightningHit1;
    [SerializeField] ParticleSystem lightningHit2;
    [SerializeField] float spawnDelay;
    bool spawningHits;

    bool doesLightning;

    [Header("SFX")]
    [SerializeField] ElectroSFX electroSFX;

    // Start is called before the first frame update
    void Start()
    {
        //lightning = attackPoint.GetComponent<LineRenderer>();
        UpdateLightningNoLook();
        //lightning.enabled = true;
    }

   
    public override void Fire()
    {
        
        if (InputManager.Instance.Action.Kinesis.IsPressed() && HasFocus())
        {
            if(InputManager.Instance.Action.Kinesis.WasPressedThisFrame())
            {
                OnElectroStart?.Invoke();
                focusParticles.SetActive(true); 
                isCasting = true;
            }

            if(doesLightning && GameManager.instance.GetPlayerResources().SpendFocus(focusCost * Time.deltaTime))
            {
                LookCast();
                //lightning.enabled = true;
                lightningParticles.SetActive(true);

            }

        }
        else if(/*lightning.enabled*/ lightningParticles.activeInHierarchy && InputManager.Instance.Action.Kinesis.IsPressed() && !HasFocus())
        {
            //lightning.enabled = false;
            lightningParticles.SetActive(false);
            focusParticles.SetActive(false);
            isCasting = false;
        }
        else if (InputManager.Instance.Action.Kinesis.WasReleasedThisFrame()) 
        {
            //lightning.enabled = false; 
            lightningParticles.SetActive(false);
            OnElectroStop?.Invoke();
            doesLightning = false;
            focusParticles.SetActive(false);
            isCasting = false;
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


            if (!spawningHits)
            {
                StartCoroutine(spawnHit(hit.point));
            }
            
            if (damageable != null && !hit.collider.CompareTag("Player"))
            {
                Debug.Log("reached inside of null check");
                damageable.TakeElectroDamage(Damage * Time.deltaTime);
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
        electroSFX.PlayElectro_Shoot();
    }



    bool HasFocus()
    {
        return GameManager.instance.GetPlayerResources().Focus.CurrentValue >= (focusCost * Time.deltaTime);
    }

    public override void StopFire()
    {
        isCasting = false;
        spawningHits = false;
        doesLightning = false;

        OnElectroForceStop?.Invoke();
    }
}
