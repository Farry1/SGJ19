using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawableShip : SawableObject
{
    public Rigidbody2D shipRigidbody;
    public override void Explode()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(deathFX);
            obj.transform.position = this.transform.position;
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
                MasterControl.Instance.sawLevel += 0.3f;
                MasterControl.Instance.enemies.Remove(this.gameObject);
                shipRigidbody.velocity = new Vector3(0, 0, 0);
                shipRigidbody.constraints = RigidbodyConstraints2D.None;
                shipRigidbody.gameObject.GetComponent<MoveToLeft>().enabled = false;

                shipRigidbody.AddForce(transform.right * -5, ForceMode2D.Impulse);
                shipRigidbody.gravityScale = 3;


                polygonCollider.enabled = false;
                saw.ForceFromAbove(250);
                Explode();
            }
        }
    }
}
