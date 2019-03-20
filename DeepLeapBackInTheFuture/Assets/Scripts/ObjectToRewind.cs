using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate ObjectInfo infosHandler(ObjectInfo infos = null);

public class ObjectToRewind : MonoBehaviour
{
    private static int lastId = 0;
    protected ObjectInfo.Type type = ObjectInfo.Type.DEFAULT;
    protected int id;

    private void Start()
    {
        register();
    }

    protected void register()
    {
        id = ++lastId;
        GameManager.instance.objectsToBeRewind.Add(id, manageInfos);
    }

    private void OnDestroy()
    {
        if (GameManager.instance.objectsToBeRewind.ContainsKey(id))
            GameManager.instance.objectsToBeRewind.Remove(id);
        if (GameManager.instance.objectsInfos.ContainsKey(id))
            GameManager.instance.objectsInfos.Remove(id);
    }

    public void rewind(ObjectInfo infos)
    {
        Debug.Log("REWINDING " + type.ToString());
        gameObject.transform.position = infos.position;
        gameObject.transform.rotation = infos.rotation;
    }

    public ObjectInfo manageInfos(ObjectInfo infos = null)
    {
        if (infos != null)
            rewind(infos);
        return new ObjectInfo(gameObject.transform.position, gameObject.transform.rotation, type);
    }
}
