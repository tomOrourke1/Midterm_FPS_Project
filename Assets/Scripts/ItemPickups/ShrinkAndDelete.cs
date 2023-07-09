using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkAndDelete : MonoBehaviour
{
    [SerializeField] float shrinkTime;
    [SerializeField] AnimationCurve curve;
    [SerializeField] GameObject model;
    [SerializeField] SphereCollider[] colliders;

    private bool isShrinking;

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

            yield return 0;
        }
        Destroy(gameObject);
    }
}
