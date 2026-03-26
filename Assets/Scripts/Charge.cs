using System;
using UnityEngine;

public class Charge : MonoBehaviour
{
    [SerializeField] Transform l;
    [SerializeField] Transform r;
    [SerializeField] GameObject bomb;
    [SerializeField] float chargeSpeed = 0.01f;
    bool facing = false;
    bool charging = false;
    GameObject obj;
    float size = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotR = r.rotation * Calibration.offsetR;
        Quaternion rotL = l.rotation * Calibration.offsetL;
        Vector3 fR = rotR * Vector3.forward;
        Vector3 fL = rotL * Vector3.forward;
        fR = fR.normalized;
        fL = fL.normalized;
        Vector3 posR = r.position;
        Vector3 posL = l.position;
        Vector3 dirRL = (posL - posR).normalized;
        Vector3 dirLR = (posR - posL).normalized;
        float d1 = Vector3.Dot(fR,dirRL);
        float d2 = Vector3.Dot(fL,dirLR);
        if(d1 > 0.80f && d2 > 0.80f)
        {
            facing = true;
            OVRInput.SetControllerVibration(1.0f,1.0f,OVRInput.Controller.RTouch);
            OVRInput.SetControllerVibration(1.0f,1.0f,OVRInput.Controller.LTouch);
        }
        else
        {
            facing = false;
            OVRInput.SetControllerVibration(0.0f,0.0f,OVRInput.Controller.RTouch);
            OVRInput.SetControllerVibration(0.0f,0.0f,OVRInput.Controller.LTouch);
        }
        float leftTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        float rightTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        if(obj != null)
        {
            obj.transform.position = (posL + posR) / 2;
        }
        if (facing)
        {
            if(charging == false)
            {
                obj = Instantiate(bomb,(posL + posR) / 2,Quaternion.identity);
                charging = true;
            }
            float add = chargeSpeed;
            if(leftTrigger > 0.50f)
            {
                add *= 2;
            }
            if(rightTrigger > 0.50f)
            {
                add *= 2;
            }
            float maxsize = (posL - posR).magnitude / 2.0f * 0.75f;
            size += add * Time.deltaTime;
            size = Mathf.Min(size, maxsize);
            obj.transform.localScale = Vector3.one * size;
        }
        else
        {
            float d = Vector3.Dot(fR,fL);
            if(d > 0.30f)
            {
                charging = false;
                obj.GetComponent<GoForward>().vec = ((fR + fL) / 2).normalized;
                obj.GetComponent<GoForward>().size = size;
                obj.GetComponent<GoForward>().move = true;
                obj = null;
                size = 0.0f;
            }
        }
    }
}
