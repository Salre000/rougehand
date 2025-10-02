using System.Collections;
using System.Collections.Generic;
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
        if(test)return;
        Distribute();
        test = true;
    }

    // ランダムで配ります
    private void Distribute()
    {
        deck = CardManager.instance.GetDeck();
        hand.Capacity = CardManager.instance.GetHandSize();
        int index = deck.Count;
        // デッキ分のキャパを獲得
        List<int> dammyDeckArray = new List<int>(index);
        for (int i = 0; i < index; i++)
            dammyDeckArray.Add(i);
        // 現在のハンド分のキャパを獲得
        int count = CardManager.instance.GetHandSize();
        // ハンド分繰り返す
        for (int i = 0; i < count; i++)
        {
            // 一回繰り返すごとにランダムで出た数値を取り除いて手札に渡す
            index = Random.Range(0, dammyDeckArray.Count);
            // デッキのダミーデッキの場所にある情報を手札追加
            Card.Trump trump = deck[dammyDeckArray[index]];
            trump.state = State.hand;
            deck[dammyDeckArray[index]] = trump;

            hand.Add(deck[dammyDeckArray[index]]);
            // 一度出た場所の数値は出ないようにする
            dammyDeckArray.Remove(index);

        }

        hand.Sort((x,y)=>y.number-x.number);
        for(int i=0;i < hand.Count; i++)
            Debug.Log("スート : " + hand[i].suit + "ナンバー : " + hand[i].number);

        CardObjectUtility.HandToCard(hand);
        CardObjectUtility.StartHandMove();

    }
}
