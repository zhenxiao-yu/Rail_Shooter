using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}

    [SerializeField] GameState state;
    [SerializeField] PlayerMove playerMove;
    [SerializeField] int playerHealth = 10;


    private float currentHealth;
    private int enemyHit, shotsFired;

    private void Awake()
    {
        //initialize current instance
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SwitchState(GameState.Start);
        Init();
    }

    void Init()
    {
        currentHealth = playerHealth;
    }

    public void SwitchState(GameState newState)
    {
        if(state == newState)
            return;
        
        state = newState;
        switch(state)
        {
            case GameState.Start:
                Debug.Log("Game Start");
                playerMove.enabled = false;
                this.DelayedAction(delegate { SwitchState(GameState.Gameplay); }, 3f); //switch to Gameplay State After 3 seconds
                break;
            case GameState.Gameplay:
                Debug.Log("State: Gameplay " + Time.time);
                playerMove.enabled = true;
                break;
            case GameState.LevelEnd:
                break;
        }
    }

    public void ShotHit(bool hit)
    {
        //used To Calculate The Accuracy of Shot
        if(hit)
            enemyHit++;
        shotsFired++;
    }

    public void PlayerHit(float damage)
    {
        //take damage when the player is hit
        currentHealth -= damage;
    }
}

public enum GameState //Enumeration for stages of Game
{
    Default,
    Start,
    Gameplay,
    LevelEnd
}