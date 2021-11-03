using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public bool isLoadingScreen = false;
    public int nextSceneIndex;

    void Awake(){
        if(isLoadingScreen){
            StartCoroutine(LoadNewAsyncScene(nextSceneIndex));
        }
    }

    IEnumerator LoadNewAsyncScene(int sceneIndex){

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);
        
        while(!async.isDone){
            yield return null;
        }
    }
    
    public void LoadGameOverScene(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex +1);
    }
    
    public void LoadStartScene(){
        SceneManager.LoadScene(0);
    }

    public void LoadGameScene(){
        SceneManager.LoadScene(1);
    }

    // Quit game
    public void Quit(){
        Application.Quit();
    }
}
