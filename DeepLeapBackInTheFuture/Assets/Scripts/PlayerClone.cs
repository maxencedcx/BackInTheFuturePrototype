using System.Collections.Generic;
using UnityEngine;

public class PlayerClone : MonoBehaviour {
    [SerializeField] private float shootingSpeed;
    public Queue<KeyValuePair<float, GameManager.Key>> playerInputs;

    void Update() {
        while (playerInputs.Count > 0 && playerInputs.Peek().Key < Time.time) {
            GameManager.Key key = playerInputs.Dequeue().Value;
            if (key.isMove)
                Move(key);
            else
                Shoot(key);
        }
    }

    private void Move(GameManager.Key key) {
        gameObject.transform.Translate(key.movement);
    }

    private void Shoot(GameManager.Key key) {
        Vector3 vector = key.mousePos - Camera.main.WorldToScreenPoint(transform.position);
        vector.z = vector.y;
        vector.y = 0;
        GameObject bullet = Instantiate(ResourcesManager.instance.Get("bulletPrefab"), transform.position + ((vector).normalized * 1.2f), transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce((vector).normalized * shootingSpeed, ForceMode.Impulse);
    }
}
