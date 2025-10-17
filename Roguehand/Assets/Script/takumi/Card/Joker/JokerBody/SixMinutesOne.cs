using UnityEngine;
public class SixMinutesOne : JokerBase
{
    public override JokerActionUseEnum.JokerRarity GetRarity() { return JokerActionUseEnum.JokerRarity.Common; }
    public override float Trun() { return 4; }
    public override void RoundStart()
    {
        if ((Random.Range(0, 10000) % 6) < 1)
        {
            JokerUtility.Remove(this);
        }
    }
    public override string GetExplanation2()
    {
        return Trun() < 1 ? string.Empty : StringMaster.instance.GetMaster(1999) + Trun().ToString().GetRedString();
    }

    public override string GetName()
    {
        return "ƒŠƒXƒN";
    }
}