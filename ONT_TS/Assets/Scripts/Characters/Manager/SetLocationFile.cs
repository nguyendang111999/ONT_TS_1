using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLocationFile : MonoBehaviour
{
    [SerializeField] private LocationSO _location;
    private void Start() {
        _location.Location = transform.position;
    }
}
