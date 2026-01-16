using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TutorialBoss : BossBase
{

    private int[] cardIndexs = { 0, 13,3,16,29,12,25,38};


    public override void Initializ()
    {
        base.Initializ();
        //DistributeHand.instanse.SetHandDrawFlag(true);


    }

    public override void Update()
    {
        if (DistributeHand.instanse.GetHandDrawFlag()) return;

        DistributeHand.instanse.SetHandDrawFlag(true);

        Debug.Log("到達SS");

        Distribute(cardIndexs.Length);


    }

    public override void LateUpdate()
    {
    }

    public override void End()
    {
        base.End();
    }
    private void Distribute(int drawCount)
    {

        List<Card.Trump> dommyHand = new List<Card.Trump>();
        List<Card.Trump>  hand = CardManager.instance.GetHand();
        List<Card.Trump> deck = CardManager.instance.GetDeck();
        hand.Capacity = CardManager.instance.GetHandSize();
        int index = deck.Count;
        // デッキ分のキャパを獲得
        List<int> dammyDeckArray = new List<int>(index);
        for (int i = 0; i < index; i++)
            dammyDeckArray.Add(i);

        // ハンド分繰り返す
        for (int i = 0; i < drawCount; i++)
        {


            // 
            index = cardIndexs[i];

            // まだ使われていないカードのみを対象にする
            if (deck[dammyDeckArray[index]].state != Card.State.deck)
            {
                i--;

                //デッキアウト
                if (dammyDeckArray.Count < 1)
                {
                    int deckout = 0;
                    break;
                }
                continue;
            }

            // デッキのダミーデッキの場所にある情報を手札追加
            Card.Trump trump = deck[dammyDeckArray[index]];
            trump.state = Card.State.hand;
            deck[dammyDeckArray[index]] = trump;

            hand.Add(deck[dammyDeckArray[index]]);
            dommyHand.Add(deck[dammyDeckArray[index]]);

        }

        // デッキの中に使用可能なカードが一枚もない場合
        if (dommyHand.Count <= 0)
        {
            // リザルト画面に移行する

            Application.Quit();

        }

        CardManager.instance.SetHand(hand);
        CardObjectUtility.HandToCard(dommyHand);
        CardObjectUtility.StartHandMove();
        GameUtility.SetIsDiscard(false);

        // ソート
        SortHand.instance.OnSortNumberButton();

    }





}
