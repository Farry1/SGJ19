using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class TorpedoHit : MonoBehaviour
{

    [EventRef]
    public string torpedoSound;

    public GameObject explosionforce;
    public GameObject explosion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Saw")
        {
            FMODUnity.RuntimeManager.PlayOneShot(torpedoSound);
            Debug.Log("Deactivate Torpedo");
            Destroy(transform.root.gameObject);
            MasterControl.Instance.Screenshake(3f);
            GameObject Boom = Instantiate(explosion);
            Boom.transform.position = this.transform.position;
            
        }

        if(collision.tag == "SawHandle")
        {
            FMODUnity.RuntimeManager.PlayOneShot(torpedoSound);
            Debug.Log("Kaboom!");
            Destroy(transform.root.gameObject);
            MasterControl.Instance.Screenshake(3f);
            GameObject Boom = Instantiate(explosionforce);
            Boom.transform.position = this.transform.position;
            
        }
    }
}
