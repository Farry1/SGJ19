using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public bool isSawing = false;

    public Rigidbody2D sawRigidbody;


    ISawable currentSawableObject = null;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isSawing)
        {
            
            float xVel = sawRigidbody.gameObject.transform.InverseTransformDirection(sawRigidbody.velocity).x;


           

            if (Mathf.Abs(xVel) > 8)
            {
                sawRigidbody.AddForce(-sawRigidbody.transform.up * 250);


                if (currentSawableObject != null)
                    currentSawableObject.GetSawed(-sawRigidbody.transform.up);                    
            }


    


            //sawRigidbod.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            currentSawableObject = null;
            //sawRigidbod.constraints = RigidbodyConstraints2D.None;
        }
    }



    private void FixedUpdate()
    {

    }

    public  Vector3 GetPosition()
    {
        return sawRigidbody.transform.position;
    }


    public void ProceedSaw(ISawable sawableObject)
    {
        isSawing = true;
        currentSawableObject = sawableObject;
    }

    public void EndSaw()
    {
        isSawing = false;
    }

    public void ForceFromAbove(float forcePower)
    {
        sawRigidbody.AddForce(-sawRigidbody.transform.up * forcePower, ForceMode2D.Impulse);
    }

}
