using UnityEngine;

public class BoostWall : MonoBehaviour
{
    [SerializeField] Transform HMD;
    [SerializeField] float r = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 dir = HMD.forward;
        dir.y = 0.0f;
        dir = dir.normalized;
        dir = dir * -1.0f * r;
        Vector3 pos = HMD.position + dir;
        pos.y = HMD.position.y;
        this.transform.position = pos;
        Vector3 lookDir = HMD.position - pos;
        lookDir.y = 0.0f;
        if(lookDir.magnitude > 0.001f)
        {
            this.transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }
}
