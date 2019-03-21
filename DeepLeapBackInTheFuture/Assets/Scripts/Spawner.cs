using System.Collections;
using UnityEngine;

public class Spawner : Damageable
{
    [SerializeField] private float spawnCooldown;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy() {
        while (true) {
            yield return new WaitForSeconds(spawnCooldown);
            Instantiate(ResourcesManager.instance.Get("enemyPrefab"), transform.position + 3 * transform.forward, transform.rotation);
        }
    }
}
