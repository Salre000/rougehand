using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffJoker : JokerBase
{
    public override string GetName()
    {
        return "バッファーン";
    }


    public override void SaleAction()
    {

        JokerUtility.JokerALLAction(joker =>
        {
            joker.SetCardBuff((Card.cardBuff)Random.Range(0, (int)Card.cardBuff.MAX));
            joker.SetJokerBuff((Card.JokerBuff)Random.Range(0, (int)Card.JokerBuff.MAX));

            JokerUtility.JokerChenge(JokerUtility.GetIndex(joker));



        });

        ChengeCard();

        //アクション状態に変更するコマンド
        JokerObjectUtility.CardAddAction(-1, -2);




    }
    private void ChengeCard() 
    {
        List<Card.Trump> trumps = CardManager.instance.GetHand();

        for(int i = 0; i < trumps.Count; i++) 
        {
            if (trumps[i].sealBuff != Card.sealBuff.None) continue;

            Card.Trump card = trumps[i];

            card.sealBuff = (Card.sealBuff)Random.Range(0, (int)Card.sealBuff.MAX);
            card.deckBuff = (Card.deckBuff)Random.Range(0, (int)Card.deckBuff.MAX);
            card.cardBuff = (Card.cardBuff)Random.Range(0, (int)Card.cardBuff.MAX);

            trumps[i] = card;

            CardObjectUtility.SetChengeCard(i, card);

        }
        // 手札に上書き
        CardManager.instance.SetHand(trumps);

    }

}
