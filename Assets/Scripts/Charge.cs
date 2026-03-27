using System;
using UnityEngine;

public class Charge : MonoBehaviour
{
    [SerializeField] Transform l;
    [SerializeField] Transform r;
    [SerializeField] Transform center;
    [SerializeField] Transform HMD;
    [SerializeField] GameObject bomb;
    [SerializeField] Move move;
    [HideInInspector] public bool chargable = false;
    float chargeSpeed = 0.07f;
    bool facing = false;
    bool charging = false;
    GameObject obj;
    float size = 0.0f;
    int flameCount = 10;
    Vector3[] positions;
    float resetCount = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chargable = true;
        positions = new Vector3[flameCount];
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
        Vector3 subR = posR - center.position;
        Vector3 subL = posL - center.position;
        Shift((subR + subL) / 2);
        if(d1 > 0.80f && d2 > 0.80f)
        {
            facing = true;
        }
        else
        {
            facing = false;
        }
        float leftTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        float rightTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        if(obj != null)
        {
            obj.transform.position = (posL + posR) / 2;
        }
        if (facing)
        {
            resetCount = 0.0f;
            if (chargable)
            {
                if(charging == false)
                {
                    obj = Instantiate(bomb,(posL + posR) / 2,Quaternion.identity);
                    charging = true;
                    move.stop = true;
                    Reset();
                }
                float add = chargeSpeed;
                if(leftTrigger > 0.50f)
                {
                    add += chargeSpeed;
                }
                if(rightTrigger > 0.50f)
                {
                    add += chargeSpeed;
                }
                float maxsize = (posL - posR).magnitude / 2.0f * 0.75f;
                size += add * Time.deltaTime;
                size = Mathf.Min(size, maxsize);
                obj.transform.localScale = Vector3.one * size;
            }
        }
        else
        {
            resetCount += Time.deltaTime;
            if(resetCount >= 0.20f)
            {
                chargable = true;
            }
        }
        if (charging)
        {
            OVRInput.SetControllerVibration(1.0f,1.0f,OVRInput.Controller.RTouch);
            OVRInput.SetControllerVibration(1.0f,1.0f,OVRInput.Controller.LTouch);
        }
        else
        {
            OVRInput.SetControllerVibration(0.0f,0.0f,OVRInput.Controller.RTouch);
            OVRInput.SetControllerVibration(0.0f,0.0f,OVRInput.Controller.LTouch);
        }
        if(positions[flameCount - 1].magnitude >= 0.0001f)
        {
            Vector3 sub = positions[0] - positions[flameCount - 1];
            if(sub.magnitude >= 0.1f && charging)
            {
                charging = false;
                chargable = false;
                obj.GetComponent<GoForward>().vec = sub.normalized;
                obj.GetComponent<GoForward>().size = size;
                obj.GetComponent<GoForward>().move = true;
                obj.GetComponent<GoForward>().HMD = HMD;
                obj = null;
                size = 0.0f;
                move.stop = false;
            }
        }
    }
    void Shift(Vector3 vec)
    {
        for(int i = flameCount - 2;i >= 0; i--)
        {
            positions[i + 1] = positions[i];
        }
        positions[0] = vec;
    }
    void Reset()
    {
        for(int i = 0; i < flameCount; i++)
        {
            positions[i] = Vector3.zero;
        }
    }
}
