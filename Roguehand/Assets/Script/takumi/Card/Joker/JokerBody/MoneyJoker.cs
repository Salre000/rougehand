using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class MoneyJoker : JokerBase
{
    public override void Initializ()
    {

        baseScoreFlag = true;

    }
    public override JokerActionUseEnum.JokerRarity GetRarity() { return JokerActionUseEnum.JokerRarity.Rare; }
    public override float Trun() 
    {
        return GameUtility.GetMyMoney();
        
    }
    public override string GetExplanation2()
    {
        return Trun() < 1 ? string.Empty : MasterData.instance.GetStringMaster(1999) + Trun().ToString().GetBlueString();
    }
}
