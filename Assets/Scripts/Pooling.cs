using UnityEngine;

public class Pooling : MonoBehaviour
{
    [SerializeField] private Ball prefab;
    [SerializeField] private Spawn spawn;
    [SerializeField] private Player player;
    private Pool pool;


    void Start()
    {
        pool = new Pool(Create, OnGet, OnRelease);

        pool.Prewarm();
    }
    private Ball Create()
    {
        Ball ballInstance = Instantiate(prefab, transform);
        ballInstance.Init(player, spawn, this);
        ballInstance.transform.position = new Vector3(Random.Range(-2.4f, 2.4f), 6f, 0f);
        ballInstance.SetColor(Random.Range(0, ColorMap.Colors.Length));
        ballInstance.gameObject.SetActive(false);
        return ballInstance;
    }
    private void OnGet(Ball ball)
    {
        ball.gameObject.SetActive(true);
    }
    private void OnRelease(Ball ball)
    {
        ball.transform.position = new Vector3(Random.Range(-2.4f, 2.4f), 6f, 0f);
        ball.SetColor(Random.Range(0, ColorMap.Colors.Length));
        ball.gameObject.SetActive(false);
    }
    public Ball GetBall()
    {
        Ball ball = pool.GetBall();
        return ball;
    }

    public void ReturnBall(Ball ball)
    {
        pool.ReturnBall(ball);
    }
    public void ResetPool()
    {
        foreach (Ball ball in spawn.SpawnedBalls)
        {
            pool.ReturnBall(ball);
        }
        spawn.SpawnedBalls.Clear();
    }
}