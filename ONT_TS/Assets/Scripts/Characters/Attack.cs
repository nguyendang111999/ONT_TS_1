using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]private AttackConfigSO _attackConfigSO;

    private void Awake() {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag(gameObject.tag)){
            if(other.TryGetComponent(out Damageable dmgComponent)){
                if(!dmgComponent.GetHit)
                    dmgComponent.ReceiveAttack(_attackConfigSO.AttackStrength);
            }
        }
    }
}
