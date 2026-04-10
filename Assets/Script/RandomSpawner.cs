using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnEntry
    {
        public GameObject prefab;
        [Range(0f, 100f)] public float spawnChance;

        [Header("Y Range")]
        public float minY;
        public float maxY;
    }

    [Header("Spawn Settings")]
    [SerializeField] private SpawnEntry[] spawnEntries;
    [SerializeField] private Transform spawnPoint;

    [Header("Random Spawn Time")]
    [SerializeField] private float minSpawnInterval = 0.8f;
    [SerializeField] private float maxSpawnInterval = 2.5f;

    private float timer;
    private float currentSpawnInterval;

    private void Start()
    {
        SetNextSpawnTime();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= currentSpawnInterval)
        {
            timer = 0f;
            TrySpawn();
            SetNextSpawnTime();
        }
    }

    private void SetNextSpawnTime()
    {
        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    private void TrySpawn()
    {
        float totalChance = 0f;

        for (int i = 0; i < spawnEntries.Length; i++)
        {
            if (spawnEntries[i].prefab != null)
            {
                totalChance += spawnEntries[i].spawnChance;
            }
        }

        if (totalChance <= 0f)
            return;

        float randomValue = Random.Range(0f, totalChance);
        float current = 0f;

        for (int i = 0; i < spawnEntries.Length; i++)
        {
            if (spawnEntries[i].prefab == null)
                continue;

            current += spawnEntries[i].spawnChance;

            if (randomValue <= current)
            {
                Vector3 spawnPosition = spawnPoint != null ? spawnPoint.position : transform.position;

                spawnPosition.y = Random.Range(
                    spawnEntries[i].minY,
                    spawnEntries[i].maxY
                );

                Instantiate(spawnEntries[i].prefab, spawnPosition, Quaternion.identity);
                return;
            }
        }
    }
}