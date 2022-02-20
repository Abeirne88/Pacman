using UnityEngine;

public class PowerPellet : Pellet
{
    public float duration = 8f;
    //duration of the power

    protected override void Eat()
        //overrides the eat in pellet
    {
        FindObjectOfType<GameManager>().PowerPelletEaten(this);
    }

}