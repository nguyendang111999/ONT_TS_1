using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Location", menuName = "SavePoint/Location Vector3")]
public class LocationSO : ScriptableObject
{
    [SerializeField] private Vector3 _location;//Location to respawn player
    public Vector3 Location
    {
        get { return _location; }
        set { _location = value; }
    }
}
