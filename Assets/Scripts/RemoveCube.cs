using UnityEngine;
using System.Collections.Generic;

public class RemoveCube : MonoBehaviour
{
    [SerializeField] string targetName = "CubePrefab(Clone)";
    HashSet<GameObject> set = new HashSet<GameObject>();
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == targetName)
        {
            set.Add(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == targetName && set.Contains(other.gameObject))
        {
            set.Remove(other.gameObject);
        }
    }
    void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.Two))
        {
            foreach(var obj in set)
            {
                Destroy(obj);
            }
            set.Clear();
        }
    }
}
