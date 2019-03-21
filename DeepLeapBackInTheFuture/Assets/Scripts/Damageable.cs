using UnityEngine;

public abstract class Damageable : ObjectToRewind
{
    [SerializeField] protected int maxHealth;

    protected int _health;
    virtual protected int Health {
        get { return _health; }
        set {
            _health = Mathf.Clamp(value, 0, maxHealth);
            if (_health == 0)
                Die();
        }
    }

    private void Awake()
    {
        Health = maxHealth;
    }

    public void GetDamage(int damage)
    {
        if (damage > 0)
            Health -= damage;
    }

    override public void rewind(ObjectInfo infos)
    {
        DontDie();
        base.rewind(infos);
    }

    virtual public void Die()
    {
        Debug.Log("DYING SOON");
        gameObject.SetActive(false);
        Invoke("DestroyMe", GameManager.instance.getRefreshRate());
    }

    public void DontDie()
    {
        Debug.Log("DENIED DYING SOON");
        gameObject.SetActive(true);
        CancelInvoke("DestroyMe");
    }
}