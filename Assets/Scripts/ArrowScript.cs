using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float move_speed = 5.0f;

    [HideInInspector]
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        transform.LookAt(target.transform.position);
        if(target.GetComponent<Rocket_script>().kill == true)
        {
            Destroy(transform.gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.forward * Time.fixedDeltaTime * move_speed;
    }


}
