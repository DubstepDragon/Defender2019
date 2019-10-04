using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_script : MonoBehaviour
{
    public float Rocket_Speed = 3.0f;
    public GameObject player;
    public GameObject _base;

    public bool kill = false;
    public float killTimer = 0.5f;

    [HideInInspector]
    public bool is_targeted = false;
    [HideInInspector]
    public bool was_hit = false;
    [HideInInspector]
    public Vector3 initialPos;

    Rigidbody rb;
    MeshRenderer rocketMesh;
    CapsuleCollider capsule;
    SphereCollider sphere;

    public GameObject explosion;
    public GameObject flame;

    public float damage = 1.0f;

    bool pause = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rocketMesh = transform.GetChild(0).GetComponent<MeshRenderer>();
        capsule = GetComponent<CapsuleCollider>();
        sphere = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!pause)
        {
            transform.position += transform.forward * Time.deltaTime * Rocket_Speed;
            if (transform.position.y < -5)
            {
                Destroy(transform.gameObject);
            }

            if (kill == true)
            {
                killTimer -= Time.fixedDeltaTime;
            }

            if (killTimer < 0.0f)
            {
                Destroy(transform.gameObject);
            }
        }
    }



    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Base")
        {
            _base.GetComponent<Base_script>().Hit(damage);
            Destroy(transform.gameObject);
            
        }
        if (other.gameObject.tag == "Ground")
        {
            capsule.enabled = false;
            sphere.enabled = true;
            /*
            GameObject voxChild = other.gameObject.GetComponent<Voxel_script>().child;
            if (voxChild != null)
            {   
                _base.GetComponent<Base_script>().Hit(1.0f);
            }
            */
            

        }
        if(other.gameObject.tag == "Cone")
        {
            other.SendMessageUpwards("CollWithRocket", transform.gameObject);
        }
        if(other.gameObject.tag == "Arrow")
        {
            Kill();
            Destroy(other.gameObject);
        }
        


    }

    public void Kill()
    {
        explosion.gameObject.SetActive(true);
        flame.gameObject.SetActive(false);
        kill = true;
        rocketMesh.enabled = false;
        
    }

    public void Pause()
    {
        pause = !pause;
    }
}
