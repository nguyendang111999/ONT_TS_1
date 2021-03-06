using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointHandler : MonoBehaviour
{
    [Tooltip("This is where player get spawn after dead")]
    [SerializeField]
    private LocationSO _locationSO;
    
    [Tooltip("The list of savepoint of the current map")]
    [SerializeField]
    private SavePointListSO _savePointList;

    [SerializeField] ParticleSystem _ps;
    private void Start() {
        _locationSO.Position = transform.position;
    }
    /// <summary>
    /// Add save point to saved list
    /// </summary>
    public void SaveToList(){
        
        _savePointList.AddSavePoint(_locationSO);
        _ps.Play();
    }
}
