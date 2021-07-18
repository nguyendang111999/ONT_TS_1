using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField]private GameObject _attackCollider;

    public void EnableWeapon(){
        gameObject.GetComponent<Damageable>().ReceiveAttack(10);
        _attackCollider.SetActive(true);
    }

    public void DisableWeapon(){
        _attackCollider.SetActive(false);
    }
}
