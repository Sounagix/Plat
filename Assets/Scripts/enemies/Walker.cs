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
        sTATES = STATES.PATROLLING;
        var coll = platformCollider.bounds;
        float x = Random.Range(coll.min.x,coll.max.x);
        float z = Random.Range(coll.min.z,coll.max.z);

        Vector3 pos = new Vector3(x, plataform.position.y, z);
        Vector3 dir = Vector3.Normalize(pos - transform.position);
        dir.y = 0;
        transform.LookAt(dir);
        agent.SetDestination(pos);
    }

    protected void TakeNewPath()
    {
        waiting = false;
        StartMovement();
    }

    protected bool SetPosition(Vector3 newPos)
    {
        return agent.SetDestination(newPos);
    }

    protected virtual void SetStopDistance()
    {
        agent.stoppingDistance = interatuableDistance;
    }
}
