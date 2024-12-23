using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : GameState
{
    EnemySpawner enemySpawner = EnemySpawner.instance;
    GameManager gameManager = GameManager.instance;
    public override void OnEnter()
    {
        enemySpawner.canSpawn = true;
        gameManager.waveAlarmCanvas.Show();
    }
    public override void OnExit()
    {
        
    }
    public override void OnUpdate()
    {
        //Debug.Log("In wave");
        if (enemySpawner.enemiesSpawned >= enemySpawner.maxEnemiesAmount)
        {
            enemySpawner.canSpawn = false;
            //Debug.Log("there are enemy left");
            if (enemySpawner.noEnemiesLeft == true)
            {
                Debug.Log("no enemy left");
                StateMachine.instance.ChangeState(StateName.Breaktime);
            }
        }
    }
}