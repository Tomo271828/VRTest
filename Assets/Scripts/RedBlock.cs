using UnityEngine;

public class RedBlock : MonoBehaviour
{
    [SerializeField] OVRScreenFade fade;
    [SerializeField] Move move;
    [SerializeField] Charge charge;
    [SerializeField] Vector3 respawnPoint;
    GameObject player;
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            fade.fadeTime = 1.0f;
            fade.FadeOut();
            move.stop = true;
            charge.chargable = false;
            player = collision.gameObject;
            Invoke("Respawn",1.5f);
        }
    }
    void Respawn()
    {
        fade.fadeTime = 1.0f;
        fade.FadeIn();
        move.stop = false;
        charge.chargable = true;
        player.transform.position = respawnPoint;
    }
}
