using UnityEngine;

public class Move : MonoBehaviour
{
    float speed = 2.0f;
    [SerializeField] Transform l;
    [SerializeField] Transform r;
    [SerializeField] Transform HMD;
    [HideInInspector] public bool stop = false;
    [SerializeField] float gravityScale = 0.50f;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = HMD.gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
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
        if(d > 0.25f)
        {
            Vector3 dir = fR + fL;
            dir = dir.normalized;
            dir.y = 0.0f;
            if(dir.magnitude <= 0.40f)
            {
                return;
            }
            float s = speed;
            float leftTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
            float rightTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
            if(leftTrigger > 0.50f)
            {
                s += speed * 0.50f;
            }
            if(rightTrigger > 0.50f)
            {
                s += speed * 0.50f;
            }
            HMD.position += dir * s * Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        Vector3 g = Physics.gravity * gravityScale;
        rb.AddForce(g,ForceMode.Acceleration);
    }
}
