using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [HideInInspector] public bool broken = false;
    bool started = false;
    bool shrinking = false;
    float rand = 0.0f;
    float count = 0.0f;
    Vector3 speed;
    Transform tr;
    void Start()
    {
        tr = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (broken)
        {
            if(started == false)
            {
                rand = Random.Range(2.0f,3.0f);
                Invoke("StartShrink",rand);
                started = true;
            }
        }
        if (shrinking)
        {
            tr.localScale -= speed * Time.deltaTime;
            count += Time.deltaTime;
            if(count >= 1.5f)
            {
                Destroy(this.gameObject);
            }
        }
    }
    void StartShrink()
    {
        speed = tr.localScale / 1.5f;
        shrinking = true;
    }
}
