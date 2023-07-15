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

    private bool isShooting;

    [SerializeField] UnityEvent shootEvent;
    [SerializeField] GameObject hitParticles;


    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.Action.Fire.IsPressed() && !isShooting && !GameManager.instance.InPauseState())
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
            var doHIt = Physics.SphereCast(Camera.main.transform.position, aimValue, Camera.main.transform.forward, out hit);

            if (doHIt)
            {


                IDamagable damageable = hit.collider.GetComponent<IDamagable>();

                if (damageable != null && !hit.collider.CompareTag("Player"))
                {
                    Instantiate(hitParticles, hit.point, Quaternion.identity);

                    damageable.TakeDamage(bulletDamage);
                    UIManager.instance.GetHitmarker().SetActive(true);
                    yield return new WaitForSeconds(0.05f);
                    UIManager.instance.GetHitmarker().SetActive(false);
                    GameManager.instance.GetPlayerResources().AddFocus(focusPerShot);

                    regShot = false;
                }
            }

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
