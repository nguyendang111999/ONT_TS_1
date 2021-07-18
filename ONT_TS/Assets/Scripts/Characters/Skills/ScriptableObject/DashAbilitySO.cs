using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "CharacterAbility/Earth/New Dash Ability")]
public class DashAbilitySO : AbilityBaseSO
{
    [SerializeField] private float _speed;
    [SerializeField] private float dashDuration;
    GameObject obj;

    public override void Perform()
    {
        obj = GameObject.FindWithTag("Enemy");
        Transform transform = obj.transform;
        obj.transform.DOMove(transform.position + (transform.forward * _speed), .2f);
        Debug.Log("Hooli shiet");
    }
}
