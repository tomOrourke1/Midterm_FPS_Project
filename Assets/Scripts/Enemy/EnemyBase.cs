using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{

    [SerializeField] 
    protected HealthPool health;

    [SerializeField] 
    protected GameObject bullet;


    [SerializeField] protected Renderer enemyMeshRenderer;
    [SerializeField] protected Transform enemyHeadPos;
    [SerializeField] protected Transform enemyShootPos;


    protected StateMachine stateMachine;


    protected bool enemyEnabled;
}
