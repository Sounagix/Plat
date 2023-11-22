using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

// Clase para tomar desiciones
public class Brain : Walker
{
    [SerializeField]
    [Tooltip("Rango para detectar al enemigo")]
    private float range;

    [SerializeField]
    [Tooltip("Trigger que detecta enemigo")]
    private SphereCollider rangeSphere;

    protected Attack attack;

    private void Awake()
    {
        rangeSphere.radius = range;
        agent = GetComponent<NavMeshAgent>();
        platformCollider = plataform.GetComponent<BoxCollider>();
        attack = GetComponent<Attack>();
    }
    protected void SetUp()
    {
        StartMovement();
        ManageState();
    }

    //protected void SetUp(Attack _attack) 
    //{ 
    //    attack = _attack;
    //}


    private void Update()
    {
        if (!target && !waiting && agent.hasPath && sTATES.Equals(STATES.PATROLLING))
        {
            var pS = agent.pathStatus;
            if (pS.Equals(NavMeshPathStatus.PathComplete))
            {
                waiting = true;
                float t = Random.Range(timeToWait.x, timeToWait.y);
                attack.Invoke(nameof(TakeNewPath), t);
            }
        }
        else if (target != null && attack.CanAttack() && attack.OnAttackRange())
        {
            attack.AttackEnemy();
        }
        else if(target != null && !SetPosition(target.transform.position))
        {
            target = null;
            StartMovement();
        }
    }



    private void ManageState()
    {
        switch (sTATES)
        {
            case STATES.PATROLLING:
                break;
            case STATES.ENEMY_SEEN:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (SetPosition(other.transform.position))
            {
                target = other.GetComponent<Player>();
                attack.SetTarget(target);
                sTATES = STATES.ENEMY_SEEN;
            }
            
        }
    }
}
