using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Walker : MeleeEnemy
{
    [SerializeField]
    private Vector3 dir;

    [SerializeField]    
    private float speed;


    private void Update()
    {
        transform.position = transform.position + (dir * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Limits"))
        {
            dir *= -1;
        }
    }
}
