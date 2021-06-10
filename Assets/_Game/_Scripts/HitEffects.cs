using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffects : MonoBehaviour, IHitable
{
    [SerializeField] GameObject effectsPrefabs;

    private ParticleSystem effectsCache;

    public void Hit(RaycastHit hit, int damage = 1)
    {
        if (effectsCache != null)
        {
            effectsCache.transform.position = hit.point;
            effectsCache.transform.rotation = Quaternion.LookRotation(hit.normal);
            effectsCache.Play();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (effectsPrefabs != null)
        {
            GameObject effectsTemp = Instantiate(effectsPrefabs, transform);
            effectsCache = effectsTemp.GetComponent<ParticleSystem>();


        }
    }

}
