using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SawableSubmarine : SawableObject
{
    public Rigidbody2D submarineRigidbody;

    public override void Explode()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(deathFX);
            obj.transform.position = center;
            obj.transform.rotation = Quaternion.FromToRotation(Vector3.back, new Vector3(Random.Range(0, 360), Random.Range(0, 360), 0f));
        }
    }

    
    public override void GetSawed(Vector3 direction)
    {
        if (sawTimer > 1f && !dead)
        {
            sawTimer = 0;
            MoveClosestColliderVertex(direction);
            health -= 15;

            if (health < 0)
            {
                dead = true;
                Debug.Log("Dead!");
                StartCoroutine("SelfDestruct");
                submarineRigidbody.isKinematic = false;
                submarineRigidbody.gravityScale = 0.29f;
                submarineRigidbody.AddForce(transform.right * -2, ForceMode2D.Impulse);

                polygonCollider.enabled = false;
                saw.ForceFromAbove(250);
                Explode();
            }
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(3);
        Saw.Instance.sawSoundEmitter.Stop();
        SceneManager.LoadScene("Final");
        Destroy(this.gameObject);
    }
}
