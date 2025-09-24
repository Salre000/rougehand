using UnityEngine;
public class Test1: JokerBase{
    public override JokerActionUseEnum.JokerRarity GetRarity() { return JokerActionUseEnum.JokerRarity.Common;}
public override float Trun(){return 16;}
public override void RoundStart(){
if((Random.Range(0,10000)%6)<1)
{
JokerUtility.Remove(this);
}}

}