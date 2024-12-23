using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Attack
{
    [SerializeField] public Animator animator;
    [SerializeField] PolygonCollider2D hitBox;
    private void Start()
    {
        animator = GetComponent<Animator>();
        hitBox = GetComponent<PolygonCollider2D>();
        hitBox.enabled = false;
    }
    public void Attack()
    {
        animator.Play("sword_idle");
        animator.Play("sword_attack");
    }
}
