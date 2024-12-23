using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaktime : GameState
{
    GameManager gameManager = GameManager.instance;
    EnemySpawner enemySpawner = EnemySpawner.instance;
    
    public override void OnEnter()
    {
        gameManager.wave++;
        if (gameManager.wave >= 7) enemySpawner.harderPeriod = true;
        enemySpawner.SetValue
            (enemySpawner.maxEnemiesAmount + 2, false, enemySpawner.spawnDelay - 0.075f);
        gameManager.breaktimetimer = gameManager.breakTime;
        gameManager.breaktimeCanvas.ShowFadeIn();
        Debug.Log("Enter Break");
    }
    public override void OnExit()
    {
        gameManager.breaktimeCanvas.ShowFadeOut();
        Debug.Log("Exit Break");
    }
    public override void OnUpdate()
    {
        gameManager.breaktimetimer -= Time.deltaTime;
        //Debug.Log("In Break: " + GameManager.instance.breaktimetimer);

        if (gameManager.breaktimetimer < 0)
        {
            StateMachine.instance.ChangeState(StateName.Battle);
        }
    }
}
