using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public AbilityContainerSO containerSO;
    public int Damage { get; set; } = 0;
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("index: " + containerSO.Index);
        Damage = containerSO.GetDamage();
        Debug.Log("Damage: " + Damage);
        if (!other.CompareTag(gameObject.tag))
        {
            if (other.TryGetComponent(out Damageable damageableComp))
            {
                if (damageableComp == null) Debug.Log("shiet");
                if (!damageableComp.GetHit)
                    damageableComp.ReceiveAttack(Damage);
            }
        }
        Damage = 0;
    }
}
