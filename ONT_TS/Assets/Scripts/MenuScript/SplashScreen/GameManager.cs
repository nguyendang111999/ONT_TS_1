using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;


    private void Awake()
    {

    }


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.anyKey)
        {
            GoToSceneStart();
        }
    }

    void GoToSceneStart()
    {
        SceneManager.LoadSceneAsync(1);
    }


}