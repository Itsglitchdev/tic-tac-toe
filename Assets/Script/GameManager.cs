using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private Button resetButton;

    private PlayerTurn currentTurn;
    private PlayerTurn?[] board = new PlayerTurn?[9];
    private bool gameEnded = false;

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


    private void Start()
    {
        currentTurn = PlayerTurn.X;
        UpdateTurnText();
        OneBox.OnBoxClicked += HandleBoxClick;
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(ResetGame);
        }
    }

    private void OnDestroy()
    {
        OneBox.OnBoxClicked -= HandleBoxClick;
        if (resetButton != null)
        {
            resetButton.onClick.RemoveListener(ResetGame);
        }
    }

    private void HandleBoxClick(int index, Button button)
    {
        if (gameEnded || board[index] != null) return;
        
        board[index] = currentTurn;
        TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
        text.text = currentTurn.ToString();
        button.interactable = false;

        if (CheckWin())
        {
            gameEnded = true;
            turnText.text = $"Player {currentTurn} Wins!";
            return;
        }

        if (CheckDraw())
        {
            gameEnded = true;
            turnText.text = "It's a Draw!";
            return;
        }

        currentTurn = currentTurn == PlayerTurn.X ? PlayerTurn.O : PlayerTurn.X;
        UpdateTurnText();
    }

    private void ResetGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
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
        turnText.text = currentTurn.ToString() + "'s Turn";
    }
}

enum PlayerTurn
{
    X,
    O
}