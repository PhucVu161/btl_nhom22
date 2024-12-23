using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Singleton

    public static EnemySpawner instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    ObjectPooler objectPooler;
    GameManager gameManager;

    [Serializable]
    public class NomalSpawn
    {
        public GameObject prefab;
    }
    [Serializable]
    public class SpecialSpawn
    {
        public GameObject prefab;
    }

    public List<NomalSpawn> nomalSpawnList;
    public List<SpecialSpawn> specialSpawnList;

    private void Start()
    {
        objectPooler = ObjectPooler.instance;
        gameManager = GameManager.instance;
        enemiesSpawned = 0;
        canSpawn = false;
    }

    public int enemiesSpawned = 0;
    public int maxEnemiesAmount = 5;
    public bool canSpawn = false;
    public bool noEnemiesLeft = false;
    public float spawnDelay = 2.5f;
    public float minY = -4.25f;
    public float maxY = 6f;

    public void SetValue(int maxEnemiesAmount, bool canSpawn, float spawnDelay)
    {
        enemiesSpawned = 0;
        this.maxEnemiesAmount = maxEnemiesAmount;
        this.canSpawn = canSpawn;
        if (spawnDelay < 0.75f) this.spawnDelay = 0.75f;
        else this.spawnDelay = spawnDelay;
    }

    public float time = 99;
    private void Update()
    {
        if (canSpawn) 
            SpawnEnemy();

        if (gameManager.activeEnemies.Count == 0) 
            noEnemiesLeft = true;
        else 
            noEnemiesLeft = false;
    }

    float yPosition;
    int enemyIndex;
    public bool harderPeriod = false;
    void SpawnEnemy()
    {
        time += Time.deltaTime;
        if (time > spawnDelay && canSpawn)
        {
            if (harderPeriod && enemiesSpawned % 5 == 0 && specialSpawnList.Count > 0)
            {
                enemiesSpawned++;
                yPosition = UnityEngine.Random.Range(minY, maxY);
                enemyIndex = UnityEngine.Random.Range(0, specialSpawnList.Count);
                Vector3 spawnPosition = new Vector3(transform.position.x, yPosition, 0f);

                objectPooler.SpawnFromPool(specialSpawnList[enemyIndex].prefab.name, spawnPosition, Quaternion.identity);

                time = 0;
            }
            else if (nomalSpawnList.Count > 0)
            {
                enemiesSpawned++;
                yPosition = UnityEngine.Random.Range(minY, maxY);
                enemyIndex = UnityEngine.Random.Range(0, nomalSpawnList.Count);
                Vector3 spawnPosition = new Vector3(transform.position.x, yPosition, 0f);

                objectPooler.SpawnFromPool(nomalSpawnList[enemyIndex].prefab.name, spawnPosition, Quaternion.identity);

                time = 0;
            }
            else Debug.Log("Nothing to spawn");
        }
    }
}
