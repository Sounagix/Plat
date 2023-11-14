using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private string enemyTag;

    private Rigidbody rb;

    private float damage;

    [SerializeField]
    private float destroyTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, destroyTime);
    }

    public void SetUp(string _enemyTag, float _damage, Vector3 dir, float velocity)
    {
        enemyTag = _enemyTag;
        damage = _damage;
        print(dir);
        rb.AddForce(dir * velocity, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(enemyTag))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null) 
            {
                player.ReciveDamage(damage);
            }
            else
            {
                BaseEnemy enemy = collision.gameObject.GetComponent<BaseEnemy>();
                if (enemy != null)
                {
                    enemy.ReciveDamage(damage);
                }
            }
        }

        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
