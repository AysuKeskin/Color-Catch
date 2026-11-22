using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallPool : MonoBehaviour
{
    [SerializeField] private Ball prefab;
    [SerializeField] private float spawnY = 6f;
    [SerializeField] private float spawnXRange = 2.4f;
    private Pool pool;
    private readonly List<Ball> activeBalls = new List<Ball>();

    public IReadOnlyList<Ball> ActiveBalls => activeBalls;
    public event Action OnBallMissed;


    void Start()
    {
        pool = new Pool(Create, OnGet, OnRelease);

        pool.Prewarm();
    }
    private Ball Create()
    {
        Ball ballInstance = Instantiate(prefab, transform);
        ballInstance.OnMissed += HandleBallMissed;
        ResetBall(ballInstance);
        return ballInstance;
    }
    private void HandleBallMissed(Ball ball)
    {
        Despawn(ball);
        OnBallMissed?.Invoke();
    }
    private void ResetBall(Ball ball)
    {
        ball.transform.position = new Vector3(Random.Range(-spawnXRange, spawnXRange), spawnY, 0f);
        ball.SetColor(Random.Range(0, ColorMap.Colors.Length));
        ball.gameObject.SetActive(false);
    }
    private void OnGet(Ball ball)
    {
        ball.gameObject.SetActive(true);
    }
    private void OnRelease(Ball ball)
    {
        ResetBall(ball);
    }

    // Takes a ball from the pool and adds it to the active list.
    public Ball Spawn()
    {
        Ball ball = pool.GetBall();
        activeBalls.Add(ball);
        return ball;
    }

    // Returns the ball to the pool and removes it from the active list.
    public void Despawn(Ball ball)
    {
        pool.ReturnBall(ball);
        activeBalls.Remove(ball);
    }
    public void ResetPool()
    {
        foreach (Ball ball in activeBalls)
        {
            pool.ReturnBall(ball);
        }
        activeBalls.Clear();
    }
}
