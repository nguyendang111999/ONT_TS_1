using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public AbilityContainerSO containerSO;
    private int _damage = 0;
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("index: " + containerSO.Index);
        _damage = containerSO.GetDamageByIndex();
        Debug.Log("Damage: " + _damage);
        if (!other.CompareTag(gameObject.tag))
        {
            if (other.TryGetComponent(out Damageable damageableComp))
            {
                if (!damageableComp.GetHit)
                    damageableComp.ReceiveAttack(_damage);
            }
        }
        _damage = 0;
    }
}
