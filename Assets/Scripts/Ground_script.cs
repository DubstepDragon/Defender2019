using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_script : MonoBehaviour
{

    public GameObject groundBase;
    public GameObject launcher;
    public GameObject playerBase;
    public Camera MinimapCam;
    public float ground_spacing;
    public int ground_w, ground_l;
    public float amplitude;
    public int numLaunchers;
    public float LauncherSpacing;


    [HideInInspector]
    public List<GameObject> voxels;
    [HideInInspector]
    public List<GameObject> launchers;
    [HideInInspector]
    public GameObject spawnedBase;
    // Start is called before the first frame update
    void Awake()
    {

        //Ground
        Vector3 Midpoint = new Vector3(-(ground_w / 2), 0, -(ground_l / 2));
        transform.position = Midpoint;
        Bounds ground_bound = groundBase.GetComponent<MeshRenderer>().bounds;
        for (int i = 0; i < ground_l; i++)
        {
            for (int j = 0; j < ground_w; j++)
            {
                Vector3 pos = new Vector3(i * (ground_bound.size.z + ground_spacing),
                    (Mathf.Sin(i/Mathf.PI) * Mathf.Sin(j/Mathf.PI)) * amplitude,
                    j * (ground_bound.size.x + ground_spacing));
                
                GameObject new_inst = Instantiate(groundBase, pos + transform.position, groundBase.transform.rotation);
                new_inst.transform.localScale = groundBase.transform.localScale;
                new_inst.transform.parent = this.transform;

                voxels.Add(new_inst);
  
                
            }
        }

        

        //Launchers
        int rotAng = 360 / numLaunchers;
        Vector3 center = new Vector3(0, 1, 0);
        Bounds launcher_bound = launcher.GetComponent<MeshRenderer>().bounds;
        for (int i = 0; i < numLaunchers; i++)
        {
            float ang = i * rotAng;
            Vector3 pos = RandomCircle(center, LauncherSpacing, ang);
            Transform toParent = GetClosest(voxels, pos);
            Vector3 tempPos = new Vector3(toParent.position.x, toParent.position.y + launcher_bound.size.y / 2, toParent.position.z);
            GameObject new_launcher = Instantiate(launcher, tempPos, launcher.transform.rotation);
            new_launcher.transform.localScale = launcher.transform.localScale;
            new_launcher.transform.parent = toParent;
            launchers.Add(new_launcher);
          
        }

        Transform toParent2 = GetClosest(voxels, center);
        Bounds base_bounds = playerBase.GetComponent<MeshRenderer>().bounds;
        playerBase.GetComponent<Base_script>().shield = GameObject.Find("Health");
        Vector3 tempPos2 = new Vector3(toParent2.position.x, base_bounds.size.y / 3, toParent2.position.z);
        GameObject new_base = Instantiate(playerBase, tempPos2, playerBase.transform.rotation);
        new_base.transform.localScale = playerBase.transform.localScale;
        new_base.transform.parent = toParent2;
        spawnedBase = new_base;
        
    }


    Vector3 RandomCircle(Vector3 center, float radius, float ang)
    {
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }

    Transform GetClosest(List<GameObject> objs, Vector3 cur)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = cur;
        foreach (GameObject t in objs)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin.transform;
    }

    public GameObject RandomVox()
    {
        return voxels[Random.Range(0, voxels.Count)].gameObject;
    }
}
