using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    //refs to other scripts

   /*
    public Text gameOverText;
    public Text scoreText;
    public Text livesText;
   //work in progress
   */
 

    public int ghostMultiplier { get; private set; } = 1;
    //for eating ghosts
    public int score { get; private set; }
    public int lives { get; private set; }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKey)
        {
            //lives zero press any key to continue to new game 
            NewGame();
        }
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        //this.gameOverText.enabled = false;
        //game over text is not displayed

        foreach (Transform pellet in pellets)
            //new rounds re displays the pellets 
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
        //calls the following to reset more after pacman dies but not the pellets
    }

    private void ResetState()
    {
        ResetGhostMultiplier();
        //for the new round
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }

        this.pacman.ResetState();
    }


    private void GameOver()
    {
        //this.gameOverText.enabled = true;
        //gameover text is displayed

        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
            //turns off the ghosts during the game over
        }

        this.pacman.gameObject.SetActive(false);
        //same for pacman
    }


    private void SetLives(int lives)
    {
        this.lives = lives;
        //livesText.text = "x" + lives.ToString();
        //add lives for pacman !!!!!!
    }

    private void SetScore(int score)
    {
        this.score = score;
        //this.scoreText.text = score.ToString().PadLeft(2, '0');
        //add sores !!!!!!!!!
    }

    public void PacmanEaten()
    {
        //pacman.DeathSequence();
        this.pacman.gameObject.SetActive(false);

        SetLives(this.lives - 1);
        //pacman eaten take alive

        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
            //when all lives gone restart game
        }
        else
        {
            GameOver();
            //display game over
        }
    }

    public void GhostEaten(Ghost ghost)
    {
        //any ghost eaten the scoere goes up
        int points = ghost.points * this.ghostMultiplier;
        //uses the code in ghost for amount worth
        SetScore(score + points);

        this.ghostMultiplier++;
    }
   
    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);

        SetScore(score + pellet.points);

        if (!HasRemainingPellets())
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f);
            //round won, pacman disapears so it doesnt get killed
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(pellet.duration);
        }
        //ghosts frightened state for the length of the power pellet

        PelletEaten(pellet);
        CancelInvoke(nameof(ResetGhostMultiplier));
        //cancels for the new power to start
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
        //invokes when the power pellet wears off
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf)
                //checks to see if there arr pellets left 
            {
                return true;
                //yes
            }
        }

        return false;
        //no
    }

    private void ResetGhostMultiplier()
    {
        //resets after the powerpellet goes away
        ghostMultiplier = 1;
    }
}
