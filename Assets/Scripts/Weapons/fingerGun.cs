using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class fingerGun : MonoBehaviour
{
    [Header("----- Finger Gun Settings -----")]
    [Tooltip("Damage that the player will deal to the enemy with the basic finger pistol.")]
    [SerializeField] int bulletDamage;
    [SerializeField] float fireRate;
    [SerializeField] float focusPerShot;
    [SerializeField] int shootDist;

    public bool isShooting;

    [SerializeField] UnityEvent shootEvent;
    [SerializeField] GameObject hitParticles;

    [SerializeField] FingerGunSFX audioScript;

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.Action.Fire.IsPressed() && !isShooting && !GameManager.instance.InPauseState() && InputManager.Instance.Action.Melee.IsPressed() == false)
        {
            StartCoroutine(shoot());
        }
    }

    IEnumerator shoot()
    {
        isShooting = true;

        shootEvent?.Invoke();



        // if doing aimassist 
        // then do a spherecast to ind the enemy instead of the raycast

        var aimValue = GameManager.instance.GetAimAssistValue();
        var isOn = GameManager.instance.GetSettingsManager().settings.aimAssistEnabled;

        bool regShot = true;

        if (isOn)
        {
            RaycastHit hit;
            //var doHIt = Physics.SphereCast(Camera.main.transform.position, aimValue, Camera.main.transform.forward, out hit);



            var doHItAll = Physics.SphereCastAll(Camera.main.transform.position, aimValue, Camera.main.transform.forward, shootDist);
            if(doHItAll.Length > 0)
            {
                List<RaycastHit> results = new();
                foreach(var c in doHItAll)
                {
                    if (c.collider?.GetComponent<IDamagable>() != null && !c.collider.CompareTag("Player"))
                    {
                        results.Add(c);
                    }
                }

                if(results.Count > 0)
                {
                    RaycastHit closest = results[0];
                    foreach (var r in results)
                    {
                        if(r.distance < closest.distance)
                        {
                            closest = r;
                        }
                    }
                    
                    var d = closest.collider.GetComponent<IDamagable>();

                    Instantiate(hitParticles, closest.point, Quaternion.identity);

                    d.TakeDamage(bulletDamage);
                    audioScript.PlayOneShot_HitEnemy();
                    UIManager.instance.GetHitmarker().SetActive(true);
                    yield return new WaitForSeconds(0.05f);
                    UIManager.instance.GetHitmarker().SetActive(false);
                    GameManager.instance.GetPlayerResources().AddFocus(focusPerShot);

                    regShot = false;
                }
                else
                {
                    regShot = true;
                }

            }
            else
            {
                regShot = true;
            }


            //if (doHIt)
            //{


            //    IDamagable damageable = hit.collider.GetComponent<IDamagable>();

            //    if (damageable != null && !hit.collider.CompareTag("Player"))
            //    {
            //        Instantiate(hitParticles, hit.point, Quaternion.identity);

            //        damageable.TakeDamage(bulletDamage);
            //        audioScript.PlayOneShot_HitEnemy();
            //        UIManager.instance.GetHitmarker().SetActive(true);
            //        yield return new WaitForSeconds(0.05f);
            //        UIManager.instance.GetHitmarker().SetActive(false);
            //        GameManager.instance.GetPlayerResources().AddFocus(focusPerShot);

            //        regShot = false;
            //    }
            //}

        }

        if (regShot)
        {

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDist))
            {
                Instantiate(hitParticles, hit.point, Quaternion.identity);


                IDamagable damageable = hit.collider.GetComponent<IDamagable>();

                if (damageable != null)
                {
                    damageable.TakeDamage(bulletDamage);
                    UIManager.instance.GetHitmarker().SetActive(true);
                    yield return new WaitForSeconds(0.05f);
                    UIManager.instance.GetHitmarker().SetActive(false);
                    GameManager.instance.GetPlayerResources().AddFocus(focusPerShot);
                }

            }
        }




        //if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDist))
        //{
        //    Instantiate(hitParticles, hit.point, Quaternion.identity);


        //    IDamagable damageable = hit.collider.GetComponent<IDamagable>();

        //    if (damageable != null)
        //    {
        //        damageable.TakeDamage(bulletDamage);
        //        UIManager.instance.GetHitmarker().SetActive(true);
        //        yield return new WaitForSeconds(0.05f);
        //        UIManager.instance.GetHitmarker().SetActive(false);
        //        GameManager.instance.GetPlayerResources().AddFocus(focusPerShot);
        //    }

        //}

        yield return new WaitForSeconds(fireRate);
        isShooting = false;
    }


    public int GetShootDist()
    {
        return shootDist;
    }
}
