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
    public List<Card.Trump> pick=new List<Card.Trump>();

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
    /// <param name="cards">並べなおしたいカードリスト</param>
    /// <returns>整頓済みカードリスト</returns>
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
    /// <param name="cards">並べなおしたいカードリスト</param>
    /// <returns>整頓済みカードリスト</returns>
    public List<Card.Trump> SuitSort(List<Card.Trump> cards)
    {
        // 並び変えたものをvalueに代入
        List<Card.Trump> value = new();
        // 一度Number順でソートし、aceを前に持ってくる
        value = NumberSort(cards);
        // コピーリストの作成
        // ※foreachでvalueの中身をいじるため、エラー回避
        List<Card.Trump> copy = new(value);

        int topIndex = 0;
        // ♠ → ♥ → ♦ → ♣
        for (int i = 0; i < 4; i++)
        {
            foreach (Card.Trump card in copy)
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
    /// <summary>
    /// 手札のIDのカードを選択状態と切り替える
    /// </summary>
    /// <param name="ID"></param>
    public void SetIsSelect(int ID)
    {

        List<Card.Trump> hand = this.hand;

        Card.Trump dummyHand = hand[ID];
        dummyHand.isSelect = !dummyHand.isSelect;
        hand[ID] = dummyHand;

        this.hand = hand;

        CardObjectUtility.StopCardObject(ID);
        CardObjectUtility.ChengeStandby(ID);

    }

    /// <summary>
    /// 一枚に含まれるカードの情報の全てを比べて全く一緒のカードか判定します。
    /// </summary>
    /// <param name="card1">比べたいカードその1</param>
    /// <param name="card2">比べたいカードその2</param>
    /// <returns>全て合致していればtrue</returns>
    public bool JastCardCheck(Card.Trump card1, Card.Trump card2)
    {
        if (card1.suit != card2.suit)           return false;
        if (card1.number != card2.number)       return false;
        if (card1.sealBuff != card2.sealBuff)   return false;
        if (card1.cardBuff != card2.cardBuff)   return false;
        if (card1.deckBuff != card2.deckBuff)   return false;
        if (card1.state != card2.state)         return false;
        if (card1.isFeice != card2.isFeice)     return false;
        if (card1.isSelect != card2.isSelect)   return false;

        return true;
    }


    public void SetDeck(List<Card.Trump> _deck) { this.deck = _deck; }
    public List<Card.Trump> GetDeck() { return deck; }

    public void SetHandSize(int _hand) { this.handSize = _hand; }
    public int GetHandSize() { return handSize; }

    public void SetHand(List<Card.Trump> _hand) { hand = _hand; }
    public List<Card.Trump> GetHand() { return hand; }

    public void SetPick(List<Card.Trump> _pick) { pick = _pick; }
    public List<Card.Trump > GetPick() { return pick; }

}
