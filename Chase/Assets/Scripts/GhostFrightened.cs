using UnityEngine;

public class GhostFrightened : GhostBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;
    //to change the ghosts when the power pellet is eaten

    public bool eaten { get; private set; }
    //ghost has been eaten

    public override void Enable(float duration)
        //override enable because it needs to reset if 2 or more pellets are eaten
    {
        base.Enable(duration);

        this.body.enabled = false;
        this.eyes.enabled = false;
        this.blue.enabled = true;
        this.white.enabled = false;
        //turns off the original bdy and turns on the white flashing one to show its changing

        Invoke(nameof(Flash), duration / 2f);
        //happends to show its going back
    }

    public override void Disable()
    {
        base.Disable();

        this.body.enabled = true;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;
        //reverts back when its not frightened
    }

    private void Eaten()
    {
        this.eaten = true;
        //its eaten
        this.ghost.SetPosition(this.ghost.home.inside.position);
        //teleports to the home
        this.ghost.home.Enable(duration);
        //calls to it

        this.body.enabled = false;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;
        //turns off and on
    }

    private void Flash()
    {
        if (!eaten)
        {
            this.blue.enabled = false;
            this.white.enabled = true;
            this.white.GetComponent<AnimatedSprite>().Restart();
            //restarts the animation
        }
    }

    private void OnEnable()
    {
        blue.GetComponent<AnimatedSprite>().Restart();
        this.ghost.movement.speedMultiplier = 0.5f;
        //slows the ghost
        this.eaten = false;
        //becomes frightened it can be eaten again
    }

    private void OnDisable()
    {
        this.ghost.movement.speedMultiplier = 1f;
        //oposite
        this.eaten = false;
        //oposite
    }

    private void OnTriggerEnter2D(Collider2D other)
        //running away from target pacman
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            // Find the available direction that moves farthest from pacman
            foreach (Vector2 availableDirection in node.availableDirections)
            {
                // If the distance in this direction is greater than the current
                // max distance then this direction becomes the new farthest
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (ghost.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }

            ghost.movement.SetDirection(direction);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            //colliding it gets eaten, calling to eaten
            if (enabled)
            {
                Eaten();
            }
        }
    }

}