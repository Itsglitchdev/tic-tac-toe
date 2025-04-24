using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TwoZeroFourEight
{
    public class UIManager : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private TextMeshProUGUI gameOverText;
        [SerializeField] private Button restartButton;

        private void Start()
        {
            if (gameOverText != null)
                gameOverText.gameObject.SetActive(false);
                
            if (restartButton != null)
                restartButton.onClick.AddListener(RestartGame);
                
            UpdateHighScoreDisplay();
        }

        public void UpdateScoreDisplay(int score)
        {
            if (scoreText != null)
                scoreText.text = $"Score: {score}";
        }

        public void UpdateHighScoreDisplay()
        {
            if (highScoreText != null)
                highScoreText.text = $"Best: {PlayerPrefs.GetInt("HighScore", 0)}";
        }

        public void ShowGameOver()
        {
            if (gameOverText != null)
                gameOverText.gameObject.SetActive(true);
                gameOverText.text = "Game Over!";
        }

        public void ShowWinMessage()
        {
            if (gameOverText != null)
            {
                gameOverText.gameObject.SetActive(true);
                gameOverText.text = "You Win!";
            }
        }

        public void HideWinMessage()
        {
            if (gameOverText != null )
                gameOverText.gameObject.SetActive(false);
        }

        public void HideGameOver()
        {
            if (gameOverText != null)
                gameOverText.gameObject.SetActive(false);
        }

        private void RestartGame()
        {
            HideGameOver();
            HideWinMessage();
            Two_GameManager.Instance.GameStateManager.StartNewGame();
        }
    }
}