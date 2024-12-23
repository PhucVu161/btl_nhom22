using UnityEngine;

public class Necromancer : Enemy
{
    [SerializeField] FlyingAttack flyingAttack;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        timeLastAttack += Time.deltaTime;
        FindNearestTargetRangeAttack();
        if (target != null) Attack();
        if (CheckObstacleInFront() == false) Move();
        Show();
        CheckDead();
    }
    public void Attack()
    {
        float distanceFromPlayer = Vector2.Distance(target.transform.position, transform.position);
        if (distanceFromPlayer < attackRange && timeLastAttack > coolDown)
        {
            ObjectPooler.instance.SpawnFlyingAttackFromPool(flyingAttack.name, transform.position, Quaternion.identity, target.transform.position);

            timeLastAttack = 0;
        }
    }
}
