using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    [SerializeField] float damageRadius; //effective Range
    [SerializeField] float delayUntilDestroy; 

    //Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delayUntilDestroy);
        DamageNearbyObjects();
    }

    //Damage near by objects 
    void DamageNearbyObjects()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, damageRadius);
        
        foreach (var col in cols)
        {
            IHitable[] hitables = col.GetComponents<IHitable>();

            RaycastHit hit;

            if (Physics.Raycast(transform.position, col.transform.position - transform.position, out hit))
            //Check calidity of hitable objects and excute hit
            if(hitables != null && hitables.Length > 0)
            {
                foreach (var hitable in hitables)
                {
                    hitable.Hit(hit, 50); //Do 50 Damage, Should Kill Most Objects Instantly
                }
            }
        }
    }

    private void OnDrawGizmosSelected() //Show Range On Click
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
