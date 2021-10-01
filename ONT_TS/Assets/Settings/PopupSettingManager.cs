using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.InputSystem;
using System;

public class PopupSettingManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
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

    public PlayerInput playerInput;

    [SerializeField] private InputActionReference jumpAction = null;

    [SerializeField]
    private InputActionReference m_Action;

    public InputField leftInput;
    public InputField rightInput;
    public InputField upInput;
    public InputField downInput;

    public InputActionReference actionReference
    {
        get => m_Action;
        set
        {
            m_Action = value;
            //UpdateActionLabel();
            //UpdateBindingDisplay();
        }
    }
    [SerializeField]
    private string m_BindingId;

    public string bindingId
    {
        get => m_BindingId;
        set
        {
            m_BindingId = value;
            //UpdateBindingDisplay();
        }
    }

    private void Start()
    {
        languageBtn.onClick.AddListener(OnLanguageBtnClick);
        audioBtn.onClick.AddListener(OnAudioBtnClick);
        videoBtn.onClick.AddListener(OnVideoBtnClick);
        controlBtn.onClick.AddListener(OnControlBtnClick);

        if (playerInput == null)
        {
            playerInput = new PlayerInput();
            playerInput.Menu.SetCallbacks(_inputReader);
            playerInput.GamePlay.SetCallbacks(_inputReader);
            playerInput.Dialogue.SetCallbacks(_inputReader);
        }
        playerInput.GamePlay.Enable();

        string displayString = "";

        var action = m_Action?.action;
        if (action != null)
        {
            var bindingIndex = action.bindings.IndexOf(x => x.id.ToString() == m_BindingId);
            Debug.Log(m_BindingId);
            int i = 0;
            foreach (var act in action.bindings)
            {
                Debug.Log(act.id.ToString());
                bindingIndex = action.bindings.IndexOf(x => x.id.ToString() == act.id.ToString());
                displayString = action.GetBindingDisplayString(bindingIndex);
                if (i == 1)
                {
                    upInput.text = action.GetBindingDisplayString(bindingIndex);
                }
                if (i == 2)
                {
                    downInput.text = action.GetBindingDisplayString(bindingIndex);
                }
                if (i == 3)
                {
                    leftInput.text = action.GetBindingDisplayString(bindingIndex);
                }
                if (i == 4)
                {
                    rightInput.text = action.GetBindingDisplayString(bindingIndex);
                }
                i++;
            }
            //Debug.Log(bindingIndex);
            //if (bindingIndex != -1)
            //    displayString = action.GetBindingDisplayString(bindingIndex/*, out deviceLayoutName, out controlPath, displayStringOptions*/);
        }
        Debug.Log(displayString);
    }

    InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    public void OnLeftValueChanged(string bindingID)
    {
        var action = m_Action?.action;
        playerInput.GamePlay.Movements.Disable();
        var bindingIndex = action.bindings.IndexOf(x => x.id.ToString() == bindingID);
        //action.PerformInteractiveRebinding().WithControlsExcluding("Mouse").OnMatchWaitForAnother(0.1f).Start();
        //rebindingOperation = playerInput.GamePlay.Movements.PerformInteractiveRebinding().WithControlsExcluding("Mouse").OnMatchWaitForAnother(0.1f).OnComplete(operation => OnComplete()).Start();
        rebindingOperation = action.PerformInteractiveRebinding(bindingIndex).WithControlsExcluding("Mouse").OnMatchWaitForAnother(0.1f).OnComplete(operation => OnComplete()).Start();
    }

    void OnComplete()
    {
        var action = m_Action?.action;
        int bindingIndex = action.GetBindingIndexForControl(action.controls[1]);
        string bindingText = InputControlPath.ToHumanReadableString(action.bindings[bindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        Debug.Log(bindingText);
        rebindingOperation.Dispose();
        playerInput.GamePlay.Movements.Enable();
        Save();
    }

    void Save()
    {
        var action = m_Action?.action;
        Debug.Log(action.SaveBindingOverridesAsJson());
        string rebinds = playerInput.SaveBindingOverridesAsJson();
        Debug.Log(rebinds);
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

    public void OnSaveBinding()
    {

    }
}
