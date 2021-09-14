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
        SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
    }

    public void ResumeGameFromIngameMenu()
    {
        SceneManager.UnloadSceneAsync(3);
    }

    public void CallStatAndResource()
    {
        SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);
    }

    public void ResumeGameFromStatAndResource()
    {
        SceneManager.UnloadSceneAsync(4);
    }
}
