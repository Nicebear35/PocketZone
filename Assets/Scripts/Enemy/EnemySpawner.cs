using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private int _spawnPositionsCount;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _enemySpawnCount;
    [SerializeField] private Player _player;
    [SerializeField] private RealtimeNavMeshSurface _realtimeNavMeshSurface;
    [SerializeField] private LootingSystem _lootingSystem;

    private void Awake()
    {
        _spawnPositionsCount = _spawnPoints.Count;

        for (int i = 0; i < _enemySpawnCount; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Enemy newEnemy = Instantiate(_enemyPrefab, _spawnPoints[ChoosePointToSpawn()].position, Quaternion.identity);
        newEnemy.Initialize(_player.transform, _lootingSystem);
    }

    private int ChoosePointToSpawn()
    {
        return Random.Range(0, _spawnPositionsCount);
    }
}
