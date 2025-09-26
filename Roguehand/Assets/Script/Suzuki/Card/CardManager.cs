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
    public List<Card.Trump> deck = new List<Card.Trump>();

    [SerializeField]
    private TextMeshProUGUI _text;
    private StringBuilder _builder;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        trumpCard=new TrumpCard();
        // デッキを生成、および代入
        trumpCard.Test();

        _builder = new StringBuilder();
        Test();
        _text.text = _builder.ToString();
    }

    private int _index = 0;
    public void Test()
    {

        // 4種類の
        for (int i = 0; i < (int)Card.suit.max; i++)
        {
            // j =  0~12 = Spade
            // j = 13~25 = heart
            // j = 26~38 = diamond
            // j = 39~51 = club
            for (int j = 0; j < (int)Card.number.king; j++)
            {
                _builder.Append("suit: "+deck[_index].suit+"　"+"number: "+ deck[_index].number+"　"+"isFeice: "+deck[_index].isFeice);
                _builder.Append("\n");
                _index++;
            }
        }

        CardManager.instance.SetDeck(deck);
    }

    public void SetDeck(List<Card.Trump> deck) { this.deck = deck; }
}
