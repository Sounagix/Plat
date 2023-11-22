using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    protected Player target;

    [SerializeField]
    protected float CD;

    [SerializeField]
    protected float damage, rangeAttack;

    protected bool canAttack = true;

    public virtual void AttackEnemy()
    {
        
    }

    public virtual bool CanAttack()
    {
        return canAttack;
    }

    public virtual bool OnAttackRange()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            return distance <= rangeAttack;
        }
        else
        {
            return false;
        }
    }

    protected virtual void BackAttackCD()
    {
        canAttack = true;
    }

    public void SetTarget(Player _player)
    {
        target = _player;
    }

    public float GetRangeAttack()
    {
        return rangeAttack;
    }
}
