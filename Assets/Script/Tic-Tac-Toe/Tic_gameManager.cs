using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tic_gameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private Button resetButton;

    // Constants
    private const int BOARD_SIZE = 9;
    
    // Game state
    private PlayerTurn currentTurn;
    private PlayerTurn?[] board = new PlayerTurn?[BOARD_SIZE];
    private GameState gameState = GameState.InProgress;
    
    // Win combinations
    private readonly int[][] winCombos = new int[][]
    {
        new int[] {0, 1, 2},
        new int[] {3, 4, 5},
        new int[] {6, 7, 8},
        new int[] {0, 3, 6},
        new int[] {1, 4, 7},
        new int[] {2, 5, 8},
        new int[] {0, 4, 8},
        new int[] {2, 4, 6} 
    };

    public static event Action<PlayerTurn> OnTurnChanged;
    public static event Action<GameState, PlayerTurn?> OnGameStateChanged;

    private void Start()
    {
        InitializeGame();
    }

    private void OnEnable()
    {
        OneBox.OnBoxClicked += HandleBoxClick;
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(ResetGame);
        }
    }

    private void OnDisable()
    {
        OneBox.OnBoxClicked -= HandleBoxClick;
        if (resetButton != null)
        {
            resetButton.onClick.RemoveListener(ResetGame);
        }
    }

    private void InitializeGame()
    {
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            board[i] = null;
        }
        
        currentTurn = PlayerTurn.X;
        gameState = GameState.InProgress;
        UpdateTurnText();
        
        OnTurnChanged?.Invoke(currentTurn);
        OnGameStateChanged?.Invoke(gameState, null);
    }

    private void HandleBoxClick(int index, Button button)
    {
        if (gameState != GameState.InProgress || board[index] != null) 
            return;
        
        board[index] = currentTurn;
        UpdateBoxUI(button, currentTurn);

        if (CheckWin())
        {
            EndGame(GameState.Won);
        }
        else if (CheckDraw())
        {
            EndGame(GameState.Draw);
        }
        else
        {
            SwitchTurn();
        }
    }

    private void UpdateBoxUI(Button button, PlayerTurn playerTurn)
    {
        TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null)
        {
            text.text = playerTurn.ToString();
        }
        button.interactable = false;
    }

    private void SwitchTurn()
    {
        currentTurn = currentTurn == PlayerTurn.X ? PlayerTurn.O : PlayerTurn.X;
        UpdateTurnText();
        OnTurnChanged?.Invoke(currentTurn);
    }

    private void EndGame(GameState state)
    {
        gameState = state;
        
        if (state == GameState.Won)
        {
            turnText.text = $"Player {currentTurn} Wins!";
        }
        else if (state == GameState.Draw)
        {
            turnText.text = "It's a Draw!";
        }
        
        OnGameStateChanged?.Invoke(gameState, currentTurn);
    }

    private void ResetGame()
    {
        OneBox[] boxes = FindObjectsByType<OneBox>(FindObjectsSortMode.None);
        foreach (OneBox box in boxes)
        {
            Button button = box.GetButton();
            if (button != null)
            {
                TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
                if (text != null)
                {
                    text.text = string.Empty;
                }
                button.interactable = true;
            }
        }
        
        InitializeGame();
    }

    private bool CheckWin()
    {
        foreach (var combo in winCombos)
        {
            PlayerTurn? a = board[combo[0]];
            PlayerTurn? b = board[combo[1]];
            PlayerTurn? c = board[combo[2]];

            if (a != null && a == b && b == c)
                return true;
        }
        return false;
    }

    private bool CheckDraw()
    {
        foreach (var cell in board)
        {
            if (cell == null)
                return false;
        }
        return true;
    }

    private void UpdateTurnText()
    {
        if (turnText != null)
        {
            turnText.text = currentTurn.ToString() + "'s Turn";
        }
    }
}