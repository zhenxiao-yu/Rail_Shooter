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
        if (anim == null || !anim.enabled || !agent.enabled)
        {
            return;
        }


        if (agent.remainingDistance > 0.01f)
        {
            movementLocal = Vector3.Lerp(movementLocal, transform.InverseTransformDirection(agent.velocity).normalized, 2f * Time.deltaTime);

            //Make NashMesh follow Animator so enemy doesn't go through walls
            agent.nextPosition = transform.position;
        } 
        else
        {
            //Fix enemy animation abrupt stop problem
            movementLocal = Vector3.Lerp(movementLocal, Vector3.zero, 2f * Time.deltaTime);
        }

        anim.SetFloat("X Speed", movementLocal.x);
        anim.SetFloat("Z Speed", movementLocal.z);
        
    }

    public void Hit(RaycastHit hit, int damage = 1)
    {   
        if (isDead)
            return;
            
        currentHealth -= damage; //decrease health by damage value
        Debug.Log("Enemy Shot!");

        //After health reaches 0
        if (currentHealth <= 0)
        {
            isDead = true;
            agent.enabled = false;
            shootOutPoint.EnemyKilled();
            //change state of animatior 
            anim.SetTrigger("Dead");
            anim.SetBool("Is Dead", true);
            Destroy(gameObject, 3f); //give time to play death animation
        }
        else
        {
            anim.SetTrigger("Shot");
        }
    }
}
