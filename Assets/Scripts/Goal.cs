using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] bool tutorial = false;
    [SerializeField] OVRScreenFade fade;
    [SerializeField] string sceneName = "StageClearScene";
    [SerializeField] string nextStageName = "Stage1";
    [SerializeField] string clearedSceneName = "Stage1";
    [SerializeField] Move move;
    [SerializeField] Charge charge;
    public static string nextScene;
    public static string clearedScene;
    public static float clearTimeSum = 0.0f;
    public static float clearTime = 0.0f;
    float count = 0.0f;
    bool once = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (tutorial)
        {
            clearTimeSum = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && once)
        {
            once = false;
            move.stop = true;
            charge.chargable = false;
            clearTime = count;
            clearTimeSum += count;
            nextScene = nextStageName;
            clearedScene = clearedSceneName;
            fade.fadeTime = 1.0f;
            fade.FadeOut();
            Invoke("ChangeScene",1.5f);
        }
    }
    void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
