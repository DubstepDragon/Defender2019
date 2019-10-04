using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringPlaneScript : MonoBehaviour
{
    public Ground_script ground_Script;
    public Player_Script player;
    public GameObject cone;
    public GameObject plane;
    public GameObject arrow;
    public GameObject arrowParent;
    public Rocket_spawner_script rocketSpawner;

    private MeshRenderer coneMesh;
    private Vector3 initialPos;
    private Vector3 initialScale;

    Vector3 input;
    private bool camP = false;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        initialScale = transform.localScale;
        coneMesh = cone.GetComponent<MeshRenderer>();
        GameObject tempBase = ground_Script.spawnedBase;
        transform.position = new Vector3(tempBase.transform.position.x,
            transform.position.y,
            tempBase.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        camP = player.camP;
        if(camP)
        {
            coneMesh.enabled = true;
        }
        else
        {
            coneMesh.enabled = false;
        }
    }

    void FixedUpdate()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetAxis("RaiseLower"));

        cone.transform.localScale += new Vector3(Time.deltaTime * input.x * player.move_speed, Time.deltaTime * input.y * player.move_speed, 0);

        //plane.transform.position += new Vector3(0, Time.deltaTime * input.z * player.move_speed, 0);
        cone.transform.position += new Vector3(0, Time.deltaTime * input.z * player.move_speed, 0);
    }

    void Fire(GameObject target)
    {
        Vector3 pos = ground_Script.launchers[Random.Range(0, ground_Script.launchers.Count)].transform.position;
        GameObject new_inst = Instantiate(arrow, pos, arrow.transform.rotation);
        new_inst.transform.localScale = arrow.transform.localScale;
        new_inst.transform.parent = arrowParent.transform;
        new_inst.GetComponent<ArrowScript>().target = target;
    }

    void CollWithRocket(GameObject rocket)
    {
        Rocket_script RS = rocket.GetComponent<Rocket_script>();
        if(!RS.is_targeted)
        {
            RS.is_targeted = true;
        }
        Fire(rocket.gameObject);
    }
}
