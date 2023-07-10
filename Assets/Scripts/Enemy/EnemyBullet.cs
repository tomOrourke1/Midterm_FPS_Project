using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    [SerializeField] float bulletDamage;
    [SerializeField] int bulletSpeed;
    [SerializeField] float destroyBulletTimer;

    [SerializeField] Rigidbody rb;


    Vector3 lastPos;
    int frames;
    void Start()
    {
        Destroy(gameObject, destroyBulletTimer);
        rb.velocity = transform.forward * bulletSpeed;
        lastPos = transform.position;
        frames = 0;
    }



    private void FixedUpdate()
    {
        frames++;
        if (frames <= 1)
            return;

        RaycastHit hit;
        var dist = (transform.position - lastPos).magnitude;
        bool doesHit = Physics.Raycast(lastPos, transform.forward, out hit, dist);
        if (doesHit)
        {
            if (hit.collider.gameObject != gameObject)
            {
                var dam = hit.collider.GetComponent<IDamagable>();
                if (dam != null)
                {
                    dam.TakeDamage(bulletDamage);
                }
                Destroy(gameObject);
            }
        }

        lastPos = transform.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;


        IDamagable damagable = other.GetComponent<IDamagable>();

        if (damagable != null)
            damagable.TakeDamage(bulletDamage);

        if (other.CompareTag("Player"))
        {
            SendHitSignedAngle();
        }

        Destroy(gameObject);
    }

    private void SendHitSignedAngle()
    {
        var val1 = GameManager.instance.GetPlayerObj().transform;

        var otherDir = new Vector3(-transform.forward.x, 0f - transform.forward.z);
        var playFwd = Vector3.ProjectOnPlane(val1.forward, Vector3.up);

        var angle = Vector3.SignedAngle(playFwd, otherDir, Vector3.up);

        //var dirToSelf = transform.position - GameManager.instance.GetPlayerPOS();
        //float ang = Vector3.SignedAngle(GameManager.instance.GetPlayerObj().transform.forward, dirToSelf, Vector3.up);
        DamageIndicator.Instance.ReceiveAngle(angle);
    }
}
