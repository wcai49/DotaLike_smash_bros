using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroProperties : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;
    public float defense;

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
    
}
