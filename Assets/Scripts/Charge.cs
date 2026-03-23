using UnityEngine;

public class Charge : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotR = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
        Quaternion rotL = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        Vector3 fR = rotR * Vector3.forward;
        Vector3 fL = rotL * Vector3.forward;
        float leftTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        float rightTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        if(Vector3.Dot(fR,fL) < -0.80f)
        {
            OVRInput.SetControllerVibration(1.0f,1.0f,OVRInput.Controller.RTouch);
            OVRInput.SetControllerVibration(1.0f,1.0f,OVRInput.Controller.LTouch);
        }
        else
        {
            OVRInput.SetControllerVibration(0.0f,0.0f,OVRInput.Controller.RTouch);
            OVRInput.SetControllerVibration(0.0f,0.0f,OVRInput.Controller.LTouch);
        }
    }
}
