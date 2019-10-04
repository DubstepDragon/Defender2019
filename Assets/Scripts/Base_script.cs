using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_script : MonoBehaviour
{
    [HideInInspector]
    public GameObject shield;

    public float maxHealth = 100.0f;
    public float currentHealth = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void Hit(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        var myHealth = currentHealth / maxHealth;
        shield.transform.localScale = new Vector3(myHealth, shield.transform.localScale.y, shield.transform.localScale.z);
    }

}
