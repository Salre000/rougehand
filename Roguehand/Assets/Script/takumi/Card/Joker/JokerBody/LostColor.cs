using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LostColor : JokerBase
{
    private static PostEffect postEffect;
    private static Material sepia;

    public override JokerActionUseEnum.JokerRarity GetRarity()
    {
        return JokerActionUseEnum.JokerRarity.Uncommon;
    }

    public override void Initializ()
    {

        if (postEffect == null) postEffect = Camera.main.AddComponent<PostEffect>();

        if (sepia == null) sepia = Resources.Load<Material>("takumi/SepiaMterial");

        if (postEffect.sepia == null) postEffect.sepia = sepia;


    }
    public override void RoundStart()
    {
    }

    public override float Trun()
    {
        return 10f;
    }


    public override void SaleAction()
    {
        bool flag = false;

        JokerUtility.JokerALLAction(joker => { if (joker.GetID() == GetID()&&joker!=this) flag = true; });

        if(!flag)PostEffect.Destroy(postEffect);

    }
    public override string GetExplanation2()
    {
        return Trun() < 1 ? string.Empty : MasterData.instance.GetStringMaster(1999) + Trun().ToString().GetRedString();
    }


}
