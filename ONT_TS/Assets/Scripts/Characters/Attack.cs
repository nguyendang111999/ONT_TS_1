using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private AttackConfigSO _attackConfigSO;

    public AttackConfigSO AttackConfig => _attackConfigSO;

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
                if(damageableComp == null) Debug.Log("shiet");
				if (!damageableComp.GetHit)
					damageableComp.ReceiveAttack(_attackConfigSO.AttackStrength);
			}
		}
	}
}
