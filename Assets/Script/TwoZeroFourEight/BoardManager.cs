using System.Collections.Generic;
using UnityEngine;

namespace TwoZeroFourEight
{
    public class BoardManager : MonoBehaviour
    {
        public int[,] GameBoard;

        private void Awake()
        {
            GameBoard = new int[Two_GameConstants.GRID_SIZE, Two_GameConstants.GRID_SIZE];
        }

        public void InitializeGameBoard()
        {
            // Clear the board
            for (int i = 0; i < Two_GameConstants.GRID_SIZE; i++)
            {
                for (int j = 0; j < Two_GameConstants.GRID_SIZE; j++)
                {
                    GameBoard[i, j] = 0;
                }
            }

            // Spawn initial tiles
            for (int i = 0; i < Two_GameConstants.INITIAL_TILES; i++)
            {
                SpawnNewTile();
            }
        }

        public void SpawnNewTile()
        {
            var emptyCells = FindEmptyCells();

            if (emptyCells.Count > 0)
            {
                // Pick a random empty cell
                int randomIndex = Random.Range(0, emptyCells.Count);
                var (row, col) = emptyCells[randomIndex];
                
                // Determine the value (2 or 4) based on probability
                int value = Random.value < Two_GameConstants.SPAWN_TILE_2_PROBABILITY
                    ? Two_GameConstants.TILE_VALUE_2
                    : Two_GameConstants.TILE_VALUE_4;
                
                GameBoard[row, col] = value;
            }
        }

        public List<(int, int)> FindEmptyCells()
        {
            var emptyCells = new List<(int, int)>();
            
            for (int i = 0; i < Two_GameConstants.GRID_SIZE; i++)
            {
                for (int j = 0; j < Two_GameConstants.GRID_SIZE; j++)
                {
                    if (GameBoard[i, j] == 0)
                    {
                        emptyCells.Add((i, j));
                    }
                }
            }

            return emptyCells;
        }

        // Check if any moves are possible
        public bool HasAvailableMoves()
        {
            // If there are empty cells, moves are possible
            if (FindEmptyCells().Count > 0)
            {
                return true;
            }

            // Check for possible merges horizontally and vertically
            for (int i = 0; i < Two_GameConstants.GRID_SIZE; i++)
            {
                for (int j = 0; j < Two_GameConstants.GRID_SIZE - 1; j++)
                {
                    // Check horizontal merges
                    if (GameBoard[i, j] == GameBoard[i, j + 1])
                    {
                        return true;
                    }
                    // Check vertical merges
                    if (GameBoard[j, i] == GameBoard[j + 1, i])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // Check if the win condition (2048 tile) has been reached
        public bool HasWinningTile()
        {
            for (int i = 0; i < Two_GameConstants.GRID_SIZE; i++)
            {
                for (int j = 0; j < Two_GameConstants.GRID_SIZE; j++)
                {
                    if (GameBoard[i, j] == Two_GameConstants.WIN_TILE_VALUE)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}