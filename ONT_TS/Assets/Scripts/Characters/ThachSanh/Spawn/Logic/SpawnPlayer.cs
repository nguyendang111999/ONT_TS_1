using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private Damageable _damageable;

    [SerializeField] private SavePointListSO _savePoint;

    public void Spawn(){
        _damageable.ResetHealth();
        gameObject.transform.position = _savePoint.GetLastSavePoint();
    }
    
}
