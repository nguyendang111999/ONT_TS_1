using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLocationFile : MonoBehaviour
{
    [SerializeField] private SavePointLocationSO _location;
    private void Start() {
        _location.Location = transform.position;
    }
}
