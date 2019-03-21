using UnityEngine;

public class Player : Damageable
{
    [SerializeField] private float shootingSpeed;

    new private void Start()
    {
        GameManager.instance.player = gameObject;
        type = ObjectInfo.Type.PLAYER;
        base.Start();
    }

    void Update()
    {
        Move();
        Shoot();
    }

    new private void OnDestroy()
    {
        base.OnDestroy();
        GameManager.instance.player = null;
    }

    private void Move()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.Z)) {
            move.z += 1;
        }
        if (Input.GetKey(KeyCode.S)) {
            move.z -= 1;
        }
        if (Input.GetKey(KeyCode.D)) {
            move.x += 1;
        }
        if (Input.GetKey(KeyCode.Q)) {
            move.x -= 1;
        }

        if (Input.GetKeyDown(KeyCode.Space))
            GameManager.instance.rewind();
        
        if (move != Vector3.zero) {
            move = move.normalized * Time.deltaTime * 8;
            gameObject.transform.Translate(move);
            GameManager.instance.RecordPlayerInput(new GameManager.Key { isMove = true, movement = move } );
        }
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 vector = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            vector.z = vector.y;
            vector.y = 0;
            GameManager.instance.RecordPlayerInput(new GameManager.Key { isMove = false, mousePos = Input.mousePosition });
            GameObject bullet = Instantiate(ResourcesManager.instance.Get("bulletPrefab"), transform.position + ((vector).normalized * 1.2f), transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce((vector).normalized * shootingSpeed, ForceMode.Impulse);
        }
    }
}
