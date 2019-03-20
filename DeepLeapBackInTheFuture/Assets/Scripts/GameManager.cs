using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Dictionary<int, GameObject> objectsToBeRewind;
    public Dictionary<int, ObjectInfo> objectsInfos; 
    public GameObject player;

    protected float lastUpdate = 0;
    protected float cooldown = 0.1f;
    protected float refreshRate = 2;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(instance);
        else
            DontDestroyOnLoad(gameObject);
        objectsToBeRewind = new Dictionary<int, GameObject>();
        objectsInfos = new Dictionary<int, ObjectInfo>();
    }

    void Update()
    {
        if (lastUpdate + cooldown <= Time.time)
        {
            lastUpdate = Time.time;
            foreach (KeyValuePair<int, GameObject> obj in objectsToBeRewind)
            {
                if (!objectsInfos.ContainsKey(obj.Key))
                    objectsInfos.Add(obj.Key, new ObjectInfo(obj.Value.transform.position, obj.Value.transform.rotation));
                else if (objectsInfos[obj.Key].lastUpdated + refreshRate <= Time.time)
                {
                    objectsInfos[obj.Key].rotation = obj.Value.transform.rotation;
                    objectsInfos[obj.Key].position = obj.Value.transform.position;
                    objectsInfos[obj.Key].lastUpdated = Time.time;
                }
            }
;        }
    }

    public void rewind()
    {
        foreach (KeyValuePair<int, ObjectInfo> obj in objectsInfos)
        {
            objectsToBeRewind[obj.Key].transform.position = obj.Value.position;
            objectsToBeRewind[obj.Key].transform.rotation = obj.Value.rotation;
        }
        objectsInfos.Clear();
        foreach (KeyValuePair<int, GameObject> obj in objectsToBeRewind)
        {
            if (!objectsInfos.ContainsKey(obj.Key))
                objectsInfos.Add(obj.Key, new ObjectInfo(obj.Value.transform.position, obj.Value.transform.rotation));
            else if (objectsInfos[obj.Key].lastUpdated + refreshRate <= Time.time)
            {
                objectsInfos[obj.Key].rotation = obj.Value.transform.rotation;
                objectsInfos[obj.Key].position = obj.Value.transform.position;
                objectsInfos[obj.Key].lastUpdated = Time.time;
            }
        }
    }

    public Vector3 getPlayerPos()
    { return player.transform.position; }
}
