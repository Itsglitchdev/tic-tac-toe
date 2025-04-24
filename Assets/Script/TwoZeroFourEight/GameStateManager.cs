using UnityEngine;

namespace TwoZeroFourEight
{
    public enum GameState
    {
        Playing,
        Won,
        GameOver
    }
    
    public class GameStateManager : MonoBehaviour
    {
        public GameState CurrentState { get; private set; } = GameState.Playing;
        private bool continueAfterWin = false;
        
        private UIManager uiManager;

        private void Start()
        {
            uiManager = FindAnyObjectByType<UIManager>();
        }

        public void StartNewGame()
        {
            CurrentState = GameState.Playing;
            continueAfterWin = false;
            
            Two_GameManager.Instance.ScoreManager.ResetScore();
            Two_GameManager.Instance.BoardManager.InitializeGameBoard();

            if (uiManager != null)
            {
                uiManager.HideGameOver();
                uiManager.HideWinMessage();
            }
        }

        public bool CanReceiveInput()
        {
            return CurrentState == GameState.Playing || 
                  (CurrentState == GameState.Won && continueAfterWin);
        }

        public void CheckGameState()
        {
            BoardManager boardManager = Two_GameManager.Instance.BoardManager;

            if (CurrentState != GameState.Won && boardManager.HasWinningTile())
            {
                CurrentState = GameState.Won;
                Debug.Log("You Win! Continue playing for a higher score.");
                if (uiManager != null)
                {
                    uiManager.ShowWinMessage();
                }
            }
            
            // Check for game over
            if (!boardManager.HasAvailableMoves())
            {
                CurrentState = GameState.GameOver;
                Debug.Log("Game Over!");
                
                if (uiManager != null)
                {
                    uiManager.ShowGameOver();
                }
            }
        }

        public void ContinueAfterWin()
        {
            if (CurrentState == GameState.Won)
            {
                continueAfterWin = true;
            }
        }
    }
}