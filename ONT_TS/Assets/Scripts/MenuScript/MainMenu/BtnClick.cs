using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BtnClick : MonoBehaviour
{
    public void BtnNewGame()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void BtnLoadGame()
    {
        SceneManager.LoadSceneAsync(2);
        //temp
    }

}