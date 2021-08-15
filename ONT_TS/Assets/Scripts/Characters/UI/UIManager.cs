using UnityEngine;

//Open and close inventory tab
public class UIManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private UIInventory _inventoryPanel;

    private void OnEnable() {
        _inputReader.OpenInventoryEvent += OpenInventory;
        _inputReader.CloseInventoryEvent += CloseInventory;
    }
    private void OnDisable() {
        _inputReader.OpenInventoryEvent -= OpenInventory;
        _inputReader.CloseInventoryEvent -= CloseInventory;
    }

    void OpenInventory(){
        _inputReader.EnableMenuInput();
        _inventoryPanel.gameObject.SetActive(true);
    }
    void CloseInventory(){
        _inputReader.EnableGameplayInput();
        _inventoryPanel.gameObject.SetActive(false);
    }
}
