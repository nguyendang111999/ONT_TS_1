using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;
    public Transform gameModeTransform;
    public Animator _animator;
    string eventName;
    public GameObject settingPopup;

    private void Awake()
    {
        instance = this;
    }

    public void OnNewGameClicked()
    {
        _animator.SetTrigger("MoveOut");
        eventName = "NewGame";
    }

    public void OnLoadGameClicked()
    {
        _animator.SetTrigger("MoveOut");
        eventName = "LoadGame";
    }

    public void OnChooseGameMode()
    {
        LoadingController.instance.LoadScene(2);
    }

    public void OnAnimationMoveOutDoneEvent()
    {
        switch (eventName)
        {
            case "NewGame":
                gameModeTransform.gameObject.SetActive(true);
                eventName = "";
                break;
            case "LoadGame":
                LoadingController.instance.LoadScene(2);
                eventName = "";
                break;
            case "Setting":
                settingPopup.gameObject.SetActive(true);
                settingPopup.GetComponent<PopupSettingManager>().OnLanguageBtnClick();
                eventName = "";
                break;
        }
    }

    public void OnAnimationMoveInDoneEvent()
    {

    }

    public void OnOptionClick()
    {
        _animator.SetTrigger("MoveOut");
        eventName = "Setting";
    }
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
