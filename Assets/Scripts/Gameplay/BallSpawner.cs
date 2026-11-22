using UnityEngine;
using UnityEngine.Serialization;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 0.1f;
    private float spawnTimer = 0.2f;
    [FormerlySerializedAs("pooling")]
    [SerializeField] private BallPool ballPool;



    void Update()
    {
        if (LevelManager.GetLevelState() != LevelState.InProgress)
            return;
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            ballPool.Spawn();
            spawnTimer = spawnInterval;
        }
    }
}
