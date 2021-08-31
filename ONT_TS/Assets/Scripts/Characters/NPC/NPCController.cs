using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private ObjectPositionSO _pos;
    public ObjectPositionSO Position => _pos;

    // Start is called before the first frame update
    private void Awake() {
        _pos.Transform = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _pos.Transform = transform;
    }
}
