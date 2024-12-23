using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface ICharacter
{
    void Move();
    void Show();
    void Flip();
    void Shoot();
    IEnumerator TakeDamageEffect();
    void CheckDead();
}