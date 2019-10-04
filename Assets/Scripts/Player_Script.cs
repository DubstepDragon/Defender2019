using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_Script : MonoBehaviour
{
    public float move_speed = 1.0f;
    public float camLerpSpeed = 0.1f;
    public Transform cam;
    public Transform fullPlayerTF;
    public Ground_script Ground_spawner;
    public int money;
    public int moneyAmount;
    public PauseScript pauseMenu;
    public GameObject ArmParent;
    public TextMeshProUGUI scoreTxt;
    public GameObject minimap;

    public Camera mainCam;
    public Camera baseCam;

    private Rigidbody armRB;
    private SkinnedMeshRenderer personMesh;
    
    [HideInInspector]
    public float lerpTime = 0.0f;
    [HideInInspector]
    public bool camP = false;

    float MS;
    float heading = 0;
    Vector3 input;
    Vector3 tempPlayerPos;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        money = 0;
        MS = move_speed;
        armRB = transform.Find("Armature_parentTF").GetComponent<Rigidbody>();
        personMesh = transform.Find("Person_mesh").GetComponent<SkinnedMeshRenderer>();
        armRB = ArmParent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
      

        if (Input.GetButton("FreeMouse"))
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }

        if (!camP)
        {
            if (Input.GetButton("Sprint"))
            {
                move_speed = 5;
            }
            else
            {
                move_speed = MS;
            }
            if (Input.GetButtonDown("Action"))
            {
                //secondary action?
            }
        }
        else
        {
            if (Input.GetButtonDown("Action"))
            {
                Dismount();
            }

                
                
        }

        if (ArmParent.transform.position.y < -20)
        {
            armRB.useGravity = false;
            GameObject voxobj = Ground_spawner.RandomVox();
            Voxel_script vox = voxobj.GetComponent<Voxel_script>();
            Bounds voxBounds = voxobj.gameObject.GetComponent<MeshRenderer>().bounds;
            if (!vox.is_targeted)
            {

                    
                Dismount_pos(new Vector3(vox.transform.position.x,
                                            vox.transform.position.y + (voxBounds.size.y / 2),
                                            vox.transform.position.z));
                    
            }
            else
            {
                while (true)
                {
                    vox = Ground_spawner.RandomVox().GetComponent<Voxel_script>();
                    voxBounds = voxobj.gameObject.GetComponent<MeshRenderer>().bounds;
                    if (!vox.is_targeted)
                    {
                            
                        Dismount_pos(new Vector3(vox.transform.position.x,
                                                    vox.transform.position.y + (voxBounds.size.y / 2),
                                                    vox.transform.position.z));
                           
                        break;
                    }
                }
            }

        }

        if (!pauseMenu.pauseTime)
        {
            baseCam.enabled = false;
            mainCam.enabled = true;
            minimap.SetActive(true);
        }
        else
        {          
            mainCam.enabled = false;
            baseCam.enabled = true;
            minimap.SetActive(false);
        }

    }

    private void FixedUpdate()
    {
        heading += Input.GetAxis("Mouse X") * Time.deltaTime * 90;
        fullPlayerTF.rotation = Quaternion.Euler(0, heading, 0);
        if (!camP)
            input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        else        
            input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetAxis("RaiseLower"));
                          

        Vector3 CamF = cam.forward;
        Vector3 CamR = cam.right;

        CamF.y = 0;
        CamR.y = 0;

        CamF = CamF.normalized;
        CamR = CamR.normalized;

        
        if (!camP)
        {
            
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1 ||
                Mathf.Abs(Input.GetAxis("Vertical")) > 0.1)
            {
                setRunAnim(true);
            }
            else
            {
                setRunAnim(false);
            }

            //transform.position += new Vector3(input.x, 0, input.y) * Time.deltaTime * move_speed;
            transform.position += (CamF * input.y + CamR * input.x) * Time.deltaTime * move_speed;

        }
        else
        {
            if (lerpTime < 1.0f)
            {
                lerpTime += Time.deltaTime * camLerpSpeed;
                fullPlayerTF.position = Vector3.Lerp(tempPlayerPos, new Vector3(0, 10, 0), lerpTime);

            }
            fullPlayerTF.position += new Vector3(0, Time.deltaTime * input.z * move_speed, 0);
        }
        
    }




    public void setRunAnim(bool state)
    {
        Animator anim = GetComponent<Animator>();
        anim.SetBool("running", state);
    }
   
    public void CamPause()
    {
        camP = !camP;
    }

    //when I touchy the launcher
    public void CollTime(GameObject launcher) 
    {
        if (!camP)
        {
            lerpTime = 0.0f;
            armRB.useGravity = false;
            personMesh.enabled = false;
            launcher.GetComponent<Launcher_Script>().containPlayer = true;
            fullPlayerTF.transform.GetComponent<PlayerFace>().enabled = false;
            tempPlayerPos = fullPlayerTF.position;
            setRunAnim(false);
            CamPause();
        }
    }

    public void Dismount()
    {
        if (camP)
        {
            Dismount_pos(tempPlayerPos);
        }
    }

    public void Dismount_pos(Vector3 respawnPos)
    {
        if (camP)
        {
            CamPause();
            cam.GetComponent<thirdCam_Script>().ResetCamDist();
            transform.position = respawnPos;
            armRB.position = respawnPos;
            fullPlayerTF.position = respawnPos;
            fullPlayerTF.transform.GetComponent<PlayerFace>().enabled = true;
            personMesh.enabled = true;
            armRB.useGravity = true; //gravity turns off but player keeps falling?
        }
    }

    public void Score()
    {
        money += moneyAmount;
        scoreTxt.text = "Score: " + money;
    }

    public void Pay(int cost)
    {
        money -= cost;
        scoreTxt.text = "Score: " + money;
    }

    public void BaseMode()
    {
        pauseMenu.pauseTime = true;
        
    }
}
