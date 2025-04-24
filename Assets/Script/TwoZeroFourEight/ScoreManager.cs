using UnityEngine;

namespace TwoZeroFourEight
{
    public class ScoreManager : MonoBehaviour
    {
        public int CurrentScore { get; private set; }
        public int HighScore { get; private set; }
        
        private UIManager uiManager;

        private void Start()
        {
            HighScore = PlayerPrefs.GetInt("HighScore", 0);
            
            uiManager = FindAnyObjectByType<UIManager>();
            
            ResetScore();
        }

        public void AddScore(int points)
        {
            CurrentScore += points;
            
            if (CurrentScore > HighScore)
            {
                HighScore = CurrentScore;
                PlayerPrefs.SetInt("HighScore", HighScore);
                PlayerPrefs.Save();
            }
            
            if (uiManager != null)
            {
                uiManager.UpdateScoreDisplay(CurrentScore);
                uiManager.UpdateHighScoreDisplay();
            }
        }

        public void ResetScore()
        {
            CurrentScore = 0;
            
            if (uiManager != null)
            {
                uiManager.UpdateScoreDisplay(CurrentScore);
            }
        }
    }
}