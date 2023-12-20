using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Animate : MonoBehaviour
{
    Animator animator;
    public float horizontal;

    
    public ParticleSystem dust;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {   

        //Debug.Log(horizontal.ToString());
        if(horizontal == 0)
        {
            animator.SetBool("Freez", true);
        } else
        {
            dust.Play();
            animator.SetBool("Freez", false);
            animator.SetFloat("H", horizontal);
        }
    }

}
