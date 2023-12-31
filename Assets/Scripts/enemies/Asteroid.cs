using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float damage;

    private Vector2 dir;

    private float velocity;

    private Rigidbody rb;

    [SerializeField]
    private ParticleSystem ptc;

    [SerializeField]
    private AudioClip explotion;

    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    public void InitAsteroid(float t,Vector2 _dir, float v)
    {
        dir = _dir;
        velocity = v;
        Invoke(nameof(StartAsteroid), t);
    }

    private void StartAsteroid()
    {
        rb.isKinematic = false;
        rb.AddForce(dir * velocity,ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().ReciveDamage(damage);
        }
        audioSource.PlayOneShot(explotion);
        ptc.Play();
        Destroy(gameObject,1);
    }
}
