using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleScript : MonoBehaviour
{
    
    public PauseScript pauseMenu;

    private bool Local_pause;
    private Vector3 initialPos;
    private Vector2 input;
    private float move_speed = 0.5f;

  
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        Local_pause = pauseMenu.pauseTime;
        if(Local_pause)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            transform.position += new Vector3(input.x * move_speed, 0, input.y * move_speed);
            
            if (Input.GetButtonDown("Action"))
            {
                transform.position = initialPos;    
            }
        }
    }
}
