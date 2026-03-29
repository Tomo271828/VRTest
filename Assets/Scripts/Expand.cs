using Meta.WitAi;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Expand : MonoBehaviour
{
    [HideInInspector] public bool started = false;
    [HideInInspector] public bool once = true;
    //true: 次のステージ
    //false: やめる
    [SerializeField] bool mode = true;
    [SerializeField] Expand other;
    [SerializeField] OVRScreenFade fade;
    float count = 0.0f;
    float size = 0.0f;
    float max = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            count += Time.deltaTime;
            size = Mathf.Min(count, max);
            transform.localScale = new Vector3(size, size, size);
            transform.Rotate(new Vector3(0.0f,90.0f,0.0f) * Time.deltaTime);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (started && once && collision.gameObject.tag == "Bullet")
        {
            once = false;
            other.once = false;
            fade.fadeTime = 1.0f;
            fade.FadeOut();
            if (mode)
            {
                Invoke("ChangeScene",1.5f);
            }
            else
            {
                Invoke("Quit",1.5f);
            }
        }
    }
    void ChangeScene()
    {
        SceneManager.LoadScene(Goal.nextScene);
    }
    void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
