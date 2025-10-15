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


    public override void RoundStart()
    {

       if(postEffect==null) postEffect = Camera.main.AddComponent<PostEffect>();

        if (sepia == null) sepia = Resources.Load<Material>("takumi/SepiaMterial");

        if(postEffect.sepia==null)postEffect.sepia = sepia;


    }

    public override float Trun()
    {
        return 10f;
    }


    public override void SaleAction()
    {

        PostEffect.Destroy(postEffect);

    }
    public override string GetName()
    {
        return "ÉåÉgÉç";
    }

}
