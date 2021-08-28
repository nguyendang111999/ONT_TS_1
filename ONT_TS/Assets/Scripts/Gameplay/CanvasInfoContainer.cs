using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasInfoContainer : MonoBehaviour
{
    [SerializeField] private GameObject instructionText;
    [SerializeField] private GameObject informationPanel;
    [SerializeField] private TextMeshProUGUI text;

    public GameObject InstructionText => instructionText;
    public GameObject InformationPanel => informationPanel;
    public TextMeshProUGUI InfoText => text;
}
