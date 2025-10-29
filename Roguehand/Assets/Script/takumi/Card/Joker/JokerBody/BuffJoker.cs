using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffJoker : JokerBase
{


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
        List<Card.Trump> deck = CardManager.instance.GetDeck();





        for (int i = 0; i < trumps.Count; i++)
        {
            if (trumps[i].sealBuff != Card.sealBuff.None) continue;

            Card.Trump card = trumps[i];

            int index = deck.IndexOf(card);
            if (index < 0) 
            {
                //選択中だと一致に引っかからない為
                card.isSelect = !card.isSelect;

                index = deck.IndexOf(card);
            }



            card.sealBuff = (Card.sealBuff)Random.Range(0, (int)Card.sealBuff.MAX);
            card.deckBuff = (Card.deckBuff)Random.Range(0, (int)Card.deckBuff.MAX);
            card.cardBuff = (Card.cardBuff)Random.Range(0, (int)Card.cardBuff.MAX);

            CardManager.instance.Chenge(deck[index], card);



            CardObjectUtility.SetChengeCard(i, card);



        }
    }

}
