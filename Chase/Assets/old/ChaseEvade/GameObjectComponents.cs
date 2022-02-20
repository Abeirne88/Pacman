using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectComponents : MonoBehaviour
{
    public GameObject target;
    public float speed = 1.0f;

	// Update is called once per frame
    void Update()
    { 
        Attributes targetAttributes = target.GetComponent<Attributes>();
        int targetHealth = targetAttributes.health;

        if (targetHealth < 50)
        {
            float speedDelta = speed * Time.deltaTime;

            if (Mathf.Abs(transform.position.x - target.transform.position.x) > speedDelta)
            {
                if (transform.position.x > target.transform.position.x)
                {
                    float deltax = -speedDelta;
                    transform.Translate(new Vector3(deltax, 0, 0));
                }
                else if (transform.position.x < target.transform.position.x)
                {
                    float deltax = speedDelta;
                    transform.Translate(new Vector3(deltax, 0, 0));
                }
            }

            if (Mathf.Abs(transform.position.y - target.transform.position.y) > speedDelta)
            {
                if (transform.position.y > target.transform.position.y)
                {
                    float deltay = -speedDelta;
                    transform.Translate(new Vector3(0, deltay, 0));
                }
                else if (transform.position.y < target.transform.position.y)
                {
                    float deltay = speedDelta;
                    transform.Translate(new Vector3(0, deltay, 0));
                }
            }
        }
    }

}






