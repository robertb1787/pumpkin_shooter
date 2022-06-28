using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PumpkinShooter.Game
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] GameSession _gameSession;

        [SerializeField] private Transform _spawnPoint = null;

        private Enemy _enemyPrefab => GameManager.Instance.gameData.enemy;

        [SerializeField] private float delayTimeSpawn = 1;

        private Enemy _lastSpawned = null;

        void Start()
        {
            StartCoroutine(DelayToSpwan());
        }

        void SpawnEnemy()
        {
            _lastSpawned = Instantiate(_enemyPrefab, _spawnPoint.position, _spawnPoint.rotation);
            _lastSpawned.onKill.AddListener(x =>
            {
                Start();
                _gameSession.AddedScore(x);
            });
        }

        IEnumerator DelayToSpwan()
        {
            yield return new WaitForSeconds(delayTimeSpawn);

            SpawnEnemy();
        }
    }
}