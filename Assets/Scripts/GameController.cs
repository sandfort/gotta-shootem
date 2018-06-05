using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float PlayAreaWidth;
    public float MinimumTimeBetweenSpawns;
    public float MaximumTimeBetweenSpawns;

    private float _timeSinceLastSpawn;
    private float _timeUntilNextSpawn;

    private void Start()
    {
        _timeSinceLastSpawn = 0f;
        _timeUntilNextSpawn = MaximumTimeBetweenSpawns;
    }

    private void Update()
    {
        _timeSinceLastSpawn += Time.deltaTime;

        if (_timeSinceLastSpawn < _timeUntilNextSpawn) return;
        ResetTimer();
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        var max = PlayAreaWidth / 2 - 1;
        var min = -max;
        Instantiate(EnemyPrefab, new Vector2(Random.Range(min, max), 6), Quaternion.identity);
    }

    private void ResetTimer()
    {
        _timeSinceLastSpawn = 0f;
        _timeUntilNextSpawn = Random.Range(MinimumTimeBetweenSpawns, MaximumTimeBetweenSpawns);
    }
}