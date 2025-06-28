using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleanimation : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator animator;
    void Start()
    {
        animator = this.GetComponent<Animator>();
        Invoke("delay", Random.Range(0.1f,9.9f));
    }

    void delay()
    {
        animator.SetBool("delay", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
