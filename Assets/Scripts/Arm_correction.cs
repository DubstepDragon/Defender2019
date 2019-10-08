using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm_correction : MonoBehaviour
{
    public Transform correctionTf;
    public GameObject extScript;
    public Vector3 jump;
    public float jumpForce = 2.0f;

    public bool isGrounded;

    Rigidbody myR;
    private bool camPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        myR = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);

    }

    // Update is called once per frame
    void Update()
    {
        camPaused = extScript.GetComponent<Player_Script>().camP;
        if (!camPaused)
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {

                myR.AddForce(jump * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }
        transform.rotation = correctionTf.rotation;
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Launcher")
        {
            gameObject.SendMessageUpwards("CollTime", other.gameObject);
        }
        if (other.gameObject.tag == "Base")
        {
            gameObject.SendMessageUpwards("BaseMode", other.gameObject, SendMessageOptions.DontRequireReceiver);
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Base")
        {
            gameObject.SendMessageUpwards("BaseModeExit", other.gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Coin")
        {
            gameObject.SendMessageUpwards("Score");
        }
        
    }
}
