﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawControl : MonoBehaviour
{
    public Rigidbody2D rb;

    public bool IsRight;

    public float speed;
    public float rotationSpeed = 1f;
    public float maxSpeed;

    public Vector2 input;

    public string hAxisName;
    public string vAxisName;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Saw.Instance.isInAir)
        {
            float inputH = Input.GetAxis(hAxisName);
            // do the same for the value of "Vertical" axis
            float inputV = Input.GetAxis(vAxisName);

            input = new Vector2(inputH, inputV);

            Vector3 direction = new Vector3(input.x, input.y, 0f);
            rb.AddForce(direction * speed, ForceMode2D.Impulse);
            Vector3 rVelocity = rb.velocity;
            if (rVelocity.magnitude > maxSpeed)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }
        }
        
       



    }


}
