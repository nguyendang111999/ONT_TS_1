using UnityEngine;

//Open and close inventory tab
public class UIManager : MonoBehaviour
{
    [SerializeField] InputReader _inputReader;
    [SerializeField] PopupSettingManager _settingManager;
    [SerializeField] GameObject _ingameMenu;
    [SerializeField] GameObject _background;
    [SerializeField] GameObject _resourceBG;
    [SerializeField] GameObject _TSHud;
    private void OnEnable() {
        _inputReader.OpenIngameMenu += OpenIngameMenu;
        _inputReader.EscapeEvent += CloseIngameMenu;
    }
    private void OnDisable() {
        _inputReader.OpenIngameMenu -= OpenIngameMenu;
        _inputReader.EscapeEvent -= CloseIngameMenu;
    }

    void OpenIngameMenu(){
        if(_resourceBG.activeSelf) return;
        _inputReader.EnableMenuInput();
        _background.SetActive(true);
        _ingameMenu.SetActive(true);
        _TSHud.SetActive(false);
    }
    public void CloseIngameMenu(){
        // _inputReader.EnableGameplayInput();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _background.SetActive(false);
        _ingameMenu.SetActive(false);
        _TSHud.SetActive(true);
    }

    public void OpenSetting(){
        _settingManager.gameObject.SetActive(true);
        _settingManager.OnLanguageBtnClick();
        _ingameMenu.SetActive(false);
    }

    public void QuitGame(){
        Debug.Log("Quit");
        Application.Quit();
    }

    public void ReturnToMainMenu(){
        
    }
}
