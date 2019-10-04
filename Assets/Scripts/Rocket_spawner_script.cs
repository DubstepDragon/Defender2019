using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_spawner_script : MonoBehaviour
{
    public GameObject rocket;
    public GameObject CoinParent;
    public GameObject coin;
    public Ground_script Ground_spawner;
    public float Spawn_Timer = 2.0f;
    private float Spawn_timer_max;

    bool pause = false;
    [HideInInspector]
    
    // Start is called before the first frame update
    void Start()
    {
        Spawn_timer_max = Spawn_Timer;
        Spawn_Timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause)
        {
            if (Spawn_Timer < Spawn_timer_max)
            {
                Spawn_Timer += Time.deltaTime;
            }
            else
            {

                Voxel_script vox = Ground_spawner.RandomVox().GetComponent<Voxel_script>();
                if (!vox.is_targeted)
                {
                    vox.is_targeted = true;
                    SpawnRocket(vox.transform.position);
                    SpawnCoin();
                    
                }
                else
                {
                    while (true)
                    {
                        vox = Ground_spawner.RandomVox().GetComponent<Voxel_script>();
                        if (!vox.is_targeted)
                        {
                            vox.is_targeted = true;
                            SpawnRocket(vox.transform.position);
                            SpawnCoin();
                            break;
                        }
                    }
                }
                Spawn_Timer = 0.0f;

            }
        }

        
     
    }

    void SpawnRocket(Vector3 vox)
    {
        Vector3 pos = RandomCircle(transform.position, Random.Range(5, 15), Random.Range(0, 360));
        GameObject new_inst = Instantiate(rocket, pos, rocket.transform.rotation);
        new_inst.transform.localScale = rocket.transform.localScale;
        new_inst.transform.parent = this.transform;
        new_inst.transform.LookAt(vox);
       
    }

    Vector3 RandomCircle(Vector3 center, float radius, float ang)
    {
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }

    public void Pause()
    {
        pause = !pause;
    }

    public void SpawnCoin()
    {
        int temp = Random.Range(0, 4);
        Vector3 pos = RandomCircle(transform.position, Random.Range(5, 25), Random.Range(0, 360));
        GameObject new_inst = Instantiate(coin, pos, rocket.transform.rotation);
        new_inst.transform.localScale = coin.transform.localScale;
        new_inst.transform.parent = CoinParent.transform;
    }
}
