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


    private void Awake()
    {
        if (instance == null)
            instance = this;
        trumpCard=new TrumpCard();
        // デッキを生成、および代入
        //trumpCard.Test();

    }
    private void Start()
    {
        trumpCard.Test();

    }

    public void SetDeck(List<Card.Trump> deck) { this.deck = deck; }
}
