using UnityEngine;
public class Test2: JokerBase{
    public override JokerActionUseEnum.JokerRarity GetRarity() { return JokerActionUseEnum.JokerRarity.Common;}
float _magnification=0;
public override void UpData(){if(JokerUtility.GetTarget()!=JokerActionUseEnum.JokerActionTarget.sale)return;
_magnification+=2;}
public override float Trun(){return _magnification;
}

}