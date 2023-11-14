using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    [SerializeField]
    private float CD;

    private bool canAttack = true;

    [SerializeField]
    private float damage;


    public override void ReciveDamage(float dmg)
    {
        life -= (dmg / 2);
        meshRenderer.material.color = Color.red;
        Invoke(nameof(BackColor), 1.5f);

        if (life <= 0)
        {
            Die();
        }
    }
    public virtual void Attack()
    {
        CancelInvoke();
        if (target != null)
        {
            target.ReciveDamage(damage);
        }
        canAttack = false;

        Invoke(nameof(BackAttackCD), CD);
    }

    private void BackAttackCD()
    {
        canAttack = true;
    }

    protected bool CanAttack()
    {
        return true;
    }
}
