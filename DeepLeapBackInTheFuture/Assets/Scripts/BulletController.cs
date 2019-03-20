using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnTriggerEnter(Collider other) {
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable) {
            damageable.GetDamage(damage);
        }
        Destroy(gameObject);
    }
}
