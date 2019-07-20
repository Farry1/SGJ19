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

    public Vector3 levelScale;
    public Vector3 tempLevelScale;
    public Vector3 targetLevelScale;
    public float levelScaleDelta;

    public ParticleSystem Bubbles;
    public Vector2 bubblesSize;

    public enum GameState {start,mid,end}
    public GameState gs;
    public Transform waterLevel;
    public Vector3 waterLevelBasePos = Vector3.zero;

    public float tickFish;
    public float tickMaxFish;
    public float tickTorpedo;
    public float tickMaxTorpedo;

    public List<GameObject> enemies;
    public List<GameObject> enemyType;

    private static MasterControl _instance;

    public static MasterControl Instance { get { return _instance; } }


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
        mainCam.orthographicSize = CamSizeStart;
        mainCam.transform.position = CamPosStart;
        bounds.localScale = new Vector3(BoundSizeStart, BoundSizeStart);
    }

    // Update is called once per frame
    void Update()
    {
        tickFish += 0.1f;
        if (tickFish >= tickMaxFish)
        {
            tickFish = 0;
            GameObject e = Instantiate(enemyType[0]);
            e.transform.position = new Vector3(100, Random.Range(-15, 10), 0);
            if (gs == MasterControl.GameState.end)
            {
                e.transform.localScale = tempLevelScale;
                e.GetComponentInChildren<SawableObject>().SetHealth(e.GetComponentInChildren<SawableObject>().maxHealth / sawLevel);
            }
            enemies.Add(e);
        }
        tickTorpedo += 0.1f;
        if (tickTorpedo >= tickMaxTorpedo)
        {
            tickTorpedo = 0;
            GameObject e = Instantiate(enemyType[1]);
            e.transform.position = new Vector3(100, Random.Range(-10, 10), 0);
            if (gs == MasterControl.GameState.end)
            {
                e.transform.localScale = tempLevelScale;
                //e.GetComponentInChildren<SawableObject>().SetHealth(e.GetComponentInChildren<SawableObject>().maxHealth / sawLevel);
            }
            enemies.Add(e);
        }

        //sawLevel += 0.001f;
        //sawLevel += 0.2f;
        //levelLevel += 0.1f;

        if (sawLevel >= 1)
        {
            gs = GameState.end;
        }
        if(sawLevel > 10)
        {
            sawLevel = 10;
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
        targetLevelScale = new Vector3(Mathf.Clamp(levelScale.x / (sawLevel/2),0.3f,1), Mathf.Clamp(levelScale.y / (sawLevel/2), 0.3f, 1), 1);
        if (Bubbles != null)
        {
            var mainBubbles = Bubbles.main;
            mainBubbles.startSizeMultiplier = bubblesSize.x/sawLevel;
        }
        Debug.Log("End Update");
        float tempSize = Mathf.Lerp(CamSizeMid, CamSizeEnd, tempLerpEnd);
        Vector3 tempPos = Vector3.Lerp(CamPosMid, CamPosEnd, tempLerpEnd);
        float tempScale = Mathf.Lerp(BoundSizeMid, BoundSizeEnd, tempLerpEnd);
        tempLevelScale = Vector3.Lerp(levelScale, targetLevelScale, -Time.deltaTime* levelScaleDelta);
        foreach (BackScroll back in Backgrounds)
        {
            back.transform.localScale = tempLevelScale;
        }

        waterLevel.localPosition = new Vector3(waterLevelBasePos.x, waterLevelBasePos.y-sawLevel, waterLevelBasePos.x);
        mainCam.orthographicSize = tempSize;
        mainCam.transform.position = tempPos;
        bounds.localScale = new Vector3(tempScale, tempScale);
    }
}
