using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour
{
    public void takeDamage(float damage)
    {
        Debug.Log("Received" + damage + "damage.");
    }
}
