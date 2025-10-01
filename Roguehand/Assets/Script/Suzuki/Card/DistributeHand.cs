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
    // Start is called before the first frame update
    void Start()
    {
        deck = CardManager.instance.GetDeck();
        hand.Capacity = CardManager.instance.GetHand();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ランダムで配ります
    private void Distribute()
    {
        int index = deck.Count;
        // デッキ分のキャパを獲得
        List<int> deckArray = new List<int>(index);
        for (int i = 0; i < index; i++)
            deckArray.Add(i);
        // 現在のハンド分のキャパを獲得
        int count = CardManager.instance.GetHand();
        // ハンド分繰り返す
        for (int i = 0; i < count; i++)
        {
            // 一回繰り返すごとにランダムで出た数値を取り除いて手札に渡す
            index = Random.Range(0, deckArray.Count);
            deckArray.Remove(index);
            // デッキのindex番にある情報を手札も追加
            deck[index] = new(deck[index].suit, deck[index].number, Card.State.hand, deck[index].isFeice);
            hand.Add(deck[index]);
            Debug.Log(index);
        }
    }
}
