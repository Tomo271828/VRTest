using UnityEngine;
using UnityEngine.SceneManagement;

public class Calibration : MonoBehaviour
{
    [SerializeField] GameObject left;
    [SerializeField] GameObject right;
    [SerializeField] string sceneName;
    public static Quaternion offsetL;
    public static Quaternion offsetR;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float leftTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        float rightTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        if(leftTrigger > 0.5f && rightTrigger > 0.5f)
        {
            Quaternion rotR = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
            Quaternion rotL = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
            Vector3 fR = rotR * Vector3.forward;
            Vector3 fL = rotL * Vector3.forward;
            offsetR = Quaternion.FromToRotation(fR,Vector3.up);
            offsetL = Quaternion.FromToRotation(fL,Vector3.up);
            SceneManager.LoadScene(sceneName);
        }
    }
}
