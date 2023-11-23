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
        if (target)
        {
            Vector3 dir = Vector3.Normalize(target.transform.position - transform.position);
            dir.y = 0;
            transform.LookAt(dir);
            Debug.DrawRay(transform.position, dir * 10, Color.red, 1.0f);
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance > attack.GetRangeAttack() && !SetPosition(target.transform.position))
            {
                BackToPatroll();
            }
            else if (attack.CanAttack() && attack.OnAttackRange())
            {
                attack.AttackEnemy();
            }
            else if (distance > attack.GetRangeAttack() - 1 && !SetPosition(target.transform.position))
            {

            }
        }
        else
        {
            if (!waiting && ReachDestination() && sTATES.Equals(STATES.PATROLLING))
            {
                waiting = true;
                float t = Random.Range(timeToWait.x, timeToWait.y);
                Invoke(nameof(TakeNewPath), t);
            }
            else if (!IsInvoking()) 
            {
                waiting = true;
                float t = Random.Range(timeToWait.x, timeToWait.y);
                Invoke(nameof(TakeNewPath), t);
            }
        }
    }

    private void BackToPatroll()
    {
        GetComponent<MeshRenderer>().material.color = Color.green;
        target = null;
        sTATES = STATES.PATROLLING;
        attack.SetTarget(null);
        waiting = true;
        float t = Random.Range(timeToWait.x, timeToWait.y);
        Invoke(nameof(TakeNewPath), t);
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
                GetComponent<MeshRenderer>().material.color = Color.red;
                target = other.GetComponent<Player>();
                attack.SetTarget(target);
                sTATES = STATES.ENEMY_SEEN;
            }
            else
            {
                BackToPatroll();
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!SetPosition(other.transform.position))
            {
                BackToPatroll();
            }
        }
    }
}
