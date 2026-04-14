using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    private readonly float _spawnTime = 2f;
    private readonly float _limitDirection = 1f;
    private readonly Vector3 _additionalPosition = new(0, 1.5f, 0);

    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_enemyPrefab),
            actionOnGet: (enemy) => PerformActionOnGet(enemy),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false)
            );
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds seconds = new(_spawnTime);

        while (enabled)
        {
            yield return seconds;
            _pool.Get();
        }
    }

    private void PerformActionOnGet(Enemy enemy)
    {
        enemy.transform.SetPositionAndRotation(GetSpawnPosition(), Quaternion.identity);
        enemy.SetDirection(GetDirection());
        enemy.gameObject.SetActive(true);
        enemy.FellOverEdge += ReleaseEnemy;
    }

    private void ReleaseEnemy(Enemy enemy)
    {
        _pool.Release(enemy);
        enemy.FellOverEdge -= ReleaseEnemy;
    }

    private Vector3 GetSpawnPosition()
    {
        int pointIndex = Random.Range(0, _spawnPoints.Length);
        return _spawnPoints[pointIndex].position + _additionalPosition;
    }

    private Vector3 GetDirection()
    {
        return new Vector3(Random.Range(-_limitDirection, _limitDirection), 0, Random.Range(-_limitDirection, _limitDirection));
    }
}
