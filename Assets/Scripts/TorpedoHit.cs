using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoHit : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Saw")
        {
            Debug.Log("Deactivate Torpedo");
            Destroy(transform.root.gameObject);
            MasterControl.Instance.Screenshake(3f);
        }

        if(collision.tag == "SawHandle")
        {
            Debug.Log("Kaboom!");
            Destroy(transform.root.gameObject);
            MasterControl.Instance.Screenshake(3f);
        }
    }
}
