using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.objectsToBeRewind.Add(1, gameObject);
    }

    void Update()
    {
        Vector3 move = Vector3.zero;

        move.x = Input.GetAxis("Horizontal") * Time.deltaTime * 8;
        move.z = Input.GetAxis("Vertical") * Time.deltaTime * 8;

        if (Input.GetKeyDown(KeyCode.Space))
            GameManager.instance.rewind();

        gameObject.transform.Translate(move);
    }
}
