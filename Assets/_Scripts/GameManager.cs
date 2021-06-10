using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}

    [SerializeField] GameState state;
    [SerializeField] PlayerMove playerMove;

    private void Awake()
    {
        //initialize current instance
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SwitchState(GameState.Start);
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
}

public enum GameState //Enumeration for stages of Game
{
    Default,
    Start,
    Gameplay,
    LevelEnd
}