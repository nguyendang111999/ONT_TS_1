using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] AbilityContainerSO _container;
    [SerializeField] AbilityBaseSO _ability;
    private int index = 0;
    [SerializeField] private Slider slider;
    [SerializeField] private Image icon = default;
    // [SerializeField] private Image cooldownImage = default;

    private void Awake()
    {
        _ability = _container.AbilityList[_container.Index];
    }
    private void Start()
    {
        index = _container.Index;
        _ability = _container.AbilityList[_container.Index];
        // Debug.Log("Ability name: " + _ability.AbilityName);
        SetIcon(_ability.Icon);
        SetMaxCooldown(_ability.CoolDownDuration);
        SetCooldown(_ability.CoolDownCounter);
    }
    private void Update()
    {
        if (index != _container.Index)
        {
            index = _container.Index;
            _ability = _container.AbilityList[_container.Index];

            SetIcon(_ability.Icon);
            SetMaxCooldown(_ability.CoolDownDuration);
        }
        SetCooldown(_ability.CoolDownCounter);
    }

    public void SetIcon(Sprite s)
    {
        icon.sprite = s;
    }
    public void SetMaxCooldown(float value)
    {
        slider.maxValue = value;
        slider.value = value;
    }
    public void SetCooldown(float value)
    {
        slider.value = value;
    }
}
