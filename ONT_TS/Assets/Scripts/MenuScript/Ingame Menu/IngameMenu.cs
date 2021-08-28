using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CallStatAndResource();
        }
    }

    

    public void PauseGame()
    {
        
        if (SceneManager.sceneCount>1)
        {

        }
        else {SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);}
    }

    public void ResumeGameFromIngameMenu()
    {
        SceneManager.UnloadSceneAsync(3);
    }

    public void CallStatAndResource()
    {
        if (SceneManager.sceneCount > 1)
        {

        }
        else {SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);
        }
    }

    public void ResumeGameFromStatAndResource()
    {
        SceneManager.UnloadSceneAsync(4);
    }

    public void LoadMainMenu()
    {
        SceneManager.UnloadSceneAsync(3);
        SceneManager.UnloadSceneAsync(2);
        SceneManager.LoadSceneAsync(1);
    }
}
