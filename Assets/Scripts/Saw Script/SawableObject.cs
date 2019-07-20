using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawableObject : MonoBehaviour, ISawable
{

    protected Saw _saw;
    protected Saw saw
    {
        get
        {
            if (_saw == null)
                _saw = GameObject.FindGameObjectWithTag("Player").GetComponent<Saw>();
            return _saw;
        }
    }

    protected float sawTimer = 0;

    protected Rigidbody2D rigidbody;

    protected PolygonCollider2D polygonCollider;
    public Vector3 center;
    public Vector2 pointOfCollision;
    public Vector3 normalOfCollider;

    public GameObject deathFX;

    protected bool dead = false;

    public float maxHealth;
    protected float health;
    protected bool isBeingSawed = false;

    protected MoveToLeft moveToLeft;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        center = polygonCollider.bounds.center;
        health = maxHealth;
        moveToLeft = GetComponentInParent<MoveToLeft>();
    }

    // Update is called once per frame
    void Update()
    {
        sawTimer += Time.deltaTime;

        if (moveToLeft != null && isBeingSawed && !dead)
        {
            Debug.Log("Stop Fish from Moving");
            rigidbody.velocity = new Vector3(0, 0, 0);
            moveToLeft.enabled = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            saw.ProceedSaw(this);
            pointOfCollision = collision.contacts[0].point;
         
        }        
        normalOfCollider = collision.contacts[0].normal;
    }


    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            saw.EndSaw();
        }
    }

    public void MoveClosestColliderVertex(Vector3 direction)
    {
        
        int ColliderVertexIndex = 3;

        float distanceToClosestVertex = Mathf.Infinity;
        int closestColliderVertexIndex = 0;
        Vector2[] points = polygonCollider.points;

        for (int i = 0; i < points.Length; i++)
        {
            Vector2 point = new Vector2(transform.position.x + points[i].x, transform.position.y + points[i].y);
       


            float distanceToVertex = Vector2.Distance(point, saw.GetPosition());

            if (distanceToVertex < distanceToClosestVertex)
            {
                distanceToClosestVertex = distanceToVertex;
                ColliderVertexIndex = i;
            }
        }

        points[ColliderVertexIndex].x = Mathf.Lerp(center.x - transform.position.x, points[ColliderVertexIndex].x, health / maxHealth);
        points[ColliderVertexIndex].y = Mathf.Lerp(center.y - transform.position.y, points[ColliderVertexIndex].y, health / maxHealth);


        polygonCollider.SetPath(0, points);
    }

    public virtual void GetSawed(Vector3 direction)
    {
        if (sawTimer > 1f && !dead)
        {
            sawTimer = 0;
            MoveClosestColliderVertex(direction);
            health -= 15;

            if(health < 0)
            {
                dead = true;
                Debug.Log("Dead!");
                polygonCollider.enabled = false;
                saw.ForceFromAbove(250);
                Explode();
            }
        }
    }

    public virtual void Explode()
    {
       
    }

    public void SetHealth(float hp)
    {
        maxHealth = hp;
    }

    public void EndSaw()
    {
        isBeingSawed = false;
        
        if(moveToLeft != null && !dead)
        {
            moveToLeft.enabled = true;
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public Vector3 GetRotation()
    {
        return transform.localRotation.eulerAngles;
    }
    public Vector3 GetCollisionPoint()
    {
        return pointOfCollision;
    }
    public Vector3 GetCollisionNormal()
    {
        return normalOfCollider;
    }
}
