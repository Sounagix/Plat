using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Clase para el movimiento
public class Walker : BaseEnemy
{
    [SerializeField]
    protected float speed;

    [SerializeField]
    protected Transform plataform;

    [SerializeField]
    protected Vector2 timeToWait;

    protected NavMeshAgent agent;

    protected bool waiting;

    protected BoxCollider platformCollider;


    // Empieza un movimiento
    protected void StartMovement()
    {
        GetComponent<MeshRenderer>().material.color = Color.green;
        sTATES = STATES.PATROLLING;
        var coll = platformCollider.bounds;
        float x = Random.Range(coll.min.x,coll.max.x);
        float z = Random.Range(coll.min.z,coll.max.z);

        Vector3 pos = new Vector3(x, plataform.position.y, z);
        Vector3 dir = Vector3.Normalize(pos - transform.position);
        dir.x = 0;
        dir.z = 0;
        transform.LookAt(dir);

        SetStopDistance();
        agent.SetDestination(pos);
    }

    protected void TakeNewPath()
    {
        waiting = false;
        StartMovement();
    }

    protected bool SetPosition(Vector3 newPos)
    {
        SetStopDistance();
        return agent.SetDestination(newPos);
    }

    protected virtual void SetStopDistance()
    {
        agent.stoppingDistance = interatuableDistance;
    }

    protected bool ReachDestination()
    {
        if (agent != null && agent.hasPath)
        {
            switch (agent.pathStatus)
            {
                case NavMeshPathStatus.PathComplete:
                    return true;
                case NavMeshPathStatus.PathPartial:
                    return false;
   
                case NavMeshPathStatus.PathInvalid:
                    return false;
                default:
                    return false;
                    
            }
        }
        else
        {
            return false;
        }
    }
}
