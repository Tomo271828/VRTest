using System.Collections.Generic;
using UnityEngine;

public class GripMove : MonoBehaviour
{
    [SerializeField] string targetName = "CubePrefab(Clone)";
    Dictionary<GameObject, Vector3> dict = new Dictionary<GameObject, Vector3>();
    [SerializeField] bool touched = false;
    [SerializeField] bool griping = false;
    Vector3 startControllerPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dict.Count >= 1)
        {
            touched = true;
        }
        else
        {
            touched = false;
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            OVRInput.SetControllerVibration(1.0f,0.5f,OVRInput.Controller.RTouch);
        }
        else
        {
            OVRInput.SetControllerVibration(0.0f,0.0f,OVRInput.Controller.RTouch);
        }
        if(OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger) && touched)
        {
            griping = true;
            foreach(GameObject obj in dict.Keys)
            {
                dict[obj] = obj.transform.position;
            }
            startControllerPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        }
        if (griping)
        {
            if(OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
            {
                griping = false;
            }
            else
            {
                Vector3 diff = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch) - startControllerPos;
                foreach(GameObject obj in dict.Keys)
                {
                    //obj.transform.position = dict[obj] + diff;
                    obj.transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == targetName && griping == false)
        {
            dict.Add(other.gameObject, other.gameObject.transform.position);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == targetName && griping == false && dict.ContainsKey(other.gameObject))
        {
            dict.Remove(other.gameObject);
        }
    }
}
