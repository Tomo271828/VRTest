using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] GameObject HMD;
    [SerializeField] float dist = 0.5f;
    [SerializeField] float speed = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            Vector3 vec = HMD.transform.position;
            vec.z += dist;
            HMD.transform.position = vec;
        }
        Vector2 stick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        float sx = stick.x;
        float sz = stick.y;
        if(stick.magnitude > 0.10f)
        {
            Vector3 vec = HMD.transform.position;
            vec.x += sx * speed;
            vec.z += sz * speed;
            HMD.transform.position = vec;
        }
    }
}
