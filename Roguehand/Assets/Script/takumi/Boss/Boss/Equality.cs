using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Equality : BossBase
{
    List<ScoreMaster.score> BaseScores = new List<ScoreMaster.score>();

    public override void Initializ()
    {
        base.Initializ();

        ScoreMaster.score equalityScore=new ScoreMaster.score();

        for (int i = 0; i < (int)RoleManager.Role.max; i++) 
        {
            BaseScores.Add(ScoreMaster.instance.GetScore(i + IDUtility.SCORE_ID));
        }

        for (int i = 0; i < (int)RoleManager.Role.max; i++) 
        {
            equalityScore.BasicScore += BaseScores[i].BasicScore;
            equalityScore.AddBasicScore += BaseScores[i].AddBasicScore;
            equalityScore.BasicMagnification += BaseScores[i].BasicMagnification;
            equalityScore.AddBasicMagnification += BaseScores[i].AddBasicMagnification;
        }

        equalityScore.BasicScore /= (int)RoleManager.Role.max;
        equalityScore.AddBasicScore /= (int)RoleManager.Role.max;
        equalityScore.BasicMagnification /= (int)RoleManager.Role.max;
        equalityScore.AddBasicMagnification /= (int)RoleManager.Role.max;

        for (int i = 0; i < (int)RoleManager.Role.max; i++)
        {
            ScoreMaster.instance.SetScore(i + IDUtility.SCORE_ID,equalityScore);
        }


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

        for (int i = 0; i < (int)RoleManager.Role.max; i++)
        {
            ScoreMaster.instance.SetScore(i + IDUtility.SCORE_ID, BaseScores[i]);
        }

    }
}
