using UnityEngine;
public class ItemUseNeverUp : JokerBase
{
    public override JokerActionUseEnum.JokerRarity GetRarity() { return JokerActionUseEnum.JokerRarity.Uncommon; }
    float _magnification = 0;
    public override void UpData()
    {
        if (JokerUtility.GetTarget() != JokerActionUseEnum.JokerActionTarget.item) return;

        JokerObjectUtility.CardAddPlay(JokerUtility.GetIndex(), 2);
        _magnification += 2;
    }
    public override float Trun()
    {
        return _magnification;
    }

}