using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineBossBehaviour : MonoBehaviour
{
    public enum SubmarineState { Emerging, Fighting, Dead}
    public SubmarineState submarineState;

    float firingInterval;

    Animator animator;
    public string[] animationNames;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("SubmarineEmerge"))
        {
            Debug.LogError("Animation Playing");
        } 
    }
}
