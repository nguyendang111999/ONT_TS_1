using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BoolEvent : UnityEvent<bool, GameObject>{

}
public class ZoneTrigger : MonoBehaviour
{
    [SerializeField] BoolEvent _enterZone = default;

    [SerializeField] LayerMask _layer = default;

    private void OnTriggerEnter(Collider other) {
        // Debug.Log("Is interacting");
    }
    private void OnTriggerExit(Collider other) {
        // Debug.Log("Is stop interacting");
    }
}
