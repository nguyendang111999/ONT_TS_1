using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private ObjectPositionSO _pos;
    public int Damage { get; set; } = 0;
    public int BoostedDamage { get; set; } = 0;
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        int dmg = Damage + BoostedDamage;
        if (!other.CompareTag(gameObject.tag))
        {
            if (other.TryGetComponent(out Damageable damageableComp))
            {
                if (!damageableComp.GetHit)
                    damageableComp.ReceiveAttack(dmg);
            }
        }
        if(other.GetComponent<EnemyBehaviour>() != null){
            other.GetComponent<EnemyBehaviour>().Target = _pos;
        }
    }
}
