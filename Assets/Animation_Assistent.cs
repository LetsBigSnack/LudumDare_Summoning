using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Assistent : MonoBehaviour
{
    Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        animator.SetFloat("moveY", y);
        animator.SetFloat("moveX", x);
    }
}
