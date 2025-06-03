using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public int initialSpawnCount = 3;
    public int minMonsters = 2; // Minimum monsters to maintain
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;
    public float respawnCheckInterval = 5f; // How often to check for respawns

    private int currentMonsters = 0;

    void Start()
    {
        // Initial spawn
        SpawnMonsters(initialSpawnCount);

        // Start periodic respawn checks
        InvokeRepeating(nameof(CheckMonsterCount), respawnCheckInterval, respawnCheckInterval);
    }

    void SpawnMonsters(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 randomPos = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            Instantiate(monsterPrefab, randomPos, Quaternion.identity);
            currentMonsters++;
        }
    }

    void CheckMonsterCount()
    {
        // Count remaining monsters (alternative to tracking)
        int aliveMonsters = GameObject.FindGameObjectsWithTag("Enemy").Length;

        // Spawn the difference if below minimum
        if (aliveMonsters < minMonsters)
        {
            int monstersToSpawn = minMonsters - aliveMonsters;
            SpawnMonsters(monstersToSpawn);
        }
    }

    // Optional: Call this from monster's death script
    public void OnMonsterDied()
    {
        currentMonsters--;
    }
}