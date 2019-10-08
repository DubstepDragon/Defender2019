using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private bool kill = false;
    private float killTimer = 1.0f;
    MeshRenderer mesh;
    Collider coll;
    private AudioSource cAudio;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        cAudio = GetComponent<AudioSource>();
        coll = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -10 || killTimer < 0)
        {
            Destroy(transform.gameObject);
        }
        if(kill)
        {
            killTimer -= Time.deltaTime;
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            Kill();
        }
    }

    public void Kill()
    {
        kill = true;
        mesh.enabled = false;
        coll.enabled = false;
        cAudio.Play();

    }

}
