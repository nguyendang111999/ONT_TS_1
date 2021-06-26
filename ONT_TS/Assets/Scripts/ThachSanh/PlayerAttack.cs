using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator anim;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack(){

        // Detect enemy in rang
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        // Apply damage to enemy
        foreach(Collider enemy in hitEnemies){
            Debug.Log("We hit: " + enemy.name);
        }
    }
}
