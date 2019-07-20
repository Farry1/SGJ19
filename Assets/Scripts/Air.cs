using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : MonoBehaviour
{
    public float gravityScale;
    public GameObject waterSurfaceGameObject;

    private WaterSurfaceCheck waterSurfaceCheck;

    // Start is called before the first frame update
    void Start()
    {
        waterSurfaceCheck = waterSurfaceGameObject.GetComponent<WaterSurfaceCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = gravityScale;

        if(collision.tag == "Player" && !waterSurfaceCheck.isInWater)
        {
            Saw.Instance.isInAir = true;
            Saw.Instance.sawRigidbody.gravityScale = gravityScale;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

        if (collision.tag == "Player")
        {
            Saw.Instance.isInAir = false;
            Saw.Instance.sawRigidbody.gravityScale = 0;
        }
    }
}
