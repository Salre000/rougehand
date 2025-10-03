using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (test) return;

        int addHandSize = CardManager.instance.GetHandSize() - hand.Count;
        Distribute(addHandSize);
        test = true;
    }

    // ランダムで配ります
    private void Distribute(int drawCount)
    {
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
            // デッキのダミーデッキの場所にある情報を手札追加
            Card.Trump trump = deck[dammyDeckArray[index]];
            trump.state = State.hand;
            deck[dammyDeckArray[index]] = trump;

            hand.Add(deck[dammyDeckArray[index]]);
            // 一度出た場所の数値は出ないようにする
            dammyDeckArray.RemoveAt(index);

        }

        // ソート
        //hand=CardManager.instance.NumberSort(hand);
        hand=CardManager.instance.SuitSort(hand);

        CardManager.instance.SetHand(hand);
        CardObjectUtility.HandToCard(hand);
        CardObjectUtility.StartHandMove();

    }
}
