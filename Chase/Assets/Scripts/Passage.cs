using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Passage : MonoBehaviour
{
    public Transform connection;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 position = connection.position;
        //objects current position
        position.x = this.connection.position.x;
        position.y = this.connection.position.y;
        position.z = other.transform.position.z;
        other.transform.position = position;
    }

}