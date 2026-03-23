using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCube : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    Queue<GameObject> queue = new Queue<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            GameObject obj = Instantiate(prefab,OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch),Quaternion.identity);
            /*
            queue.Enqueue(obj);
            if(queue.Count > 10)
            {
                Destroy(queue.Dequeue());
            }
            */
        }
    }
}
