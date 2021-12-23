using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour
{
    GameObject gameSystem;
    Animator animator;

    public void Start()
    {
        gameSystem = GameObject.Find("EventSystem");
        animator = GetComponent<Animator>();
    }
    public void takeDamage(float damage)
    {
        gameSystem.GetComponent<GameSystem>().enemyHit(damage);
        animator.SetTrigger("gotHit");
    }
    public void takeExecution(Vector3 playerPos)
    {
        gameSystem.GetComponent<GameSystem>().enemyExecute(playerPos, transform.position);
    }
}
