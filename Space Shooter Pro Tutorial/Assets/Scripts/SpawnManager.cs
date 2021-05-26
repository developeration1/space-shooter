using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject _enemy;
    [SerializeField]
    GameObject _enemyContainer;
    [SerializeField]
    GameObject[] _powerups;
    [SerializeField]
    GameObject _powerupContainer;

    bool _stopSpawning;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3);
        while (!_stopSpawning)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-9f, 9f), 7, 0);
            GameObject newEnemy = Instantiate(_enemy, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3);
        while (!_stopSpawning)
        {
            yield return new WaitForSeconds(Random.Range(3f, 8f));
            Vector3 spawnPosition = new Vector3(Random.Range(-9f, 9f), 8, 0);
            GameObject newPowerup = Instantiate(_powerups[Random.Range(0, 3)], spawnPosition, Quaternion.identity);
            newPowerup.transform.parent = _powerupContainer.transform;
        }
    }

    public void OnPlayerDead()
    {
        _stopSpawning = true;
    }
}
