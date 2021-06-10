using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObjects : MonoBehaviour, IHitable
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
    }
    public void Hit(RaycastHit hit, int damage = 1)
    {
        rb.AddForce(-hit.normal * 100f);
    }
}
