using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawableObject : MonoBehaviour, ISawable
{

    Saw _saw;
    Saw saw
    {
        get
        {
            if (_saw == null)
                _saw = GameObject.FindGameObjectWithTag("Player").GetComponent<Saw>();
            return _saw;
        }
    }

    float sawTimer = 0;

    

    PolygonCollider2D polygonCollider;
    public Vector3 center;
    public Vector2 pointOfCollision;
    public Vector3 normalOfCollider;

    public GameObject deathFX;

    bool dead = false;

    public float maxHealth;
    public float health;


    // Start is called before the first frame update
    void Start()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        center = polygonCollider.bounds.center;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        sawTimer += Time.deltaTime;
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

    public void GetSawed(Vector3 direction)
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

    public void Explode()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(deathFX);
            obj.transform.position = center;
            obj.transform.rotation = Quaternion.FromToRotation(Vector3.back, new Vector3(Random.Range(0, 360), Random.Range(0, 360), 0f));
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
