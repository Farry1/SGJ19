using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawableAudioTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sawable")
        {
            Debug.LogError("Collision With Sawable");

            if (!Saw.Instance.sawAudioIsPlaying)
            {
                Saw.Instance.sawSoundEmitter.Play();
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Sawable")
        {
            Saw.Instance.sawAudioIsPlaying = false;
            Saw.Instance.sawSoundEmitter.Stop();
        }
    }
}
