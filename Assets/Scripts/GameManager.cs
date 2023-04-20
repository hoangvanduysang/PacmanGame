using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    
    public Ghost[] ghosts;

    public Pacman pacman;

    public Transform  pellets;

    public int ghostMultiplier { get; private set; } = 1;
    
    public int score {get; private set; }
    public int lives {get; private set; }
    public BackgroundMusic soundManager { get; private set; }
    
    public Text scoreText;
    public Text livesText; 
    

    private void Awake()
    {
        soundManager = GetComponentInChildren<BackgroundMusic>();
    }
    
    private void Start()
    {
        NewGame();
    }
    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown) {
            NewGame();
        }
    }
    private void NewRound()
    {
        

        foreach (Transform pellet in pellets) {
        pellet.gameObject.SetActive(true);
        }
        ResetState();
    }
    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
        soundManager.PlayMusic();
    }
    private void ResetState()
    {
        for (int i = 0; i < this.ghosts.Length; i++) {
            this.ghosts[i].gameObject.SetActive(true);
        }
        this.pacman.gameObject.SetActive(true);
    }
    private void GameOver()
    {
        

        for (int i = 0; i < ghosts.Length; i++) {
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);
        
    }


    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = "x" + lives.ToString();
        
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(2, '0');
        
    }
    
    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);

        SetLives(this.lives - 1);

        if (this.lives > 0) {
            Invoke(nameof(ResetState), 3f);
        } else {
            GameOver();
        }
    }
    public void GhostEaten(Ghost ghost)
    {
        
        int points = ghost.points * this.ghostMultiplier;

        SetScore(score + points);

        this.ghostMultiplier++;
       
    }
    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);

        SetScore(this.score + pellet.points);

        if (!HasRemainingPellets())
        {
            pacman.gameObject.SetActive(true);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].frightened.Enable(pellet.duration);
        }
        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }
    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf) {
                return true;
            }
        }

        return false;
    }
    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }





}