using UnityEngine;

[CreateAssetMenu(fileName = "New Particle Distance", menuName = "Environment/Effect/Particle System Distance")]
public class ParticleDistanceSO : ScriptableObject
{
    [Tooltip("A particle will start playing when player is within this distance")]
    [SerializeField] private float distance;
    public float Distance {
        get {return distance;}
    }
}
