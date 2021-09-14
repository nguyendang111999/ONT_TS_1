using UnityEngine;

[CreateAssetMenu(menuName = "CharacterConfig/Player Position")]
public class ObjectPositionSO : ScriptableObject
{
    [HideInInspector] public bool isSet = false;

    private Transform _transform;

    public Transform Transform
    {
        get { return _transform; }
        set
        {
            _transform = value;
            isSet = _transform != null;
        }
    }

    private void OnDisable()
    {
        _transform = null;
        isSet = false;
    }
}
