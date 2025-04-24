using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private float minLoadTime = 3f;
    
    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        if (progressBar != null)
            progressBar.value = 0;

        // Get the target scene name
        SceneName targetScene = GameManager.GetTargetScene();
        
        // Start loading the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene.ToString());
        asyncLoad.allowSceneActivation = false;
        
        float elapsedTime = 0f;
        float targetProgress = 0f;
        float currentProgress = 0f;
        float smoothSpeed = 5f;
        
        while (!asyncLoad.isDone)
        {
            elapsedTime += Time.deltaTime;
            
            
            float loadProgress = asyncLoad.progress / 0.9f; 
            float timeProgress = elapsedTime / minLoadTime;
            targetProgress = Mathf.Min(1f, Mathf.Max(loadProgress, timeProgress));
            
            currentProgress = Mathf.Lerp(currentProgress, targetProgress, Time.deltaTime * smoothSpeed);
            
            if (progressBar != null)
                progressBar.value = currentProgress;
            
            if (asyncLoad.progress >= 0.9f && elapsedTime >= minLoadTime)
            {
                asyncLoad.allowSceneActivation = true;
            }
            
            yield return null;
        }
    }
}