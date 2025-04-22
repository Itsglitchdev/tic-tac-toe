using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [System.Serializable]
    public class GameButton
    {
        public Button button;
        public SceneName sceneToLoad;
    }

    [SerializeField] private GameButton[] gameButtons;

    private void Start()
    {
        foreach (var gameButton in gameButtons)
        {
            if (gameButton.button != null)
            {
                SceneName sceneToLoad = gameButton.sceneToLoad;
                gameButton.button.onClick.AddListener(() => LoadGame(sceneToLoad));
            }
        }
    }

    private void LoadGame(SceneName sceneToLoad)
    {
        GameManager.Instance.LoadSceneWithLoading(sceneToLoad);
    }
}