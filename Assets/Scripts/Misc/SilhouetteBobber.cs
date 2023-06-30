using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilhouetteBobber : MonoBehaviour, IDamagable
{


    [SerializeField] float speed;
    [SerializeField][Range(0.5f, 2.0f)] float offSetMax;
    [SerializeField][Range(0.1f, 0.5f)] float offSetMin;


    float yCurr;
    float time;
    bool isJumping;
    int frames;

    float jumpOffset;


    public float GetCurrentHealth()
    {
        return 0;
    }

    public void TakeDamage(float dmg)
    {
        isJumping = true;
        time = 0;
        frames = 0;
        jumpOffset = Random.Range(offSetMin, offSetMax);
    }
    public void TakeIceDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void TakeElectroDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void TakeFireDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    public void TakeLaserDamage(float dmg)
    {
        TakeDamage(dmg);
    }
    private void Start()
    {
        yCurr = transform.position.y;
    }


    private void Update()
    {
        if(isJumping)
        {
            var newPos = (Mathf.Sin(time * speed) * jumpOffset) + yCurr;
            var pos = transform.position;
            pos.y = newPos;

            if (newPos <= yCurr && frames > 0)
            {
                pos.y = yCurr;
                isJumping = false;
            }

            transform.position = pos;

            time += Time.deltaTime;
            
            frames++;
        }
    }
}
