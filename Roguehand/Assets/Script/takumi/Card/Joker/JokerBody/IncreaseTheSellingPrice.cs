using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseTheSellingPrice : JokerBase
{

    public override void RoundEnd()
    {
        //ラウンドの終了時にジョーカーの金額を２増やす
        JokerUtility.JokerALLAction(jokerBase =>
        {

            jokerBase.AddSaleValue(2);

        });

    }

    public override string GetName()
    {
        return "マーチャント";
    }

}
