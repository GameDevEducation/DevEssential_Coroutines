using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] string TargetScene;
    [SerializeField] Text LoadingProgress;
    [SerializeField] float LoadDelay = 5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PerformLoading());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PerformLoading()
    {
        // start the async loading
        var loadOperation = SceneManager.LoadSceneAsync(TargetScene, LoadSceneMode.Single);

        // prevent automatic activation of the next scene
        loadOperation.allowSceneActivation = false;

        // loop while the loading is in progress
        float loadDelayProgress = 0f;
        while (!loadOperation.isDone)
        {
            // ready to finish loading?
            if (loadOperation.progress >= 0.9f)
            {
                // update the load delay progress
                loadDelayProgress += Time.deltaTime / LoadDelay;

                // time to activate scene?
                if (loadDelayProgress >= 1f)
                    loadOperation.allowSceneActivation = true;
                else
                {
                    // update the progress ui
                    LoadingProgress.text = "Loading: " + (Mathf.Lerp(90f, 100f, loadDelayProgress)).ToString("0") + " %";  
                }  
            }
            else
            {
                // update the progress ui
                LoadingProgress.text = "Loading: " + (loadOperation.progress * 100).ToString("0") + " %";                
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
