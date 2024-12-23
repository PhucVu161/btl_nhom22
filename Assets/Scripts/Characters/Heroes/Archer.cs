using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Hero
{
    [SerializeField] FlyingAttack arrow;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            ObjectPooler.instance.SpawnFlyingAttackFromPool(arrow.name, transform.position, Quaternion.identity, target.transform.position);

            timeLastAttack = 0;
        }
    }
}
