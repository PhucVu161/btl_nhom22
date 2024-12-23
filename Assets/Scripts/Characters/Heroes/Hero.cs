using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour, IPooledObject
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] Image hpBarImage;
    [SerializeField] public bool canBlockEnemy;

    [Header("Stat")]
    [SerializeField] public int hp = 50;
    [SerializeField] public int maxHp = 50;
    [SerializeField] public int def = 0;
    [SerializeField] public int atk = 5;
    [SerializeField] public int gold = 50;
    [SerializeField] int score = 100;
    [SerializeField] float invisibleTime = 1.5f;
    [SerializeField] bool isInvisible = false;
    [SerializeField] protected float moveSpeed = 0f;
    //[SerializeField] float baseMoveSpeed = 0f;

    [Header("Attack")]
    [SerializeField] protected GameObject target;
    [SerializeField] protected float attackRange = 10f;
    [SerializeField] protected float coolDown = 2f;
    [SerializeField] protected float timeLastAttack = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isInvisible)
        {
            if (collision.CompareTag("Enemy's Attack"))
            {
                Attack attack = collision.GetComponent<Attack>();
                TakeDamage(attack.damage);
                StartCoroutine(TakeDamageEffect());
            }
            if (collision.CompareTag("Enemy"))
            {
                Enemy enemy = collision.GetComponent<Enemy>();
                TakeDamage(enemy.atk);
                StartCoroutine(TakeDamageEffect());
            }
        }
    }
    public void TakeDamage(int amount)
    {
        hp -= amount - def;
    }
    public void OnObjectSpawn()
    {
        hp = maxHp;
        isInvisible = false;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);

        GameManager.instance.IncreaseScore(score);
        GameManager.instance.DecreaseGold(gold);
        GameManager.instance.activeHeroes.Add(gameObject);
    }
    protected void Show()
    {
        Vector3 ObjectTransform = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        transform.position = ObjectTransform;
        hpBarImage.fillAmount = (float)hp / maxHp;
        Flip();
    }
    protected void Flip()
    {
        if (target != null && target.transform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.identity;

        }
    }
    protected void FindNearestTarget()
    {
        float minDistance = 50;

        if (GameManager.instance.activeEnemies.Count > 0)
        {
            foreach (GameObject enemy in GameManager.instance.activeEnemies)
            {
                if (enemy != null)
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distanceToEnemy < minDistance)
                    {
                        minDistance = distanceToEnemy;
                        target = enemy;
                    }
                }
            }
        }
        else target = null;
    }
    
    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    protected IEnumerator TakeDamageEffect()
    {
        if (hp > 0)
        {
            isInvisible = true;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);

            yield return new WaitForSeconds(invisibleTime);

            isInvisible = false;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        }
    }
    protected void CheckDead()
    {
        if (hp <= 0)
        {
            GameManager.instance.activeHeroes.Remove(gameObject);
            gameObject.SetActive(false);
        }
    }
}
