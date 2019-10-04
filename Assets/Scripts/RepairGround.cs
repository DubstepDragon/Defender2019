using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairGround : MonoBehaviour
{
    public Player_Script player;
    public PauseScript pauseMenu;
    public Ground_script groundS;
    private static List<GameObject> underMe;
   
    private Collider coll;
    public int listlength;
    // Start is called before the first frame update
    void Start()
    {
        underMe = new List<GameObject>();
        coll = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        listlength = underMe.Count;
        if (pauseMenu.pauseTime)
        {
            
            if (Input.GetButtonDown("Action2"))
            {
                print("heal");
                for(int i = 0; i < underMe.Count - 1; i++)
                {
                    
                    if (player.money > 0)
                    {
                        
                        if (groundS.voxels.Contains(underMe[i].gameObject))
                        {
                            groundS.voxels[groundS.voxels.IndexOf(underMe[i].gameObject)].GetComponent<Voxel_script>().Respawn();
                            player.Pay(1);
                        }
                    }
                }
            }
        }
        else
        {
            

        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (!underMe.Contains(other.gameObject))
        {
            underMe.Add(other.gameObject);
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (underMe.Contains(other.gameObject))
        {
            underMe.Remove(other.gameObject);
        }
    }
}
