using UnityEngine;
using TwoZeroFourEight;

namespace TwoZeroFourEight
{
    public class Two_GameManager : MonoBehaviour
    {
        public static Two_GameManager Instance;
        
        [HideInInspector] public BoardManager BoardManager;
        [HideInInspector] public InputHandler InputHandler;
        [HideInInspector] public MovementSystem MovementSystem;
        [HideInInspector] public GameStateManager GameStateManager;
        [HideInInspector] public ScoreManager ScoreManager; 
        
        public int[,] gameBoard => BoardManager.GameBoard;

        private UIManager uiManager;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                // Initialize components
                BoardManager = gameObject.AddComponent<BoardManager>();
                InputHandler = gameObject.AddComponent<InputHandler>();
                MovementSystem = gameObject.AddComponent<MovementSystem>();
                GameStateManager = gameObject.AddComponent<GameStateManager>();
                ScoreManager = gameObject.AddComponent<ScoreManager>();
                uiManager = FindFirstObjectByType<UIManager>();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            GameStateManager.StartNewGame();
        }

        public void AfterMove(bool moved, int scoreToAdd = 0)
        {
            if (moved)
            {
                // Add score if tiles were merged
                if (scoreToAdd > 0)
                {
                    ScoreManager.AddScore(scoreToAdd);
                }
                
                BoardManager.SpawnNewTile();
                GameStateManager.CheckGameState();
            }
        }
    }
}