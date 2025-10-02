using System.Collections;
using System.Collections.Generic;
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

    public void SetDeck(List<Card.Trump> _deck) { this.deck = _deck; }
    public List<Card.Trump> GetDeck() { return deck; }

    public void SetHandSize(int _hand) {  this.handSize = _hand; }
    public int GetHandSize() { return handSize; }

    public void SetHand(List<Card.Trump> _hand) { hand = _hand; }
    public List<Card.Trump> GetHand() { return hand;}
}
