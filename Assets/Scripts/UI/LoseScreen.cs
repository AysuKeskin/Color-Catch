using UnityEngine;
using TMPro;
public class LoseScreen : MonoBehaviour
{
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI highestScoreText;


    void OnEnable()
    {
        LevelManager.OnLose += ShowLoseScreen;
    }
    private void ShowLoseScreen()
    {
        highestScoreText.text = "Highest Score: " + ScoreProgress.Score;
        losePanel.SetActive(true);
    }
    void OnDisable()
    {
        LevelManager.OnLose -= ShowLoseScreen;
    }


    public void RestartLevel()
    {
        losePanel.SetActive(false);
        gameManager.Restart();
    }
}