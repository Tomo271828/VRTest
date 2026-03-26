using UnityEngine;

public class ControllerDirectionShow : MonoBehaviour
{
    [SerializeField] LineRenderer line;
    [SerializeField] Transform anchor;
    [SerializeField] bool r;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = anchor.position;
        Quaternion rot = anchor.rotation;
        if (r)
        {
            rot *= Calibration.offsetR;
        }
        else
        {
            rot *= Calibration.offsetL;
        }
        Vector3 forward = rot * Vector3.forward;
        line.SetPosition(0,pos);
        line.SetPosition(1,pos + forward * 2);
    }
}
