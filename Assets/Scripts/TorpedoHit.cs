using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoHit : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if(collision.gameObject.tag == "Saw")
        {
            Debug.Log("Deactivate Torpedo");
            Destroy(transform.root.gameObject);
        }

        if(collision.tag == "Player")
        {
            Debug.Log("Kaboom!");
            Destroy(transform.root.gameObject);
        }


      
    }
}
