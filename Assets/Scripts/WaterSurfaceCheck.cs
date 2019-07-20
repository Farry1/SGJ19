using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSurfaceCheck : MonoBehaviour
{
    public bool isInWater;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isInWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isInWater = false;
        }
    }
}
