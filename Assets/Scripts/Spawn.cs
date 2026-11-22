using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Spawn : MonoBehaviour
{
    [SerializeField] private Ball prefab;
    [SerializeField] private Player player;
    [SerializeField] private float spawnInterval = 0.1f;
    private float spawnTimer = 0.2f;
    public List<Ball> SpawnedBalls { get; set; } = new List<Ball>();
    [SerializeField] private  Pooling pooling;



    void Update()
    {
        if (LevelManager.GetLevelState() != LevelState.InProgress)
            return;
        if (Time.time >= spawnTimer)
        {
            SpawnBall();
            spawnTimer += spawnInterval; 
        }
    }
    private void SpawnBall()
    {
        Ball spawnedObject = pooling.GetBall();
        SpawnedBalls.Add(spawnedObject);
    }
    
}