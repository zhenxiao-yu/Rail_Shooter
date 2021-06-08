using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour, IHitable
{
    [SerializeField] int maxHealth;//Enemy Health
    [SerializeField] Transform targetPos; //Target Position 

    private int currentHealth;
    private Transform player;
    private bool isDead;
    private NavMeshAgent agent;
    private ShootOutPoint shootOutPoint;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = Camera.main.transform;

        agent.updateRotation = false; //Don't look at player until initialized
    }

    public void Init(ShootOutPoint point)
    {
        currentHealth = maxHealth;
        shootOutPoint = point;

        //Give Enemy a Destination
        if (agent != null)
        {
            agent.SetDestination(targetPos.position);
        }
    }

    // Update is called once per frame
    void Update()
    {   
        //The Enemy Always Facing The Player
        if(player != null && !isDead)
        {
            Vector3 direction  = player.position - transform.position;
            direction.y = 0f;

            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    public void Hit(RaycastHit hit)
    {   
        if (isDead)
            return;
            
        currentHealth--;
        Debug.Log("Enemy Shot!");

        if (currentHealth <= 0)
        {
            isDead = true;
            agent.enabled = false;
            shootOutPoint.EnemyKilled();
            Destroy(gameObject);
        }
    }
}
