using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Pool;
using System;
public class Pool{
    [SerializeField] private int poolSize = 10;
    private ObjectPool<Ball> ballPool;


    public Pool(Func<Ball> Create, Action<Ball> OnGet, Action<Ball> OnRelease){
        ballPool = new ObjectPool<Ball>(Create, OnGet, OnRelease);
    }

    public Ball GetBall()
    {
        Ball ball = ballPool.Get();
        return ball;
    }

    public void ReturnBall(Ball ball)
    {
        ballPool.Release(ball);
    }
    public void Prewarm()
    {
        HashSet<Ball> prewarmedBalls = new HashSet<Ball>();
        for (int i = 0; i < poolSize; i++)
        {
            Ball ball = ballPool.Get();
            prewarmedBalls.Add(ball);
            
        }
        foreach(Ball ball in prewarmedBalls)
        {
            ballPool.Release(ball);
        }
    }
}