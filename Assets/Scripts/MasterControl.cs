using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterControl : MonoBehaviour
{
    public Camera mainCam;
    public List<BackScroll> Backgrounds;
    public List<GameObject> EnemiesSpawned;
    public List<GameObject> EnemiesDeck;
    public Transform bounds;

    public float sawLevel;
    public float levelLevel;

    public float CamSizeStart;
    public float CamSizeMid;
    public float CamSizeEnd;
    public float BoundSizeStart;
    public float BoundSizeMid;
    public float BoundSizeEnd;
    public Vector3 CamPosStart;
    public Vector3 CamPosMid;
    public Vector3 CamPosEnd;
    public float TransSpeedMid;
    public float TransSpeedEnd;
    public float tempLerpMid;
    public float tempLerpEnd;

    public ParticleSystem Bubbles;
    public Vector2 bubblesSize;

    public enum GameState {start,mid,end}
    public GameState gs;

    // Start is called before the first frame update
    void Start()
    {
        mainCam.orthographicSize = CamSizeStart;
        mainCam.transform.position = CamPosStart;
        bounds.localScale = new Vector3(BoundSizeStart, BoundSizeStart);
    }

    // Update is called once per frame
    void Update()
    {
        //sawLevel += 0.2f;
        //levelLevel += 0.1f;

        if (sawLevel > 40)
        {
            gs = GameState.mid;
        }
        if (levelLevel > 40)
        {
            gs = GameState.end;
        }

        StateCheck();
    }

    public void StateCheck()
    {
        switch (gs)
        {
            case GameState.start:
                StartUpdate();
                break;
            case GameState.mid:
                MidUpdate();
                break;
            case GameState.end:
                EndUpdate();
                break;
            default:
                Debug.Log("How did we end up here?");
                break;
        }
    }

    public void StartUpdate()
    {
        Debug.Log("Start Update");
    }
    public void MidUpdate()
    {
        tempLerpMid += TransSpeedMid;

        Debug.Log("Mid Update");
        float tempSize = Mathf.Lerp(CamSizeStart, CamSizeMid, tempLerpMid);
        Vector3 tempPos = Vector3.Lerp( CamPosStart, CamPosMid, tempLerpMid); 
        float tempScale = Mathf.Lerp( BoundSizeStart, BoundSizeMid, tempLerpMid);
        mainCam.orthographicSize = tempSize;
        mainCam.transform.position = tempPos;
        bounds.localScale = new Vector3(tempScale, tempScale);

    }
    public void EndUpdate()
    {
        tempLerpEnd += TransSpeedEnd;
        
        if (Bubbles != null)
        {
            var mainBubbles = Bubbles.main;
            mainBubbles.startLifetimeMultiplier = bubblesSize.x;
        }
        Debug.Log("End Update");
        float tempSize = Mathf.Lerp(CamSizeMid, CamSizeEnd, tempLerpEnd);
        Vector3 tempPos = Vector3.Lerp(CamPosMid, CamPosEnd, tempLerpEnd);
        float tempScale = Mathf.Lerp(BoundSizeMid, BoundSizeEnd, tempLerpEnd);
        mainCam.orthographicSize = tempSize;
        mainCam.transform.position = tempPos;
        bounds.localScale = new Vector3(tempScale, tempScale);
    }
}
