using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Clase para el ataque cuerpo a cuerpo
public class MeleeEnemy : Attack
{
    public override void AttackEnemy()
    {
        CancelInvoke();
        if (target != null)
        {
            target.ReciveDamage(damage);
        }
        canAttack = false;

        Invoke(nameof(BackAttackCD), CD);
    }
}
