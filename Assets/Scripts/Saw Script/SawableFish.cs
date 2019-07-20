using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawableFish : SawableObject
{
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer spriteRenderer
    {
        get
        {
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();
            return _spriteRenderer;
        }
    }


    public Sprite alternativeFrontSide;
    public GameObject backSideGameObject;

  

   

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
        isBeingSawed = true;

        if (sawTimer > 1f && !dead)
        {
            sawTimer = 0;
            MoveClosestColliderVertex(direction);
            health -= 15;

            if (health < 0)
            {
                dead = true;
                MasterControl.Instance.sawLevel += 1;
                MasterControl.Instance.enemies.Remove(this.gameObject);
                Debug.Log("Dead!");

                if(moveToLeft != null)
                {
                    moveToLeft.enabled = false;
                    isBeingSawed = false;                    
                }

                if (backSideGameObject != null)
                {
                    backSideGameObject.gameObject.SetActive(true);
                    backSideGameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 5, ForceMode2D.Impulse);
                }

                if (alternativeFrontSide != null)
                {
                    spriteRenderer.sprite = alternativeFrontSide;
                }

                rigidbody.isKinematic = false;
                rigidbody.AddForce(transform.right * -5, ForceMode2D.Impulse);

                

                polygonCollider.enabled = false;
                saw.ForceFromAbove(250);
                Explode();
            }
        }
    }
}
