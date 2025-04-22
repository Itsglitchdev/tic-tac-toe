using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    private static SceneName targetScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
 
    public void LoadSceneWithLoading(SceneName sceneName)
    {
        targetScene = sceneName;
        SceneManager.LoadScene(SceneName.Loading.ToString());
    }

    public static SceneName GetTargetScene()
    {
        return targetScene;
    }

   
}
