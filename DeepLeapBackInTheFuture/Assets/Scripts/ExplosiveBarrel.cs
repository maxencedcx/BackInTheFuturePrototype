using System.Collections;
using UnityEngine;

public class ExplosiveBarrel : Damageable
{
    [SerializeField] private float radius;
    [SerializeField] private int damage;
    [SerializeField] private float timer;
    private Coroutine explosion = null;

    protected override int Health {
        get { return _health; }
        set {
            _health = Mathf.Clamp(value, 0, maxHealth);
            if (_health == 0 && explosion == null)
                explosion = StartCoroutine(StartExplosion());
        }
    }

    private IEnumerator StartExplosion() {
        GetComponent<Renderer>().material.color = Color.gray;
        yield return new WaitForSeconds(timer);
        Die();
    }

    public override void rewind(ObjectInfo infos) {
        base.rewind(infos);

        StopAllCoroutines();
        GetComponent<Renderer>().material.color = Color.yellow;
        explosion = null;
        Health = maxHealth;
    }

    public override void Die() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        for (int i = 0; i < colliders.Length; ++i) {
            Damageable d = colliders[i].GetComponent<Damageable>();
            if (d)
                d.GetDamage(damage);
        }

        base.Die();
    }
}
