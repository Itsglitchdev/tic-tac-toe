using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TwoZeroFourEight
{
    public class GridRenderer : MonoBehaviour
    {
        [SerializeField] private GameObject tilePrefab;
        private GameObject[,] tileObjects;

        void Start()
        {
            tileObjects = new GameObject[Two_GameConstants.GRID_SIZE, Two_GameConstants.GRID_SIZE];
            RenderGrid();
        }

        void Update()
        {
            UpdateGrid();
        }

        private void RenderGrid()
        { 
            // Clear any existing tiles
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            GridLayoutGroup grid = GetComponent<GridLayoutGroup>();
            if (grid == null)
            {
                Debug.LogError("GridLayoutGroup component is missing!");
                return;
            }

            // Create and position tiles
            for (int i = 0; i < Two_GameConstants.GRID_SIZE; i++)
            {
                for (int j = 0; j < Two_GameConstants.GRID_SIZE; j++)
                {
                    GameObject tile = Instantiate(tilePrefab, transform);
                    tileObjects[i, j] = tile;
                    UpdateTile(i, j);
                }
            }
        }

        private void UpdateGrid()
        {
            for (int i = 0; i < Two_GameConstants.GRID_SIZE; i++)
            {
                for (int j = 0; j < Two_GameConstants.GRID_SIZE; j++)
                {
                    UpdateTile(i, j);
                }
            }
        }

        private void UpdateTile(int row, int col)
        {
            if (tileObjects[row, col] == null) return;

            int value = Two_GameManager.Instance.gameBoard[row, col];
            TextMeshProUGUI textMesh = tileObjects[row, col].GetComponentInChildren<TextMeshProUGUI>();
            Image tileImage = tileObjects[row, col].GetComponent<Image>();

            if (value == 0)
            {
                textMesh.text = "";
            }
            else
            {
                textMesh.text = value.ToString();
            }

            // Update colors using our TileColors class
            tileImage.color = TileColors.GetTileColor(value);
            textMesh.color = TileColors.GetTextColor(value);
        }
    }
}