using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;

    private Vector2 _previousMovementInput;

    [NonSerialized] public bool attackInput;

    private void OnEnable() {
        _inputReader.moveEvent += OnMove;
        _inputReader.attackEvent += OnAttack;
    }
    private void OnDisable() {
        _inputReader.moveEvent -= OnMove;
        _inputReader.attackEvent -= OnAttack;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("x = " + _previousMovementInput.x + ": y = " + _previousMovementInput.y);

    }

    void updateAttack(){

    }
    //--- Event Listeners ---
    private void OnMove(Vector2 movement){
        _previousMovementInput = movement;
    }
    private void OnAttack() => attackInput = true;

    // Triggerd from animation Event
    private void FinishAttack() => attackInput = false;
}
