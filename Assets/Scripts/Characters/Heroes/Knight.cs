using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Knight : Hero
{
    [SerializeField] Sword sword;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sword = GetComponentInChildren<Sword>();
    }
    void Update()
    {
        timeLastAttack += Time.deltaTime;
        FindNearestTarget();
        if (target != null) Attack();
        Show();
        CheckDead();
    }
    public void Attack()
    {
        float disFromEnemy = Vector2.Distance(target.transform.position, transform.position);
        if (disFromEnemy < attackRange && timeLastAttack > coolDown)
        {
            sword.Attack();

            timeLastAttack = 0;
        }
    }
}
