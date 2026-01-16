using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : BossBase
{

    private readonly float rate = 1.5f;
    float MaxScore = 0;
    public override void Initializ()
    {
        base.Initializ();

        int index =GameUtility.GetAllRoundCount()+ IDUtility.TARGET_SCORE_ID+1;

        MaxScore = MasterData.instance.GetIntMaster(index);
        MaxScore *= rate;

        MasterData.instance.SetStringMaster(index, ((int)MaxScore).ToString());
    }

    public override void Update()
    {



    }

    public override void LateUpdate()
    {
    }

    public override void End()
    {
        base.End();
    }


}
