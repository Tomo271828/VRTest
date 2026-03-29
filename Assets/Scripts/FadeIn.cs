using System.Collections;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    [SerializeField] OVRScreenFade fade;
    [SerializeField] float time = 0.5f;    
    void Start()
    {
        StartCoroutine(StartCor());
    }
    IEnumerator StartCor()
    {
        fade.fadeTime = 0.0f;
        fade.FadeOut();
        yield return null;
        fade.fadeTime = time;
        fade.FadeIn();
    }
}
