using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SawableCredit : SawableObject
{

   

    public override void Explode()
    {
        CreditsControl.Instance.sawProgress++;
    }


}
