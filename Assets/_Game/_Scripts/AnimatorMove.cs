using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorMove : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnAnimatorMove()
    {
        if (anim ==  null)
        // Make Enemy Move Along With It's Animator Component
        transform.parent.position += anim.deltaPosition;
    }
}
