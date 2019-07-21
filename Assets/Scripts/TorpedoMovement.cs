using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class TorpedoMovement : MonoBehaviour
{
    public Rigidbody2D rigidbody;

    [EventRef]
    public string bubblyMovementsSound;

    public float minSpeed;
    public float maxSpeed;
    float actualSpeed;


    void Start()
    {
        actualSpeed = CalculateSpeed();
        FMODUnity.RuntimeManager.PlayOneShot(bubblyMovementsSound);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = transform.forward;

        rigidbody.AddForce(transform.right * actualSpeed);

   
        

    }

    private float CalculateSpeed()
    {
        return -(Random.Range(minSpeed, maxSpeed));
    }
}
