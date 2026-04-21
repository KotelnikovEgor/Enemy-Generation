using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoints;

    private readonly float _spawnTime = 2f;

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
            CreateEnemy();
        }
    }

    private void CreateEnemy()
    {
        SpawnPoint spawnPoint = GetSpawnPoint();

        spawnPoint.GetEnemy();
    }

    private SpawnPoint GetSpawnPoint()
    {
        int index = Random.Range(0, _spawnPoints.Length);
        return _spawnPoints[index];
    }
}
