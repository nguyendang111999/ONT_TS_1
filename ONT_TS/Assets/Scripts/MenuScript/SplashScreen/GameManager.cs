using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public Animator _animator;
    bool isPressed = false;

    void Update()
    {
        if (Input.anyKey && !isPressed)
        {
            isPressed = true;
            //_animator.SetTrigger("Pressed");
            _animator.gameObject.SetActive(false);
            GoToSceneStart();
        }
    }

    void GoToSceneStart()
    {
        LoadingController.instance.LoadScene(1);
    }


}