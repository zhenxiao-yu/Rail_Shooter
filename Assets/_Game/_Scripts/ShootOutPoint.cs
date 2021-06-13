using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootOutPoint : MonoBehaviour
{
    [SerializeField] EnemyEntry[] enemyList;
    public bool AreaCleared {get; private set;}
    private bool activePoint;
    private PlayerMove playerMove;
    private int enemyKilled, totalEnemy; //player kill count 

    public void Initialize(PlayerMove value)
    {
        playerMove = value;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerMove.SetPlayerMovement (false);
        }

        if(Input.GetKeyDown(KeyCode.Return) && activePoint)
        {
            playerMove.SetPlayerMovement (true);
            AreaCleared = true;
            activePoint = false;
        }      
    }

    public void StartShootOut(float timer)
    {
        activePoint = true;
        playerMove.SetPlayerMovement (false);
        StartCoroutine(SendEnemies());
        this.DelayedAction(SetAreaCleared, timer);
        GameManager.Instance.StartTimer(timer);
    }

    IEnumerator SendEnemies()
    {
        foreach(var enemy in enemyList)
        {
            //only count enemy, not the hostage
            totalEnemy = !(enemy.enemy is HostageScript) ? totalEnemy + 1 : totalEnemy + 0;
            yield return new WaitForSeconds(enemy.delay);
            //Get Enemy To Move
            enemy.enemy.Init(this); // pass shoot out point

            Debug.Log(enemy.enemy.gameObject.name + " Spawned");
        }
    }

    public void EnemyKilled()
    {
        enemyKilled++;

        if (enemyKilled == totalEnemy)
        {
            Debug.Log(gameObject.name + " cleared!");
            playerMove.SetPlayerMovement (true);
            AreaCleared = true;
            activePoint = false;
            //Stop timer if all enemies has been killed
            GameManager.Instance.StopTimer();
        }
    }

    public void SetAreaCleared()
    {
        if (AreaCleared)
            return;
        
        AreaCleared = true;
        playerMove.SetPlayerMovement(true);

        foreach (var enemy in enemyList)
        {
            enemy.enemy.StopShooting();
        }
    }
}

[System.Serializable]
public class EnemyEntry
{
    public EnemyScript enemy;
    public float delay;

}
