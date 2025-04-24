using System.Collections.Generic;
using UnityEngine;

namespace TwoZeroFourEight
{
    public class MovementSystem : MonoBehaviour
    {
        private int moveScore;

        public bool MoveLeft()
        {
            moveScore = 0;
            bool moved = false;
            for (int row = 0; row < Two_GameConstants.GRID_SIZE; row++)
            {
                bool columnMoved = MergeLine(row, true, false);
                if (columnMoved)
                {
                    moved = true;
                }
            }
            Two_GameManager.Instance.AfterMove(moved, moveScore);
            return moved;
        }

        public bool MoveRight()
        {
            moveScore = 0;
            bool moved = false;
            for (int row = 0; row < Two_GameConstants.GRID_SIZE; row++)
            {
                bool columnMoved = MergeLine(row, true, true);
                if (columnMoved)
                {
                    moved = true;
                }
            }
            Two_GameManager.Instance.AfterMove(moved, moveScore);
            return moved;
        }

        public bool MoveUp()
        {
            moveScore = 0;
            bool moved = false;
            for (int col = 0; col < Two_GameConstants.GRID_SIZE; col++)
            {
                bool columnMoved = MergeLine(col, false, false);
                if (columnMoved)
                {
                    moved = true;
                }
            }
            Two_GameManager.Instance.AfterMove(moved, moveScore);
            return moved;
        }

        public bool MoveDown()
        {
            moveScore = 0;
            bool moved = false;

            for (int col = 0; col < Two_GameConstants.GRID_SIZE; col++)
            {
                bool columnMoved = MergeLine(col, false, true);
                if (columnMoved)
                {
                    moved = true;
                }
            }
            Two_GameManager.Instance.AfterMove(moved, moveScore);
            return moved;
        }


        private bool MergeLine(int lineIndex, bool isRow, bool reverse)
        {
            bool moved = false;
            int[] line = new int[Two_GameConstants.GRID_SIZE];
            int[,] gameBoard = Two_GameManager.Instance.gameBoard;

            // Extract the line
            for (int i = 0; i < Two_GameConstants.GRID_SIZE; i++)
            {
                line[i] = isRow ? gameBoard[lineIndex, i] : gameBoard[i, lineIndex];
            }

            // Merge the line and track score
            int lineScore;
            moved = MergeNumbers(line, reverse, out lineScore);
            moveScore += lineScore;

            // Put the line back
            for (int i = 0; i < Two_GameConstants.GRID_SIZE; i++)
            {
                if (isRow)
                {
                    gameBoard[lineIndex, i] = line[i];
                }
                else
                {
                    gameBoard[i, lineIndex] = line[i];
                }
            }

            return moved;
        }

        private bool MergeNumbers(int[] line, bool reverse, out int score)
        {
            bool moved = false;
            score = 0;
            List<int> numbers = new List<int>();

            // Collect non-zero numbers
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] != 0)
                {
                    numbers.Add(line[i]);
                }
            }

            if (reverse)
            {
                numbers.Reverse();
            }

            // Merge adjacent equal numbers and calculate score
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                if (numbers[i] == numbers[i + 1])
                {
                    numbers[i] *= 2;

                    // Add to score - the value of the merged tile
                    score += numbers[i];

                    numbers.RemoveAt(i + 1);
                    moved = true;
                }
            }

            // Save the original line to check for changes
            int[] originalLine = (int[])line.Clone();

            // Fill the result back into the line
            for (int i = 0; i < line.Length; i++)
            {
                int newValue;
                if (reverse)
                {
                    newValue = (i < numbers.Count) ? numbers[i] : 0;
                    line[line.Length - 1 - i] = newValue;
                }
                else
                {
                    newValue = (i < numbers.Count) ? numbers[i] : 0;
                    line[i] = newValue;
                }
            }

            // Check if anything moved
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] != originalLine[i])
                {
                    moved = true;
                }
            }

            return moved;
        }
    }
}