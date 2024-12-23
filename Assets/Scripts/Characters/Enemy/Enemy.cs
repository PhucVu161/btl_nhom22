using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IPooledObject
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Animator animator;
    [SerializeField] Image hpBarImage;

    [Header("Stat")]
    [SerializeField] public int hp = 10;
    [SerializeField] public int maxHp = 10;
    [SerializeField] public int def = 0;
    [SerializeField] public int atk = 5;
    [SerializeField] public int atkToCrytal = 10;
    [SerializeField] int gold = 7;
    [SerializeField] int score = 100;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float baseMoveSpeed = 2f;
    [SerializeField] Transform moveCheck;
    [SerializeField] Vector2 moveCheckSize = new Vector2(1, 1);

    [Header("Attack")]
    [SerializeField] protected GameObject target;
    [SerializeField] protected float attackRange = 7f;
    [SerializeField] protected float coolDown = 1.5f;
    [SerializeField] protected float timeLastAttack = 2f;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero's Attack"))
        {
            Attack attack = collision.GetComponent<Attack>();
            TakeDamage(attack.damage);
            StartCoroutine(TakeDamageEffect());
        }
    }
    public void TakeDamage(int amount)
    {
        hp -= amount - def;
    }
    public void OnObjectSpawn()
    {
        hp = maxHp;
        spriteRenderer.color = Color.white;
        moveSpeed = baseMoveSpeed;

        GameManager.instance.activeEnemies.Add(gameObject);
    }
    protected bool CheckObstacleInFront()
    {
        
        Collider2D[] colliders = Physics2D.OverlapBoxAll(moveCheck.position, new Vector2(1, 1), 0f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out Hero hero) && hero.canBlockEnemy)
            {
                animator.SetBool("isBlocked", true);
                return true;
            }
        }
        animator.SetBool("isBlocked", false);
        return false;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(moveCheck.position, new Vector3(moveCheckSize.x, moveCheckSize.y, 0f));
    }
    protected void Move()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }
    protected void Show()
    {
        Vector3 ObjectTransform = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        transform.position = ObjectTransform;
        hpBarImage.fillAmount = (float)hp / maxHp;
    }
    protected void FindNearestTargetRangeAttack()
    {
        float minDistance = 50;
        
        if (GameManager.instance.activeHeroes.Count > 0)
        {
            foreach (GameObject hero in GameManager.instance.activeHeroes)
            {
                if (hero != null)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, hero.transform.position);
                    if (distanceToTarget < minDistance)
                    {
                        minDistance = distanceToTarget;
                        target = hero;
                    }
                }
                
            }
        }
        else target = null;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    private IEnumerator TakeDamageEffect()
    {
        if (hp > 0)
        {
            spriteRenderer.color = Color.red;
            moveSpeed -= 0.8f;

            yield return new WaitForSeconds(0.2f);

            spriteRenderer.color = Color.white;
            moveSpeed = baseMoveSpeed;
        }
        
    }
    protected void CheckDead()
    {
        if (hp <= 0)
        {
            GameManager.instance.IncreaseScore(score);
            GameManager.instance.IncreaseGold(gold);
            GameManager.instance.activeEnemies.Remove(gameObject);

            gameObject.SetActive(false);
        }
    }
}
