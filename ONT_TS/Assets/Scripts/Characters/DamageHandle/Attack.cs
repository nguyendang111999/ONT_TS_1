using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int Damage { get; set; } = 0;
    public int BoostedDamage { get; set;}
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void SetAdditionDamage(int percentage)
    {
        BoostedDamage += Damage * percentage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(gameObject.tag))
        {
            if (other.TryGetComponent(out Damageable damageableComp))
            {
                if (!damageableComp.GetHit)
                    damageableComp.ReceiveAttack(Damage);
            }
        }
    }
}
