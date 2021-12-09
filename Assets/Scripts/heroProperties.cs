using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroProperties : MonoBehaviour
{
    // move and jump
    public float moveSpeed;
    public float jumpHeight;

    // attack
    public Transform attackPoint;
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;
    public float defense;
    public float attackRecover;

    // defense

    // spells
    public void castSpell1()
    {
        Debug.Log("Berserker's Call");
    }

    public void castSpell2()
    {
        Debug.Log("Battle Hunger");
    }
    public void castPassive()
    {
        Debug.Log("Counter Helix");
    }
    public void castUltimate()
    {
        Debug.Log("Culling Blade");
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
