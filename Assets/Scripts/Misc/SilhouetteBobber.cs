using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilhouetteBobber : MonoBehaviour, IDamagable
{


    [SerializeField] float speed;
    [SerializeField] float offSet;


    float yCurr;
    float time;
    bool isJumping;

    int frames;

    public float GetCurrentHealth()
    {
        return 0;
    }

    public void TakeDamage(float dmg)
    {
        isJumping = true;
        time = 0;
        frames = 0;
    }

    private void Start()
    {
        yCurr = transform.position.y;
    }


    private void Update()
    {
        if(isJumping)
        {
            var newPos = (Mathf.Sin(time * speed) * offSet) + yCurr;
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
