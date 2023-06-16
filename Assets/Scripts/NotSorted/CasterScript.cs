using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterScript : MonoBehaviour
{
    WindKinesis aero;
    pyroBlast pyro;
    IceKinesis ice;
    LightningKinesis lightning;
    TelekinesisController telekinesis;
   
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (UIManager.instance)
        {
            case 1:
                //telekinesis.Fire();
                break;
            case 2:
            pyro.Fire();
                break;
            case 3:
                ice.Fire();
                break;
            case 4:
                lightning.Fire();
                break;
       /*     case 5:
                aero.Fire();
                break*/;




        }
    }
}
