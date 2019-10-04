using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdCam_Script : MonoBehaviour
{
    private const float Y_ANGLE_MIN = 10.0f;
    private const float Y_ANGLE_MAX = 70.0f;

    public GameObject player;
    public Player_Script extScript;

    public float camDist_zoomed = 0.4f;
    
    [HideInInspector]
    public float distz = 2.0f;
    [HideInInspector]
    public float init_distz;

    private float curX = 0.0f;
    private float curY = 0.0f;

    private bool camPaused = false;
    float lerpTime;

    bool pause = false;
  
    // Start is called before the first frame update
    public void Start()
    {
        init_distz = distz;
    }

    // Update is called once per frame
    public void Update()
    {
        if (!pause)
        {
            camPaused = extScript.camP;

            if (camPaused)
            {

                if (extScript.lerpTime < 1.0f)
                {
                    distz = Mathf.Lerp(distz, distz + camDist_zoomed, extScript.lerpTime);
                }
            }

            curX += Input.GetAxis("Mouse X") * Time.deltaTime * 90;
            curY += Input.GetAxis("Mouse Y");
            curY = Mathf.Clamp(curY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        }
        
    }

    public void FixedUpdate()
    {
        if (!pause)
        {
            Vector3 dir = new Vector3(0, 0, -distz);
            Quaternion rotate = Quaternion.Euler(curY, curX, 0);
            transform.position = player.transform.position + rotate * dir;
            transform.LookAt(player.transform.position);
        }
    }

    public void ResetCamDist()
    {
        distz = init_distz;
    }

    public void Pause()
    {
        pause = !pause;
    }
}
