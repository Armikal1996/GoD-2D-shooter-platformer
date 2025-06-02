using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public int spawnCount = 3;
    public Vector2 spawnMin;
    public Vector2 spawnMax;

    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 randomPos = new Vector2(Random.Range(spawnMin.x, spawnMax.x), Random.Range(spawnMin.y, spawnMax.y));
            Instantiate(monsterPrefab, randomPos, Quaternion.identity);
        }
    }
}
