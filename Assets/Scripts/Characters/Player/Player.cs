using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Pool;
using DG.Tweening.Core.Easing;

public class Player : MonoBehaviour
{
    #region Singleton
        public static Player instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameManager gameManager;
    SpriteRenderer spriteRenderer;
    Animator animator;

    [Header("Stat")]
    [SerializeField] public int hp;
    [SerializeField] public int maxHp = 10;
    [SerializeField] float invisibleTime = 1.5f;
    [SerializeField] bool isInvisible = false;

    [Header("Movement")]
    public bool isMoving;
    [SerializeField] float moveSpeed = 5f;
    public Vector3 worldMousePosition;

    [Header("Attack")]
    [SerializeField] FlyingAttack attack;
    [SerializeField] public float coolDown = 1f;
    [SerializeField] public float timeLastAttack = 1f;
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

    void Start()
    {
        gameManager = GameManager.instance;
        instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        timeLastAttack += Time.deltaTime;
        Move();
        Show();
        if (Input.GetMouseButton(0) && timeLastAttack > coolDown && !gameManager.isGamePaused)
        {
            Shoot();
        }
        CheckDead();
    }
    Vector3 previousPosition;
    void Move()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }

        transform.position += direction * moveSpeed * Time.deltaTime;

        if (transform.position != previousPosition) isMoving = true;
        else isMoving = false;

        previousPosition = transform.position;

        animator.SetBool("isMoving", isMoving);
    }
    private void Show() //giup cho sprite hien thi theo cu ly xa gan
    {
        Vector3 ObjectTransform = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        transform.position = ObjectTransform;
        Flip();
    }
    public void Flip()
    {
        if (Time.timeScale == 0f) return;

        worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMousePosition.z = 0f;
        if (worldMousePosition.x < transform.position.x) transform.rotation = Quaternion.Euler(0, 180, 0);
        else transform.rotation = Quaternion.identity;
    }
    public void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("Player Attack");

        ObjectPooler.instance.SpawnFlyingAttackFromPool(attack.name, transform.position, Quaternion.identity, worldMousePosition);

        timeLastAttack = 0;
    }
    private IEnumerator TakeDamageEffect()
    {
        isInvisible = true;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);

        yield return new WaitForSeconds(invisibleTime);

        isInvisible = false;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
    }
    void TakeDamage(int amount)
    {
        hp -= amount;
    }
    private void CheckDead()
    {
        if (hp <= 0)
        {
            gameManager.GameOver();
            Debug.Log("game over");
        }
    }
}
