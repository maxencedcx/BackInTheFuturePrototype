using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Dictionary<int, infosHandler> objectsToBeRewind;
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
        objectsToBeRewind = new Dictionary<int, infosHandler>();
        objectsInfos = new Dictionary<int, ObjectInfo>();
    }

    void Update()
    {
        if (lastUpdate + cooldown <= Time.time)
        {
            lastUpdate = Time.time;
            updateAllInfos();
        }
    }

    public void updateAllInfos()
    {
        foreach (KeyValuePair<int, infosHandler> obj in objectsToBeRewind)
        {
            if (!objectsInfos.ContainsKey(obj.Key))
                objectsInfos.Add(obj.Key, obj.Value());
            else if (objectsInfos[obj.Key].lastUpdated + refreshRate <= Time.time)
                objectsInfos[obj.Key] = obj.Value();
        }
    }

    public void rewind()
    {
        foreach (KeyValuePair<int, ObjectInfo> obj in objectsInfos)
            objectsToBeRewind[obj.Key](obj.Value);
        objectsInfos.Clear();
        updateAllInfos();
    }

    public Vector3 getPlayerPos()
    {
        if (player != null)
            return player.transform.position;
        else
            return Vector3.zero;
    }
}
