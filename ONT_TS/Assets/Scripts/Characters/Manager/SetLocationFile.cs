using UnityEngine;

public class SetLocationFile : MonoBehaviour
{
    [SerializeField] private LocationSO _location;
    private void Start() {
        _location.Position = transform.position;
    }
}
