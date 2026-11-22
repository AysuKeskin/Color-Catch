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
        if (Time.time >= spawnTimer)
        {
            ballPool.Spawn();
            spawnTimer += spawnInterval;
        }
    }
}
