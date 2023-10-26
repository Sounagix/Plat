using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float velocity;

    private Vector3 dir = new Vector3(-1.0f, 0.0f, 0.0f);

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (dir * velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Limits"))
        {
            dir *= -1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().ReciveDamage();
        }
    }
}
