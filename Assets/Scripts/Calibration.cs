using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Calibration : MonoBehaviour
{
    [SerializeField] GameObject left;
    [SerializeField] GameObject right;
    [SerializeField] OVRScreenFade fade;
    [SerializeField] string sceneName;
    public static Quaternion offsetL;
    public static Quaternion offsetR;
    bool once = true;
    void Start()
    {
        StartCoroutine(StartCor());
    }
    IEnumerator StartCor()
    {
        fade.fadeTime = 0.0f;
        fade.FadeOut();
        yield return null;
        fade.fadeTime = 0.5f;
        fade.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        float leftTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        float rightTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        if(leftTrigger > 0.5f && rightTrigger > 0.5f && once)
        {
            once = false;
            Quaternion rotR = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
            Quaternion rotL = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
            Vector3 fR = rotR * Vector3.forward;
            Vector3 fL = rotL * Vector3.forward;
            offsetR = Quaternion.FromToRotation(fR,Vector3.up);
            offsetL = Quaternion.FromToRotation(fL,Vector3.up);
            fade.fadeTime = 0.5f;
            fade.FadeOut();
            Invoke("ChangeScene",0.6f);
        }
    }
    void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
