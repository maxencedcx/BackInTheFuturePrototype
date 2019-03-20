using UnityEngine;

public abstract class Damageable : MonoBehaviour {
    [SerializeField] private int maxHealth;

    private int _health;
    private int Health {
        get { return _health; }
        set {
            _health = Mathf.Clamp(value, 0, maxHealth);
            if (_health == 0)
                Die();
        }
    }

    private void Awake() {
        Health = maxHealth;
    }

    public void GetDamage(int damage) {
        if (damage > 0)
            Health -= damage;
    }

    public void Die() {
        Destroy(gameObject);
    }
}