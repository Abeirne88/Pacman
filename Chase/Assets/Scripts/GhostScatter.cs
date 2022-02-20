using UnityEngine;

public class GhostScatter : GhostBehavior
{
    private void OnDisable()
    {
        this.ghost.chase.Enable();
        //when it stps scatter mode
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("I am");
        Node node = other.GetComponent<Node>();

        // Do nothing while the ghost is frightened
        if (node != null && this.enabled && !this.ghost.frightened.enabled)
        {
            Debug.Log("in");
            // Pick a random available direction from zero to all available
            int index = Random.Range(0, node.availableDirections.Count);

            // Prefer not to go back the same direction so cycle the index to
            // the next available direction
            if (node.availableDirections[index] == -this.ghost.movement.direction && node.availableDirections.Count > 1)
            {
                Debug.Log("Hell");
                index++;

                // Wrap the index back around to go again if overflowed
                if (index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }

            this.ghost.movement.SetDirection(node.availableDirections[index]);
            //finaly set the direction to that direction
        }
    }

}