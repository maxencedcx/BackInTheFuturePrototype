using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate ObjectInfo infosHandler(ObjectInfo infos = null);

public class ObjectToRewind : MonoBehaviour
{
    private static int lastId = 0;
    protected ObjectInfo.Type type = ObjectInfo.Type.DEFAULT;
    protected int id;

    protected void Start()
    {
        id = ++lastId;
        GameManager.instance.objectsToBeRewind.Add(id, manageInfos);
    }

    protected void OnDestroy()
    {
        Debug.Log(type.ToString() + " DIED.");
        if (GameManager.instance.objectsToBeRewind.ContainsKey(id))
            GameManager.instance.objectsToBeRewind.Remove(id);
        if (GameManager.instance.objectsInfos.ContainsKey(id))
            GameManager.instance.objectsInfos.Remove(id);
    }

    public void DestroyMe()
    { Destroy(gameObject); }

    public void rewind(ObjectInfo infos)
    {
        Debug.Log("REWINDING " + type.ToString());
        if (infos.createdAt + GameManager.instance.getRefreshRate() > Time.time + GameManager.instance.getCooldown())
            Destroy(gameObject);
        else
        {
            gameObject.transform.position = infos.position;
            gameObject.transform.rotation = infos.rotation;
        }
    }

    public ObjectInfo manageInfos(ObjectInfo infos = null)
    {
        if (infos != null)
            rewind(infos);
        return new ObjectInfo(gameObject.transform.position, gameObject.transform.rotation, type);
    }
}
