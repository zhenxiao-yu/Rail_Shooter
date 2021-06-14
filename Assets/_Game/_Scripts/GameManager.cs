using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}

    [SerializeField] GameState state;
    [SerializeField] PlayerMove playerMove;
    [SerializeField] PlayerScript playerScript;
    [SerializeField] int playerHealth = 10; //default health value

    [SerializeField] UIManager uiManager = new UIManager();

    //Game Stats
    private float currentHealth;
    private int enemyHit, shotsFired, enemyKilled, totalEnemy, hostageKilled;

    private TimerObject timerObject = new TimerObject();

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
        uiManager.Init(currentHealth); //initialize health value
    }

    void OnDisable()
    {
        //unbind events
        uiManager.RemoveEvent();
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
        uiManager.UpdateHealth(currentHealth); //Change UI
        playerScript.ShakeCamera(0.5f, 0.2f, 5f);
    }

    public void StartTimer(float duration)
    {
        timerObject.StartTimer(this, duration);
    }

    public void StopTimer()
    {
        timerObject.StopTimer(this);
    }

        public void RegisterEnemy()
    {
        totalEnemy++;
    }

    public void HostageKilled(Vector3 worldPos)
    {
        hostageKilled++;
        ShowHostageKilled(worldPos, true);
        this.DelayedAction(delegate { ShowHostageKilled(worldPos, false); }, 3f);
    }

    public void EnemyKilled()
    {
        enemyKilled++;
    }

    void ShowHostageKilled(Vector3 pos, bool show)
    {
        Vector3 screenPos = playerMove.GetComponent<Camera>().WorldToScreenPoint(pos);
        uiManager.ShowHostageKilled(screenPos, show);
    }
}

public enum GameState //Enumeration for stages of Game
{
    Default,
    Start,
    Gameplay,
    LevelEnd
}