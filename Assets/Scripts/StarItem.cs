using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StarItem : MonoBehaviour
{
    [SerializeField]
    private float velocity;


    private void Start()
    {
        StarManager.instance.AddStar();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StarManager.instance.TakeStar();
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * velocity * Time.deltaTime);
    }
}
