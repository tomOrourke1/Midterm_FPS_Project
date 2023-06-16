using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class WindKinesis : KinesisBase
{
    [SerializeField] float windRange;
    [SerializeField] float windRadius;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Fire()
    {
       /* RaycastHit hit;
        if (Physics.SphereCast(Camera.main.transform.position, windRadius, Camera.main.transform.forward, out hit, windRange)) 
        {
         IDamagable damagable = hit.collider.GetComponent<IDamagable>();
          if (damagable != null)
            {
               
            }
        }*/
    }
    
}
