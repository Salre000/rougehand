using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffJoker : JokerBase
{

    public override void SaleAction()
    {

        JokerUtility.JokerALLAction(joker =>
        {
            joker.SetCardBuff((Card.cardBuff)Random.Range(0, (int)Card.cardBuff.MouseJammer));
            joker.SetJokerBuff((Card.JokerBuff)Random.Range(0, (int)Card.JokerBuff.ObjectMove));

            JokerUtility.JokerChenge(JokerUtility.GetIndex(joker));

        });

        //アクション状態に変更するコマンド
        JokerObjectUtility.CardAddAction(-1, -2);




    }

}
