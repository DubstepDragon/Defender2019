using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher_Script : MonoBehaviour
{
    public bool containPlayer = false;
    [HideInInspector]
    public bool is_alive = true;

    MeshRenderer mesh;
    Collider coll;
    Voxel_script parent;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        coll = GetComponent<Collider>();
        parent = GetComponentInParent<Voxel_script>();
    }

    // Update is called once per frame
    void Update()
    {
        if(parent.was_hit == true)
        {
            mesh.enabled = false;
            coll.enabled = false;
            is_alive = false;
        }
    }

    public void RespawnLauncher()
    {
        mesh.enabled = true;
        is_alive = true;
        coll.enabled = true;
    }
}
