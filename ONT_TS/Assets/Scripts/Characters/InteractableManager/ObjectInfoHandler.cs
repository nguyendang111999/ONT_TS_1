using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectInfoHandler : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _distanceToActive;
    [SerializeField] private ObjectPositionSO _playerPos;
    [SerializeField] private LoreSO _lore;
    private CanvasInfoContainer _canvasContainer;
    private GameObject _infoCanvas;
    private GameObject _instructionText;
    private GameObject _informationPanel;
    private TextMeshProUGUI _text;
    private bool isMet = false;

    private void OnEnable()
    {
        _inputReader.SkipEvent += Escape;
    }
    private void OnDisable()
    {
        _inputReader.SkipEvent -= Escape;
    }
    private void Awake()
    {
        _infoCanvas = GameObject.FindWithTag("InfoCanvas");
        SetupUIElement();
    }

    void Update()
    {
        DisplayGuide();
    }

    public void Interact()
    {
        isMet = true;
        _informationPanel.SetActive(true);
        _text.text = _lore.Detail;
        
        _instructionText.SetActive(false);
        _inputReader.EnableDialogueInput();
    }

    public void Escape()
    {
        isMet = false;
        _informationPanel.SetActive(false);
        _inputReader.EnableGameplayInput();
    }

    private void SetupUIElement()
    {
        _canvasContainer = _infoCanvas.GetComponent<CanvasInfoContainer>();
        _instructionText = _canvasContainer.InstructionText;
        _informationPanel = _canvasContainer.InformationPanel;
        _text = _canvasContainer.InfoText;
    }

    void DisplayGuide(){
        if (_playerPos.GetDistance(transform.position) < _distanceToActive && !isMet)
        {
            _instructionText.SetActive(true);
        }
        else _instructionText.SetActive(false);
    }
}
