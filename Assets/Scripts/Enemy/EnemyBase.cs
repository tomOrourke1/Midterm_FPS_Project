using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{

    [SerializeField] 
    protected HealthPool health;


    [SerializeField] protected Renderer enemyMeshRenderer;


    [SerializeField] protected float viewConeAngle;
    [SerializeField] protected float eyeOffset = 1f;



    protected StateMachine stateMachine;


    protected bool enemyEnabled;

    protected Color enemyColor;

    protected bool hasLanded;
    protected bool isDead;

    public bool Landed => hasLanded;
    public bool IsDead => isDead;

    public HealthPool HealthPool => health;


    protected float GetDistToPlayer()
    {
        return Vector3.Distance(GameManager.instance.GetPlayerObj().transform.position, gameObject.transform.position);
    }


    protected Vector3 GetDirToPlayer()
    {
        return (GameManager.instance.GetPlayerPOS() - (transform.position + (Vector3.up * eyeOffset))).normalized;
    }

    protected float GetAngleToPlayer()
    {
        return Vector3.Angle(GetDirToPlayer(), gameObject.transform.forward);
    }


    protected bool GetDoesSeePlayer()
    {

        var dir = GetDirToPlayer();
        var angle = GetAngleToPlayer();

        RaycastHit hit;
        if (Physics.Raycast((transform.position + (Vector3.up * eyeOffset)), dir, out hit) && (angle <= viewConeAngle))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }
    protected void RotToPlayer()
    {
        var dir = GetDirToPlayer();
        dir.y = 0;
        dir.Normalize();

        transform.rotation = Quaternion.LookRotation(dir);
    }

    protected void SetFacePlayer()
    {

        var dir = GetDirToPlayer();
        dir.y = 0;
        dir.Normalize();
        transform.rotation = Quaternion.LookRotation(dir);
    }

    Vector3 SamplePoint(Vector3 point)
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(point, out hit, 10, 1);
        return hit.position;
    }


}
