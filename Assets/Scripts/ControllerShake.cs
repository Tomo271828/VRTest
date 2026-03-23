using UnityEngine;

public class ControllerShake : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float leftTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        float rightTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        if(rightTrigger > 0.01f)
        {
            OVRInput.SetControllerVibration(1.0f,rightTrigger,OVRInput.Controller.RTouch);
        }
        else
        {
            OVRInput.SetControllerVibration(0.0f,0.0f,OVRInput.Controller.RTouch);
        }
        if(leftTrigger > 0.1f)
        {
            OVRInput.SetControllerVibration(1.0f,leftTrigger,OVRInput.Controller.LTouch);
        }
        else
        {
            OVRInput.SetControllerVibration(0.0f,0.0f,OVRInput.Controller.LTouch);
        }
    }
}
