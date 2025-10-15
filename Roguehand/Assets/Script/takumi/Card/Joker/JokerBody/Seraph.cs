using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seraph : JokerBase
{


    public override void SaleAction() 
    {

        JokerUtility.AddMagnification(100f);


        //ハンドの残り回数をゼロにする


    }



    public override JokerActionUseEnum.JokerRarity GetRarity() { return JokerActionUseEnum.JokerRarity.Rare;}

    public override string GetName()
    {
        return "レイディアンス";
    }
}
