using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAttack : Attack, IPooledObject
{
    [SerializeField] public Vector3 direction;
    [SerializeField] public float speed = 5f;
    [SerializeField] public float lifeTime = 1f;
    [SerializeField] float time = 0f;

    public void OnObjectSpawn()
    {
        gameObject.SetActive(false);
        Debug.Log("false");
    }
    public void OnObjectSpawn(Vector3 targetPos)
    {
        direction = targetPos - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        direction.z = 0f;
        direction.Normalize();
    }
    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        time += Time.deltaTime;
        if (time > lifeTime)
        {
            time = 0f;
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            time = 0f;
            gameObject.SetActive(false);
        }
    }
}
