using UnityEngine;
using UnityEngine.Pool;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Target _target;

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

    public void GetEnemy()
    {
        _pool.Get();
    }

    private void PerformActionOnGet(Enemy enemy)
    {
        enemy.transform.SetPositionAndRotation(transform.position + _additionalPosition, Quaternion.identity);
        enemy.SetTarget(_target);
        enemy.gameObject.SetActive(true);
        enemy.CaughtTarget += ReleaseEnemy;
    }

    private void ReleaseEnemy(Enemy enemy)
    {
        _pool.Release(enemy);
        enemy.CaughtTarget -= ReleaseEnemy;
    }
}
