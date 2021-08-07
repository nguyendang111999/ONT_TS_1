using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    
    public InputReader _inputReader = default;

    private void OnEnable() {
        _inputReader.InteractEvent += OnInteractionButtonPress;
    }
    private void OnDisable() {
        _inputReader.InteractEvent -= OnInteractionButtonPress;
    }

    public void OnInteractionButtonPress(){
        _inputReader.EnableMenuInput();
    }
    
}
