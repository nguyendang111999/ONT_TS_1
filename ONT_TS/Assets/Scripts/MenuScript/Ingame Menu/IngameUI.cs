using UnityEngine;

public class IngameUI : MonoBehaviour
{
    [SerializeField] InputReader _inputReader;

    [SerializeField] GameObject background;
    [SerializeField] GameObject backgroundBag;
    [SerializeField] GameObject header;

    [SerializeField] GameObject glossary;
    [SerializeField] GameObject glossaryArtefact;
    [SerializeField] GameObject map;
    [SerializeField] GameObject quest;
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject inventoryBag;
    [SerializeField] GameObject ability;
    [SerializeField] GameObject skills;
    [SerializeField] UIInventory _inventoryPanel;

    private void OnEnable()
    {
        _inputReader.OpenInventoryEvent += OpenInventory;
        _inputReader.CloseInventoryEvent += Escape;
        _inputReader.EscapeEvent += Escape;
    }

    private void OnDisable()
    {
        _inputReader.OpenInventoryEvent -= OpenInventory;
        _inputReader.CloseInventoryEvent -= Escape;
        _inputReader.EscapeEvent -= Escape;
    }

    void EnableBackground()
    {
        background.SetActive(true);
        backgroundBag.SetActive(true);
        header.SetActive(true);
    }

    void DisableBackground()
    {
        background.SetActive(false);
        backgroundBag.SetActive(false);
        header.SetActive(false);
    }

    public void OpenGlossary()
    {
        DisableAllObject();
        glossary.SetActive(true);
    }

    public void OpenMap()
    {
        DisableAllObject();
        map.SetActive(true);
    }

    public void OpenQuest()
    {
        DisableAllObject();
        quest.SetActive(true);
    }


    public void OpenInventory()
    {
        _inputReader.EnableMenuInput();
        EnableBackground();
        DisableAllObject();
        inventory.SetActive(true);
    }

    void CloseInventory()
    {
        _inputReader.EnableGameplayInput();
        DisableBackground();
        DisableAllObject();
    }

    public void OpenAbility()
    {
        DisableAllObject();
        ability.SetActive(true);
    }

    public void OpenSkills()
    {
        DisableAllObject();
        skills.SetActive(true);
    }

    void DisableAllObject()
    {
        glossary.SetActive(false);
        glossaryArtefact.SetActive(false);
        map.SetActive(false);
        quest.SetActive(false);
        inventory.SetActive(false);
        inventoryBag.SetActive(false);
        ability.SetActive(false);
        skills.SetActive(false);
    }
    void Escape()
    {
        _inputReader.EnableGameplayInput();
        DisableAllObject();
        DisableBackground();
    }
}
