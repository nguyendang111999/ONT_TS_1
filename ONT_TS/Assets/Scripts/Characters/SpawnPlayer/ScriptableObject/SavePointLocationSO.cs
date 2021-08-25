using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Savepoint", menuName = "SavePoint/Savepoint Location")]
public class SavePointLocationSO : ScriptableObject
{
    [SerializeField] private Vector3 _location;//Location to spawn player
    public Vector3 SpawnLocation
    {
        get { return _location; }
        set { _location = value; }
    }
}
