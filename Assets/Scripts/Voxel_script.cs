using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voxel_script : MonoBehaviour
{
    public Player_Script player;

    [HideInInspector]
    public GameObject child; 

    [HideInInspector]
    public bool is_targeted = false;
    [HideInInspector]
    public bool was_hit = false;
    [HideInInspector]
    public Vector3 initialPos;

    Rigidbody rb;
    MeshRenderer mesh;
    MeshCollider coll;

    bool pause = false;
    // Start is called before the first frame update
    void Start()
    {
        if(transform.childCount == 0)       
            child = null;       
        else
            child = transform.GetChild(0).gameObject;

        initialPos = transform.position;
        rb = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();
        coll = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause)
        {
            if (transform.position.y < -10)
            {
                mesh.enabled = false;
                rb.isKinematic = true;
                rb.useGravity = false;
                coll.enabled = false;
                transform.position = initialPos;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Rocket")
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            is_targeted = false;
            was_hit = true;
            other.gameObject.GetComponent<Rocket_script>().Kill();
        }
    }

    public void Respawn()
    {
        mesh.enabled = true;
        was_hit = false;
        coll.enabled = true;
    }

    public void Pause()
    {
        pause = !pause;
    }
}
