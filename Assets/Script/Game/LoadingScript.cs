using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private float minLoadTime = 3f;
    [SerializeField] private float maxLoadTime = 5f;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        if (progressBar != null)
            progressBar.value = 0;

        float loadTime = Random.Range(minLoadTime, maxLoadTime);
        float elapsedTime = 0;
        
        while (elapsedTime < loadTime)
        {
            elapsedTime += Time.deltaTime;
            
            if (progressBar != null)
                progressBar.value = elapsedTime / loadTime;
                
            yield return null;
        }
        
        if (progressBar != null)
            progressBar.value = 1f;
            
        SceneName targetScene = GameManager.GetTargetScene();
        SceneManager.LoadScene(targetScene.ToString());
    }
}