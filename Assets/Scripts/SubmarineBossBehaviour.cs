using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineBossBehaviour : MonoBehaviour
{
    public enum SubmarineState { Emerging, Fighting, Dead}
    public SubmarineState submarineState;

    public float firingInterval;
    float firingTimer;

    Animator animator;
    public string[] animationNames;

    int animationIndex = 0;

    string currentAnimationName = "";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        
        

    }

    // Update is called once per frame
    void Update()
    {
        switch (submarineState){
            case SubmarineState.Emerging:

              
                break;

            case SubmarineState.Fighting:
                

                if(firingTimer > firingInterval)
                {
                    firingTimer = 0;
                    //If Submarine Goes Up And Down
                    if (animationIndex == 0)
                    {
                        Debug.LogError("Firring left");
                    }
                    //If Submarine Strives Left and Right
                    else
                    {
                        Debug.LogError("Firring Up");
                    }
                } else
                {
                    firingTimer += Time.deltaTime;
                }


                break;

            case SubmarineState.Dead:
                animator.StopPlayback();
                break;

        }


    }

    public void CalculateNextAnimation()
    {
        submarineState = SubmarineState.Fighting;
        Debug.LogError("Calculating Next");
        animationIndex = Random.Range(0, 2);
        currentAnimationName = animationNames[animationIndex];
        animator.Play(currentAnimationName);
    }
}
