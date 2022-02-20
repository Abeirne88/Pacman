using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
//requires the sprite render to animate the sprites
public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    //to ref in other scripts
    public Sprite[] sprites = new Sprite[0];
    //cycles through the sprites
    public float animationTime = 0.25f;
    //time between them
    public int animationFrame { get; private set; }
    //keeps track of the current sprite
    public bool loop = true;
    //loops the sprites

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //gets first
    }

    private void Start()
    {
        InvokeRepeating(nameof(Advance), this.animationTime, this.animationTime);
        //involkes advance per animation time
    }

    private void Advance()
    {
        
         if (!this.spriteRenderer.enabled)
        {
            return;
        }
        
        this.animationFrame++;

        if (this.animationFrame >= this.sprites.Length && this.loop)
        {
            this.animationFrame = 0;
        }

        if (this.animationFrame >= 0 && this.animationFrame < this.sprites.Length)
        {
            this.spriteRenderer.sprite = this.sprites[this.animationFrame];
        }
    }

    public void Restart()
    {
        this.animationFrame = -1;

        Advance();
        //instead of using zero and readding the code using -1 and calling advaance restarts it to go again
    }

}