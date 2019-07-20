using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLeft : MonoBehaviour
{
    public float maxLeft;
    public float minSpeed;
    public float maxSpeed;


    float speed = 0;

    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponentInChildren<SawableObject>().gameObject.GetComponent<Rigidbody2D>();
        speed = CalculateSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = new Vector3(speed, 0, 0);
        if(transform.position.x < -100f)
        {
            Destroy(transform.root.gameObject);
        }
    }

    private void FixedUpdate()
    {
        
    }

    private float CalculateSpeed()
    {
        return -(Random.Range(minSpeed, maxSpeed));
    }
}
