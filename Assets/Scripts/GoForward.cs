using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoForward : MonoBehaviour
{
    [HideInInspector] public Vector3 vec;
    [HideInInspector] public float size;
    [HideInInspector] public bool move = false;
    [SerializeField] float speed = 0.05f;
    Transform tr;
    HashSet<GameObject> targets = new HashSet<GameObject>();
    float count = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tr = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(move)
        {
            count += Time.deltaTime;
            tr.position += vec * speed * Time.deltaTime;
            if(count >= 100.0f)
            {
                Destroy(this.gameObject);
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if(move == false)
        {
            return;
        }
        foreach(GameObject obj in targets)
        {
            if(obj.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                Vector3 dir = obj.transform.position - tr.position;
                float d = dir.magnitude + 0.0001f;
                dir = dir.normalized;
                rb.AddForce(dir * size * 100.0f / d,ForceMode.Impulse);
            }
        }
        Destroy(this.gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        targets.Add(other.gameObject);
    }
    void OnTriggerExit(Collider other)
    {
        targets.Remove(other.gameObject);
    }
}
