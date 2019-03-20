using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float shootingSpeed;

    void Start()
    {
        GameManager.instance.objectsToBeRewind.Add(1, gameObject);
    }

    void Update()
    {
        Move();
        Shoot();
    }

    private void Move() {
        Vector3 move = Vector3.zero;

        move.x = Input.GetAxis("Horizontal") * Time.deltaTime * 8;
        move.z = Input.GetAxis("Vertical") * Time.deltaTime * 8;

        if (Input.GetKeyDown(KeyCode.Space))
            GameManager.instance.rewind();

        gameObject.transform.Translate(move);
    }

    private void Shoot() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Vector3 vector = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            vector.z = vector.y;
            vector.y = 0;
            GameObject bullet = Instantiate(ResourcesManager.instance.Get("bulletPrefab"), transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce((vector).normalized * shootingSpeed, ForceMode.Impulse);
        }
    }
}
