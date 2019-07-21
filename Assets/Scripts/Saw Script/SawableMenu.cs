using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SawableMenu : SawableObject
{

    public string NextScene;

    public override void Explode()
    {
        SceneManager.LoadScene(NextScene);
    }


}
