using UnityEngine;

[CreateAssetMenu(menuName = "WeatherConfig/New Weather Config")]
public class WeatherConfigSO : ScriptableObject
{
    [SerializeField] float startDistance = default;
    [SerializeField] float endDistance = default;

    public float StartDistance => startDistance;
    public float EndDistance => endDistance;
}
