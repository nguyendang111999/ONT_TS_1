using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    public static LoadingController instance;
    public GameObject loadingPanel;
    public Image loadingBar;
    public TextMeshProUGUI loadingProgress;

    private AsyncOperation checkLoad;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(int index)
    {
        loadingBar.fillAmount = 0;
        loadingProgress.text = "0%";
        loadingPanel.gameObject.SetActive(true);
        checkLoad = SceneManager.LoadSceneAsync(index);
    }

    private void Update()
    {
        if (checkLoad != null)
        {
            if (loadingProgress != null)
            {
                loadingProgress.text = (checkLoad.progress * 100).ToString() + "%";
            }
            if (!checkLoad.isDone)
            {
                loadingBar.fillAmount = checkLoad.progress;
            }
            else
            {
                loadingBar.fillAmount = 1;
                checkLoad = null;
                loadingPanel.gameObject.SetActive(false);
            }
        }
    }
}
