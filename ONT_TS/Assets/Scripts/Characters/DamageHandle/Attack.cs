using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int Damage { get; set; } = 0;
    public int BoostedDamage { get; set; } = 0;
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        int dmg = Damage + BoostedDamage;
        Debug.Log("Damage is: " + dmg);
        if (!other.CompareTag(gameObject.tag))
        {
            if (other.TryGetComponent(out Damageable damageableComp))
            {
                if (!damageableComp.GetHit)
                    damageableComp.ReceiveAttack(dmg);
            }
        }
    }
}
