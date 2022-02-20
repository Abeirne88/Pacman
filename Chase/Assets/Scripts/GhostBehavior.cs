using UnityEngine;

[RequireComponent(typeof(Ghost))]
//requires ghost
public abstract class GhostBehavior : MonoBehaviour
    //abstract because it is an inheritant and cant be added alone to an object
{
    public Ghost ghost { get; private set; }
    //refs the desired ghost 
    public float duration;

    private void Awake()
    {
        this.ghost = GetComponent<Ghost>();
        this.enabled = false;
        //ghost = GetComponent<Ghost>();
    }

    public void Enable()
    {
        Enable(this.duration);
    }
    //calls the below function for the duration of the power pellet
    public virtual void Enable(float duration)
    {
        enabled = true;

        CancelInvoke();
        //canceleds the invoke to reset the duration time
        Invoke(nameof(Disable), duration);
        //invokes the disabel after the duration (its a timer)
        //used in the frightened state of the ghosts
    }

    public virtual void Disable()
    //use virtual to override if need be
    {
        this.enabled = false;

        CancelInvoke();
    }

}