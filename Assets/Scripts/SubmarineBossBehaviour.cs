using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineBossBehaviour : MonoBehaviour
{
    public enum SubmarineState { Emerging, Fighting, Dead, AfterDead }
    public SubmarineState submarineState;

    public GameObject missilePrefab;

    public GameObject topLauncher;
    public GameObject bottomLauncher;

    public GameObject explosion;

    SawableSubmarine sawableSubmarine;

    public float firingInterval;
    float firingTimer;

    Animator animator;
    public string[] animationNames;

    int animationIndex = 0;

    string currentAnimationName = "";

    bool exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        sawableSubmarine = GetComponentInChildren<SawableSubmarine>();


    }

    // Update is called once per frame
    void Update()
    {
        if (sawableSubmarine.dead)
        {
            submarineState = SubmarineState.Dead;
        }


        switch (submarineState)
        {
            case SubmarineState.Emerging:


                break;

            case SubmarineState.Fighting:


                if (firingTimer > firingInterval)
                {
                    firingTimer = 0;
                    //If Submarine Goes Up And Down
                    if (animationIndex == 0)
                    {
                        GameObject missile = Instantiate(missilePrefab, bottomLauncher.transform.position, bottomLauncher.transform.rotation);
                        missile.transform.parent = null;
                    }
                    //If Submarine Strives Left and Right
                    else
                    {
                        GameObject missile = Instantiate(missilePrefab, topLauncher.transform.position, topLauncher.transform.rotation);
                        missile.transform.parent = null;
                    }
                }
                else
                {
                    firingTimer += Time.deltaTime;
                }
                break;

            case SubmarineState.Dead:
                if (!exploded)
                {
                    exploded = true;
                    Debug.LogError("Submarine Killed");
                    StartCoroutine(PlayExplosions());
                    animator.enabled = false;
                    submarineState = SubmarineState.AfterDead;
                }

                break;

        }
    }


    IEnumerator PlayExplosions()
    {

        Vector3 spawnPosition = transform.position;
        yield return new WaitForSeconds(0.25f);

        for (int i = 0; i < 10; i++)
        {
            GameObject explosionParticles = Instantiate(explosion, transform.position - new Vector3(Random.Range(-4f, 4f), Random.Range(-4f, 4f), 0), transform.rotation);
            yield return new WaitForSeconds(Random.Range(0.35f, 0.45f));
        }

        Destroy(transform.root.gameObject);
    }

    public void CalculateNextAnimation()
    {
        submarineState = SubmarineState.Fighting;
        animationIndex = Random.Range(0, 2);
        currentAnimationName = animationNames[animationIndex];
        animator.Play(currentAnimationName);
    }
}
