using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopupSettingManager : MonoBehaviour
{
    [Header("Sprite")]
    public Sprite languageActiveSprite;
    public Sprite languageDeactiveSprite;
    public Sprite audioActiveSprite;
    public Sprite audioDeactiveSprite;
    public Sprite videoActiveSprite;
    public Sprite videoDeactiveSprite;
    public Sprite controlActiveSprite;
    public Sprite controlDeactiveSprite;

    [Header("Button")]
    public Button languageBtn;
    public Button audioBtn;
    public Button videoBtn;
    public Button controlBtn;

    [Header("PopupChild")]
    public GameObject languageChild;
    public GameObject audioChild;
    public GameObject videoChild;
    public GameObject controlChild;
    GameObject currentChild;

    public Transform frameBorder;

    private void Start()
    {
        languageBtn.onClick.AddListener(OnLanguageBtnClick);
        audioBtn.onClick.AddListener(OnAudioBtnClick);
        videoBtn.onClick.AddListener(OnVideoBtnClick);
        controlBtn.onClick.AddListener(OnControlBtnClick);
    }

    public void OnLanguageBtnClick()
    {
        if (languageChild.activeSelf)
        {
            return;
        }
        DeactiveAllBtn();
        languageBtn.GetComponent<Image>().sprite = languageActiveSprite;
        ShowChild(languageChild);
    }

    void OnAudioBtnClick()
    {
        if (audioChild.activeSelf)
        {
            return;
        }
        DeactiveAllBtn();
        audioBtn.GetComponent<Image>().sprite = audioActiveSprite;
        ShowChild(audioChild);
    }

    void OnVideoBtnClick()
    {
        if (videoChild.activeSelf)
        {
            return;
        }
        DeactiveAllBtn();
        videoBtn.GetComponent<Image>().sprite = videoActiveSprite;
        ShowChild(videoChild);
    }

    void OnControlBtnClick()
    {
        if (controlChild.activeSelf)
        {
            return;
        }
        DeactiveAllBtn();
        controlBtn.GetComponent<Image>().sprite = controlActiveSprite;
        ShowChild(controlChild);
    }

    void ShowChild(GameObject targetChild)
    {
        if (currentChild != null)
        {
            currentChild.gameObject.SetActive(false);
        }
        frameBorder.DOScale(new Vector3(0.4f, 0.4f, 0.4f), 0.1f).onComplete += delegate
        {
            frameBorder.DOScale(Vector3.one, 0.1f).onComplete += delegate
            {
                currentChild = targetChild;
                targetChild.gameObject.SetActive(true);
            };
        };
    }

    void DeactiveAllBtn()
    {
        languageBtn.GetComponent<Image>().sprite = languageDeactiveSprite;
        audioBtn.GetComponent<Image>().sprite = audioDeactiveSprite;
        videoBtn.GetComponent<Image>().sprite = videoDeactiveSprite;
        controlBtn.GetComponent<Image>().sprite = controlDeactiveSprite;
        languageChild.SetActive(false);
        audioChild.SetActive(false);
        videoChild.SetActive(false);
        controlChild.SetActive(false);
    }

    public void OnBackToMenu()
    {
        if (currentChild != null)
        {
            currentChild.gameObject.SetActive(false);
        }
        frameBorder.DOScale(new Vector3(0.4f, 0.4f, 0.4f), 0.1f).onComplete += delegate
        {
            MainMenu.instance._animator.SetTrigger("MoveIn");
            gameObject.SetActive(false);
        };
    }

    public void BackToIngameMenu()
    {
        if (currentChild != null)
        {
            currentChild.gameObject.SetActive(false);
        }
        frameBorder.DOScale(new Vector3(0.4f, 0.4f, 0.4f), 0.1f).onComplete += delegate
        {
            gameObject.SetActive(false);
        };
    }
}
