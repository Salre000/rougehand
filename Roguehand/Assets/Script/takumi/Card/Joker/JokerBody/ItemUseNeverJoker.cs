using UnityEngine;

[System.Serializable]

public class ItemUseNeverJoker : JokerBase
{
    public override JokerActionUseEnum.JokerRarity GetRarity() { return JokerActionUseEnum.JokerRarity.Common; }
    float _magnification = 0;
    public override void UpData()
    {
        if (JokerUtility.GetTarget() != JokerActionUseEnum.JokerActionTarget.item) return;
        JokerObjectUtility.CardAddAction(JokerUtility.GetIndex(), 2);
        _magnification += 2;
    }
    public override float Trun()
    {
        return _magnification;
    }
    public override string GetExplanation2()
    {
        return Trun() < 1 ? string.Empty : MasterData.instance.GetStringMaster(1999) + _magnification.ToString().GetRedString();
    }

}