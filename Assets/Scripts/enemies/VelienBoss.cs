using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelienBoss : BaseEnemy
{
    [SerializeField]
    private Transform[] patrolls;

    [SerializeField]
    private float velocity;

    private int indexPatroll = -1;

    private bool moving = false;

    private bool attackMode = false;

    private Vector3 dir;

    private SphereCollider sphereCollider;

    private float attackCD;

    private CastEnemy castEnemy;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip movSound, shotSound;

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = interatuableDistance;
        castEnemy = GetComponent<CastEnemy>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartPatrolling();
    }


    private void StartPatrolling()
    {
        int newPatroll = -1;
        do
        {
            newPatroll = Random.Range(0, patrolls.Length);
        }
        while (newPatroll == indexPatroll);
        indexPatroll = newPatroll;
        dir = Vector3.Normalize(patrolls[indexPatroll].position - transform.position);
        moving = true;
    }

    private void Update()
    {
        if (moving)
        {
            transform.position = transform.position + (dir * velocity);
        }
        else if (attackMode && castEnemy.CanAttack())
        {
            audioSource.PlayOneShot(shotSound);
            Attack();
        }
        
    }

    private void Attack()
    {
        castEnemy.AttackEnemy();
        //Invoke(nameof(BackCD), attackCD);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BossPoint"))
        {
            StartPatrolling();
        }
        else if (other.CompareTag("Player"))
        {
            castEnemy.SetTarget(other.GetComponent<Player>());
            moving = false;
            attackMode = true;
            audioSource.loop = false;
            audioSource.Stop();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            audioSource.loop = true;
            audioSource.Play();
            castEnemy.SetTarget(null);
            moving = true;
            attackMode = false;
        }
    }
}
