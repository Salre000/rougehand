using UnityEngine;
public class ThousandMinutesOne: JokerBase{
    public override JokerActionUseEnum.JokerRarity GetRarity() { return JokerActionUseEnum.JokerRarity.Rare;}
public override float Trun(){return 16;}
public override void RoundStart(){
if((Random.Range(0,10000)%1000)<1)
{
JokerUtility.Remove(this);
}}

    public override string GetName()
    {
        return "ƒI[ƒ‰ƒ~ƒ‹";
    }
}