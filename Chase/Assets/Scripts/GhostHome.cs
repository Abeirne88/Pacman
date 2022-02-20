using System.Collections;
using UnityEngine;

public class GhostHome : GhostBehavior
{
    public Transform inside;
    //to get in and out
    public Transform outside;

    private void OnEnable()
    {
        StopAllCoroutines();
        //stops all other coroutines
    }

    private void OnDisable()
    {
        // Check for active self to prevent error when object is destroyed
        if (gameObject.activeSelf)
        {
            StartCoroutine(ExitTransition());
            //pause execution temporarily
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reverse direction everytime the ghost hits a wall to create the
        // effect of the ghost bouncing around the home
        Debug.Log("Fuck"); //it was because home had tiles in it they collided with
        if (this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            this.ghost.movement.SetDirection(-this.ghost.movement.direction);
        }
    }

    private IEnumerator ExitTransition()
    {
        // Turn off movement while we manually animate the position
        this.ghost.movement.SetDirection(Vector2.up, true);
        //forced up to go throut the wall
        this.ghost.movement.rigidbody.isKinematic = true;
        //turns off collisions unitl outside the wall
        this.ghost.movement.enabled = false;

        Vector3 position = transform.position;
        //current pos

        float duration = 0.5f;
        //transitation from node to node
        float elapsed = 0f;
        //elasped time

        // Animate to the starting point
        while (elapsed < duration)
        {
            this.ghost.SetPosition(Vector3.Lerp(position, inside.position, elapsed / duration));
            //lerpng from a to b, going throught the wall (to move an object gradually between those points).
            elapsed += Time.deltaTime;
            yield return null;
            //waits one frame between the loops 
        }

        elapsed = 0f;

        // Animate exiting the ghost home
        while (elapsed < duration)
        {
            ghost.SetPosition(Vector3.Lerp(inside.position, outside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
            //reversed the above by going from outside to inside
        }

        // Pick a random direction left or right and re-enable movement
        this.ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1f : 1f, 0f), true);
        this.ghost.movement.rigidbody.isKinematic = false;
        this.ghost.movement.enabled = true;
        //turns them back on
    }

}