using UnityEngine;

public class RightControllerTracking : MonoBehaviour
{
    Transform tr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tr = this.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        tr.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
    }
}
