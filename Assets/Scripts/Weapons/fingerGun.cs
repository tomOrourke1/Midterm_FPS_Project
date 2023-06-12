using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fingerGun : MonoBehaviour
{
    [Header("----- Finger Gun Settings -----")]
    [Tooltip("Damage that the player will deal to the enemy with the basic finger pistol.")]
    [SerializeField] int bulletDamage;
    [SerializeField] float fireRate;
    [SerializeField] float focusPerShot;
    [SerializeField] int shootDist;

    private bool isShooting;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !isShooting)
        {
            StartCoroutine(shoot());
        }
    }

    IEnumerator shoot()
    {
        isShooting = true;
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDist))
        {
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

        yield return new WaitForSeconds(fireRate);
        isShooting = false;
    }
}
