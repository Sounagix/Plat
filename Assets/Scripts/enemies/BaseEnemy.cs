using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField]
    protected float life;

    private MeshRenderer meshRenderer;


    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public virtual void ReciveDamage(float dmg)
    {
        life -= dmg;
        meshRenderer.material.color = Color.red;
        Invoke(nameof(BackColor), 1.5f);

        if (life <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected virtual void BackColor()
    {
        meshRenderer.material.color = Color.white;
    }
}
