using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Saw : MonoBehaviour
{
    public bool isSawing = false;
    public Rigidbody2D sawRigidbody;
    public GameObject blood;
    ISawable currentSawableObject = null;
    public float sawThreshold=7;
    public bool isInAir = false;
    public FMODUnity.StudioEventEmitter sawSoundEmitter;

    private static Saw _instance;

    public static Saw Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSawing)
        {

            sawSoundEmitter.Play();
            float xVel = sawRigidbody.gameObject.transform.InverseTransformDirection(sawRigidbody.velocity).x;
            if (Mathf.Abs(xVel) > sawThreshold)
            {
                sawRigidbody.AddForce(-sawRigidbody.transform.up * 100,ForceMode2D.Impulse);


                if (currentSawableObject != null)
                {
                    currentSawableObject.GetSawed(-sawRigidbody.transform.up);
                    MasterControl.Instance.Screenshake(0.2f);
                    GameObject obj = Instantiate(blood);
                    obj.transform.position = currentSawableObject.GetCollisionPoint();
                    Quaternion bloodRotation = Quaternion.LookRotation
                    (currentSawableObject.GetCollisionNormal() - (Vector3)sawRigidbody.velocity, transform.TransformDirection(Vector3.down));
                    obj.transform.rotation = Quaternion.FromToRotation(Vector3.back, new Vector3(currentSawableObject.GetCollisionNormal().x * (2 * xVel), 
                        currentSawableObject.GetCollisionNormal().y, 
                        currentSawableObject.GetCollisionNormal().z));
                }
            }
            //sawRigidbod.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            sawSoundEmitter.Stop();

            if(currentSawableObject != null)
            {
                currentSawableObject.EndSaw();
                currentSawableObject = null;
                //sawRigidbod.constraints = RigidbodyConstraints2D.None;
            }

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
