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
    private Animator anim; //Enemy Animator Ref
    private Vector3 movementLocal;


    // Start is called before the first frame update
    void Start()
    {
        //Get Navigation Mesh Component 
        agent = GetComponent<NavMeshAgent>();
        //get Player Ref
        player = Camera.main.transform;
        //Get Animator Component
        anim = GetComponentInChildren<Animator>(); 
        //Dont't change enemy pos and rot until called
        agent.updatePosition = false;
        agent.updateRotation = false; 

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

        RunBlend();
    }

    void RunBlend()
    {
        if (anim == null || !anim.enabled)
        {
            return;
        }

        /* If the enemy transform position distance with the agent 
        position is stillgreater than the threshold, we want to drive 
        the animation using the movementlocal variable*/

        if ((agent.nextPosition - transform.position).sqrMagnitude > 0.01f)
        {
            movementLocal = transform.InverseTransformDirection(agent.nextPosition - transform.position);
        }

        anim.SetFloat("X Speed", movementLocal.x);
        anim.SetFloat("Z Speed", movementLocal.z);
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
