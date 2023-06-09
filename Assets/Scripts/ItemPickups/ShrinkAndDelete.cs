using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ShrinkAndDelete : MonoBehaviour
{

    [SerializeField] float shrinkTime = 1;
    [SerializeField] AnimationCurve curve;
    [SerializeField] GameObject model;
    [SerializeField] SphereCollider[] colliders;

    [Header("Particles?")]
    [SerializeField] ParticleSystem[] particles;

    private bool isShrinking;
    //private int i;

    //private void Start()
    //{
    //    foreach (SphereCollider coll in gameObject.GetComponentsInChildren<SphereCollider>())
    //    {
    //        colliders[i] = coll;
    //        i++;
    //    }
    //}

    public void Shrink()
    {
        if (!isShrinking)
        {
            foreach (var collider in colliders)
            {
                
                collider.enabled = false;
            }

            StartCoroutine(DecreaseAndDelete());
        }
    }

    private IEnumerator DecreaseAndDelete()
    {
        float t = shrinkTime;

        while (t > 0)
        {
            t -= Time.deltaTime;

            float scaleSizeXYZ = curve.Evaluate(t);

            model.transform.localScale = new Vector3(scaleSizeXYZ, scaleSizeXYZ, scaleSizeXYZ);

            if (particles.Length > 0)
            {
                foreach (var p in particles)
                {
                    p.transform.localScale = new Vector3(scaleSizeXYZ, scaleSizeXYZ, scaleSizeXYZ);
                }
            }

            yield return 0;
        }

        Destroy(gameObject);
    }
}
