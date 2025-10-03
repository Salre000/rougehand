using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using static Card;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    TrumpCard trumpCard;
    /// <summary>
    /// 作られたデッキはマネージャーが持つ
    /// </summary>
    public List<Card.Trump> deck = new List<Card.Trump>();
    public List<Card.Trump> hand = new List<Card.Trump>();

    // 現在のハンドの大きさ
    private int handSize = 8;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        trumpCard = new TrumpCard();
        // デッキを生成、および代入
        //trumpCard.Test();

    }
    private void Start()
    {
        trumpCard.CreateDeck();

    }

    /// <summary>
    /// 受け取ったカードリストを数字は降順、次にスートを昇順で並べなおします
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public List<Card.Trump> NumberSort(List<Card.Trump> cards)
    {
        // 並び変えたものをvalueに代入
        List<Card.Trump> value = cards.OrderByDescending(x => x.number).ThenBy(x => x.suit).ToList();
        // コピーリストの作成
        // ※foreachでvalueの中身をいじるため、エラー回避
        List<Card.Trump> _value = new(value);

        int topIndex = 0;
        // aceを先頭に移動させる
        foreach (Card.Trump card in _value)
        {
            // aceが見つかる度に通る
            if (card.number != Card.number.ace) continue;
            Card.Trump aceCard = card;
            // 見つかったaceを一度抜いて
            value.Remove(card);
            // 先頭に入れなおす
            value.Insert(topIndex, aceCard);

            topIndex++;
        }

        return value;
    }

    /// <summary>
    /// 受け取ったカードリストをスートは昇順、次にスートを降順で並べなおします
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public List<Card.Trump> SuitSort(List<Card.Trump> cards)
    {
        // 並び変えたものをvalueに代入
        List<Card.Trump> value = cards.OrderByDescending(x => x.number).ThenBy(x => x.suit).ToList();
        // 一度aceを前に持ってくる
        value=NumberSort(value);
        // コピーリストの作成
        // ※foreachでvalueの中身をいじるため、エラー回避
        List<Card.Trump> _value = new(value);

        int topIndex = 0;
        // ♠ → ♥ → ♦ → ♣
        for(int i=0; i < 4; i++)
        {
            foreach (Card.Trump card in _value)
            {
                if (card.suit != (Card.suit)i) continue;
                Card.Trump aceCard = card;
                // 見つかった(Card.suit)iを一度抜いて
                value.Remove(card);
                // 先頭に入れなおす
                value.Insert(topIndex, aceCard);

                topIndex++;
            }
        }
        return value;
    }



    public void SetDeck(List<Card.Trump> _deck) { this.deck = _deck; }
    public List<Card.Trump> GetDeck() { return deck; }

    public void SetHandSize(int _hand) {  this.handSize = _hand; }
    public int GetHandSize() { return handSize; }

    public void SetHand(List<Card.Trump> _hand) { hand = _hand; }
    public List<Card.Trump> GetHand() { return hand;}
}
