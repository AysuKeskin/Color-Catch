using UnityEngine;
using TMPro;
public class LoseScreen : MonoBehaviour
{
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI highestScoreText;


    void OnEnable()
    {
        LevelManager.onLose += ShowLoseScreen;
    }
    private void ShowLoseScreen()
    {
        highestScoreText.text = "Highest Score: " + ScoreProgress.Score;
        losePanel.SetActive(true);
    }
    void OnDisable()
    {
        LevelManager.onLose -= ShowLoseScreen;
    }


    public void RestartLevel()
    {
        LevelManager.SetLevelState(LevelState.InProgress);
        losePanel.SetActive(false);
        player.ResetPlayer();
    }
}