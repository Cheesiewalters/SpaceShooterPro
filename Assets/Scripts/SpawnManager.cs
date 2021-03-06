﻿using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _EnemyPrefab;
    [SerializeField]
    private GameObject _EnemyContainer;

    [SerializeField]
    private GameObject _TripleShotPrefab;
    [SerializeField]
    private GameObject _SpeedPowerUp;
    [SerializeField]
    private GameObject _newPlayer;
    [SerializeField]
    private GameObject _ShieldPowerUp;

    [SerializeField]
    private GameObject _PowerUpContainer;

    private bool _stopSpawning = false;

    

    void Start()
    {
        
    }

    void Update()
    {
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnTripleShotPowerupRoutine());
        StartCoroutine(SpawnSpeedPowerUpRoutine());
        StartCoroutine(SpawnShieldPowerUpRoutine());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            GameObject newEnemy = Instantiate(_EnemyPrefab, new Vector3(Random.Range(-4, 3.4f), 4, 0), Quaternion.identity);
            newEnemy.transform.parent = _EnemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(2,5));
        }
    }

    IEnumerator SpawnTripleShotPowerupRoutine()
    {
        while (_stopSpawning == false)
        {
            GameObject newTripleShot = Instantiate(_TripleShotPrefab, new Vector3(Random.Range(-4, 3.4f), 4, 0), Quaternion.identity);
            newTripleShot.transform.parent = _PowerUpContainer.transform;
            yield return new WaitForSeconds(Random.Range(5,12));
        }
    }

    IEnumerator SpawnSpeedPowerUpRoutine()
    {
        while (_stopSpawning == false)
        {
            GameObject newSpeedPowerUp = Instantiate(_SpeedPowerUp, new Vector3(Random.Range(-4, 3.4f), 4, 0), Quaternion.identity);
            newSpeedPowerUp.transform.parent = _PowerUpContainer.transform;
            yield return new WaitForSeconds(Random.Range(5, 12));
        }
    }

    IEnumerator SpawnShieldPowerUpRoutine()
    {
        while (_stopSpawning == false)
        {
            GameObject newShieldPowerUp = Instantiate(_ShieldPowerUp, new Vector3(Random.Range(-4, 3.4f), 4, 0), Quaternion.identity);
            newShieldPowerUp.transform.parent = _PowerUpContainer.transform;
            yield return new WaitForSeconds(Random.Range(20, 25));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
