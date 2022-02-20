using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start!!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Trigger!!");

        GameObject trigger = collider.gameObject;

        // Typically you will check for a Tag or Object name
        // as you may have many different triggers in your level.

        Destroy(trigger);

        health += 100;
    }
}
