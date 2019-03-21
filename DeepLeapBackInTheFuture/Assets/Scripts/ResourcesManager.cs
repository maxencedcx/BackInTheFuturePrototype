using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject playerClonePrefab;
    public GameObject enemyPrefab;
    public GameObject bulletPrefab;

    public static ResourcesManager instance;
    private Dictionary<string, GameObject> objects;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
        objects = new Dictionary<string, GameObject>();
        objects.Add("playerPrefab", playerPrefab);
        objects.Add("playerClonePrefab", playerClonePrefab);
        objects.Add("enemyPrefab", enemyPrefab);
        objects.Add("bulletPrefab", bulletPrefab);
    }

    public void Add(string name, GameObject obj)
    { objects.Add(name, obj); }

    public GameObject Get(string name)
    {
        if (objects.ContainsKey(name))
            return objects[name];
        else
        {
            GameObject tmp = Resources.Load<GameObject>(name);
            if (tmp != null)
            {
                Add(name, tmp);
                Debug.Log("[RESOURCES_MANAGER]: Loaded '" + name + "'.");
                return objects[name];
            }
            else
            {
                Debug.Log("[RESOURCES_MANAGER]: Could not load '" + name + "'.");
                return null;
            }
        }
    }
}
