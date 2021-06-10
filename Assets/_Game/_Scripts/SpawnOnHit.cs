using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnHit : MonoBehaviour, IHitable
{
    [SerializeField] GameObject prefabsToSpawn;
    [SerializeField] float yOffset = 0.5f;
    [SerializeField] bool destroyOnHit;

    public void Hit(RaycastHit hit, int damage =1)
    {
        if (prefabsToSpawn != null)
        {
            Instantiate(prefabsToSpawn, transform.position + Vector3.up * yOffset, Quaternion.identity);
        }

        if (destroyOnHit)
            Destroy(gameObject);
    }
}
