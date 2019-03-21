using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Dictionary<int, infosHandler> objectsToBeRewind;
    public Dictionary<int, List<ObjectInfo>> objectsInfos; 
    public GameObject player;

    protected float lastUpdate = 0;
    protected float cooldown = 0.2f;
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
        objectsInfos = new Dictionary<int, List<ObjectInfo>>();
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
                objectsInfos.Add(obj.Key, new List<ObjectInfo>());
            else
                objectsInfos[obj.Key].Add(obj.Value());
        }
    }

    public void rewind()
    {
        removeOldInfos();
        foreach (KeyValuePair<int, List<ObjectInfo>> obj in objectsInfos)
            objectsToBeRewind[obj.Key](obj.Value.FirstOrDefault());
        objectsInfos.Clear();
        updateAllInfos();
    }

    private bool isOldInfo(ObjectInfo info)
    { return (Time.time - refreshRate > info.createdAt); }

    private void removeOldInfos()
    {
        foreach (KeyValuePair<int, List<ObjectInfo>> obj in objectsInfos)
        {
            obj.Value.RemoveAll(isOldInfo);
            obj.Value.OrderBy(x => x.createdAt);
        }
    }

    public Vector3 getPlayerPos()
    {
        if (player != null)
            return player.transform.position;
        else
            return Vector3.zero;
    }

    public float getRefreshRate()
    { return refreshRate; }

    public float getCooldown()
    { return cooldown; }
}
