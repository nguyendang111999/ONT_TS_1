using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public AttackConfigSO _attackStrength;
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(gameObject.tag))
        {
            if (other.TryGetComponent(out Damageable damageableComp))
            {
                if (!damageableComp.GetHit)
                    damageableComp.ReceiveAttack(_attackStrength.AttackStrength);
            }
        }
    }
}
