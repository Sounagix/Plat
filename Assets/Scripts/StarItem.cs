using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StarItem : MonoBehaviour
{
    [SerializeField]
    private float velocity;

    private AudioSource audioSource;

    private Collider collider;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<Collider>();
        StarManager.instance.AddStar();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StarManager.instance.TakeStar();
            audioSource.Play();
            collider.enabled = false;
            Destroy(gameObject, 1);
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * velocity * Time.deltaTime);
    }
}
