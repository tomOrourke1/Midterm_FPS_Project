using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{

    [SerializeField] 
    protected HealthPool health;


    [SerializeField] protected Renderer enemyMeshRenderer;


    protected StateMachine stateMachine;


    protected bool enemyEnabled;

    protected Color enemyColor;
}
