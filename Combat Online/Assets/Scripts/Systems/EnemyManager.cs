using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Enemy enemyAttackPrefab;
    [SerializeField] private Enemy enemySkillPrefab;
    [SerializeField] private EnemyManagerConfig config;
    [SerializeField] private Transform[] spawnPoints;

    private int currentEnemy = 0;

    private void Start()
    {
        StartCoroutine(RepeatSpawnEnemy());
    }

    IEnumerator RepeatSpawnEnemy()
    {
        yield return new WaitUntil(() => currentEnemy < config.MaxEnemy);
        int rate = Random.Range(0, 100);
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Enemy nextEnemy;
        if (rate < config.EnemySkillRate)
        {
            nextEnemy = Instantiate(enemySkillPrefab, spawnPoints[spawnPointIndex].position, Quaternion.identity);
        }
        else
        {
            nextEnemy = Instantiate(enemyAttackPrefab, spawnPoints[spawnPointIndex].position, Quaternion.identity);
        }
        currentEnemy++;
        nextEnemy.OnDead += () =>
        {
            Destroy(nextEnemy.gameObject, config.DelayDestroy);
            currentEnemy--;
        };
        float delayTime = Random.Range(config.MinDelayTime, config.MaxDelayTime);
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(RepeatSpawnEnemy());
    }
}
