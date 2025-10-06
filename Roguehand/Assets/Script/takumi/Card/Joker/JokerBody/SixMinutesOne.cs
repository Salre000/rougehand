using UnityEngine;
public class SixMinutesOne: JokerBase{
    public override JokerActionUseEnum.JokerRarity GetRarity() { return JokerActionUseEnum.JokerRarity.Common;}
public override float Trun(){return 4;}
public override void RoundStart(){
if((Random.Range(0,10000)%6)<1)
{
JokerUtility.Remove(this);
}}

}