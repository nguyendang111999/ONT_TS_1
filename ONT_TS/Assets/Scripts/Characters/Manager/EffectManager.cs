using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] TrailRenderer normalSlash;
    // [SerializeField] ParticleSystem heavyAttack;
    // [SerializeField] ParticleSystem earthAttack;

    private void Start() {
        normalSlash.gameObject.SetActive(false);
        // heavyAttack.Stop();
    }
}
