using UnityEngine;
public class SaleJokerNever: JokerBase{
    public override JokerActionUseEnum.JokerRarity GetRarity() { return JokerActionUseEnum.JokerRarity.Rare;}
float _magnification=0;
public override void UpData(){if(JokerUtility.GetTarget()!=JokerActionUseEnum.JokerActionTarget.sale)return;
_magnification+=1;}
public override float Trun(){return _magnification;
}

}