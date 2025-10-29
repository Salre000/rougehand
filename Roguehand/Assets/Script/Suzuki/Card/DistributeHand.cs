using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using static Card;

/// <summary>
/// 手札を配る
/// </summary>
public class DistributeHand : MonoBehaviour
{
    // 手札リスト
    private List<Card.Trump> deck = new List<Card.Trump>();
    private List<Card.Trump> hand = new List<Card.Trump>();
    private bool test = false;
    // Start is called before the first frame update
    void Start()
    {
        deck = CardManager.instance.GetDeck();
        hand.Capacity = CardManager.instance.GetHandSize();

        // ラウンドの終了時のドローの処理
        RoundObserver.Instance.AddRoundEndAction(() => test = false);

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (test) return;

        int addHandSize = CardManager.instance.GetHandSize() - hand.GetCount(hand=>hand.state==Card.State.hand);
        Distribute(addHandSize);
        test = true;
    }

    // ランダムで配ります
    private void Distribute(int drawCount)
    {

        List<Card.Trump> dommyHand=new List<Card.Trump>();
        hand= CardManager.instance.GetHand();
        deck = CardManager.instance.GetDeck();
        hand.Capacity = CardManager.instance.GetHandSize();
        int index = deck.Count;
        // デッキ分のキャパを獲得
        List<int> dammyDeckArray = new List<int>(index);
        for (int i = 0; i < index; i++)
            dammyDeckArray.Add(i);

        // ハンド分繰り返す
        for (int i = 0; i < drawCount; i++)
        {
            // 一回繰り返すごとにランダムで出た数値を取り除いて手札に渡す
            index = Random.Range(0, dammyDeckArray.Count);

            // 過去に使われたカードを引かないようにする
            if (deck[dammyDeckArray[index]].state == Card.State.trash) { i--;continue; }

            // デッキのダミーデッキの場所にある情報を手札追加
            Card.Trump trump = deck[dammyDeckArray[index]];
            trump.state = State.hand;
            deck[dammyDeckArray[index]] = trump;

            hand.Add(deck[dammyDeckArray[index]]);
            dommyHand.Add(deck[dammyDeckArray[index]]);
            // 一度出た場所の数値は出ないようにする
            dammyDeckArray.RemoveAt(index);

        }

        // ソート
        hand = CardManager.instance.NumberSort(hand);
        dommyHand = CardManager.instance.NumberSort(dommyHand);
        //hand =CardManager.instance.SuitSort(hand);

        //Test();

        CardManager.instance.SetHand(hand);
        CardObjectUtility.HandToCard(dommyHand);
        CardObjectUtility.StartHandMove();

    }

    /// <summary>
    /// 手札のカードを固定する関数
    /// </summary>
    private void Test()
    {
        List<Card.Trump> hand = new List<Card.Trump>();

        for (int i = 0; i < CardManager.instance.GetHandSize(); i++)
        {
            Card.Trump trump = new Trump();

           
            trump.state = State.hand;
            if (i % 3 == 0)
                trump.suit = Card.suit.Spade;
            else
                trump.suit = Card.suit.club;
            //if (i%2==0)
            trump.number = (Card.number)14-(i+1);
            //else trump.number = Card.number.ace;
            trump.isFeice = true;
            hand.Add(trump);


        }

        Card.Trump dommy = new Trump();


        dommy.state = State.hand;
        dommy.suit = Card.suit.club;
        //if (i%2==0)
        dommy.number = (Card.number)( 1);
        //else trump.number = Card.number.ace;
        dommy.isFeice = true;
        hand.Add(dommy);
        hand = CardManager.instance.NumberSort(hand);


        CardObjectUtility.HandToCard(hand);
        CardManager.instance.SetHand(hand);
        CardObjectUtility.StartHandMove();



    }
}
