using UnityEngine;

public class Player : Damageable
{
    [SerializeField] private float shootingSpeed;

    private void Start()
    {
        type = ObjectInfo.Type.PLAYER;
        register();
    }

    void Update()
    {
        Move();
        Shoot();
    }

    private void OnDestroy()
    {
        GameManager.instance.player = null;
    }

    private void Move()
    {
        Vector3 move = Vector3.zero;

        move.x = Input.GetAxis("Horizontal") * Time.deltaTime * 8;
        move.z = Input.GetAxis("Vertical") * Time.deltaTime * 8;

        if (Input.GetKeyDown(KeyCode.Space))
            GameManager.instance.rewind();

        gameObject.transform.Translate(move);
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 vector = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            vector.z = vector.y;
            vector.y = 0;
            GameObject bullet = Instantiate(ResourcesManager.instance.Get("bulletPrefab"), transform.position + ((vector).normalized * 1.2f), transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce((vector).normalized * shootingSpeed, ForceMode.Impulse);
        }
    }
}
