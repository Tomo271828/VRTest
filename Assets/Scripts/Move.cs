using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float speed = 0.05f;
    [SerializeField] Transform l;
    [SerializeField] Transform r;
    [SerializeField] Transform HMD;
    [HideInInspector] public bool stop = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stop)
        {
            return;
        }
        Quaternion rotR = r.rotation * Calibration.offsetR;
        Quaternion rotL = l.rotation * Calibration.offsetL;
        Vector3 fR = rotR * Vector3.forward;
        Vector3 fL = rotL * Vector3.forward;
        fR = fR.normalized;
        fL = fL.normalized;
        float d = Vector3.Dot(fR,fL);
        if(d > 0.50f)
        {
            Vector3 dir = fR + fL;
            dir = dir.normalized;
            dir.y = 0.0f;
            if(dir.magnitude <= 0.40f)
            {
                return;
            }
            dir = dir.normalized;
            HMD.position += dir * speed * Time.deltaTime;
        }
    }
}
