using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastEnemy : Attack
{
    [SerializeField]
    private GunBehaviour gunBehaviour;

    public override void AttackEnemy()
    {
        CancelInvoke();
        if (target != null)
        {
            gunBehaviour.ShootEnemy(target.transform.position, transform.position);
        }
        canAttack = false;

        Invoke(nameof(BackAttackCD), CD);
    }
}
