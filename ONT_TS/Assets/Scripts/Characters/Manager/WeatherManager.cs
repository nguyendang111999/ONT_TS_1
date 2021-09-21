using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] WeatherConfigSO _config;
    [SerializeField] GameObject _wall;
    [SerializeField] float speedChange = 2f;

    bool triggerd = false;
    bool isSet = false;

    private void Update()
    {
        DisableOjbect();
        ChangeWeather();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            triggerd = true;
            _wall.SetActive(true);
        }
    }

    /// <summary>
    /// Change fog density over time
    /// </summary>
    void ChangeWeather()
    {
        if (!triggerd) return;

        RenderSettings.fogMode = FogMode.Linear;
        float startDistance = RenderSettings.fogStartDistance;
        float endDistance = RenderSettings.fogEndDistance;

        if (Mathf.Abs(endDistance - _config.EndDistance) <= 0.1f)
        {
            RenderSettings.fogStartDistance = _config.StartDistance;
            RenderSettings.fogEndDistance = _config.EndDistance;
            isSet = true;
            return;
        }

        RenderSettings.fogStartDistance = Mathf.Lerp(startDistance, _config.StartDistance, speedChange * Time.deltaTime);
        RenderSettings.fogEndDistance = Mathf.Lerp(endDistance, _config.EndDistance, speedChange * Time.deltaTime);
    }

    /// <summary>
    /// Disable object when finished change weather
    /// </summary>
    void DisableOjbect()
    {
        if (isSet) gameObject.SetActive(false);
    }

}
