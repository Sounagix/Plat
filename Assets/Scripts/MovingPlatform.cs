using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Vector3 dir;

    [SerializeField]
    private float velocity;

    [SerializeField]
    private int distance;

    private Vector3 initPosition;

    private void Start()
    {
        initPosition = transform.position;
    }


    private void Update()
    {
        transform.position = transform.position + (dir * velocity * Time.deltaTime);
        float d = Vector3.Distance(transform.position, initPosition); 
        if (d > distance)
        {
            dir *= -1;
        }
    }
}
