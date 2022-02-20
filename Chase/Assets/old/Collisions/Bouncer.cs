using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Handle collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colliding....");

        Rigidbody2D toBeBounced = collision.rigidbody;

        Vector2 norm = collision.contacts[0].normal;
        norm.Normalize();
        toBeBounced.AddForce(-norm * 1000.0f);
    }
}
