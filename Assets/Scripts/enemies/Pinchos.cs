using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pinchos : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField]
    private Transform pointA, pointB;

    [SerializeField]
    private float damage, vel;


    private bool goingPointA = true;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = vel;
    }

    private void Start()
    {
        StartMovemement();
    }

    private void StartMovemement()
    {
        agent.SetDestination(goingPointA ? pointA.position : pointB.position);
        goingPointA = !goingPointA;
    }

    private void FixedUpdate()
    {
        if (!agent.hasPath && agent.pathStatus.Equals(NavMeshPathStatus.PathComplete))
        {
            StartMovemement();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().ReciveDamage(damage);
            Destroy(gameObject);
        }
    }
}
