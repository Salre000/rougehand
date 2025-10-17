using UnityEngine;
public class ConstellationUseNeverJoker : JokerBase
{
    public override JokerActionUseEnum.JokerRarity GetRarity() { return JokerActionUseEnum.JokerRarity.Common; }
    float _magnification = 0;
    public override void UpData()
    {
        if (JokerUtility.GetTarget() != JokerActionUseEnum.JokerActionTarget.constellation) return;
        JokerObjectUtility.CardAddAction(JokerUtility.GetIndex(), 2);
        _magnification += 2;
    }
    public override float Trun()
    {
        return _magnification;
    }
    public override string GetExplanation2()
    {
        return Trun()<1?string.Empty: StringMaster.instance.GetMaster(1999) + _magnification.ToString().GetRedString();
    }
    public override string GetName()
    {
        return "セレスティアル";
    }

}