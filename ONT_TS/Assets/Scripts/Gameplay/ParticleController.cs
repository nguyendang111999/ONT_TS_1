using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [Tooltip("Player position")]
    [SerializeField]
    private ObjectPositionSO _playerPos;
    [Tooltip("Distance to start playeing a particle")]
    [SerializeField]
    private ParticleDistanceSO _distance;
    private ParticleSystem _system;
    private float distance;
    private void Awake() {
        _system = gameObject.GetComponent<ParticleSystem>();
        distance = _distance.Distance;
        var main = _system.main;
        main.prewarm = true;
    }

    private void Update() {
        float d = Vector3.Distance(gameObject.transform.position, _playerPos.Transform.position);
        if(d < distance){
            _system.Play();
        }
        else _system.Stop();
    }
}
