
using System.Collections.Generic;

/// <summary>
/// 基本的な52枚構成のデッキを作ります
/// </summary>
public class TrumpCard 
{

    private Card.suit _suit;
    private Card.number _number;
    private List<Card.Trump> deck = new List<Card.Trump>(52);
    private bool _isFeice = false;
    private int _index = 0;

    /// <summary>
    /// デッキの作成実行
    /// </summary>
    public void CreateDeck()
    {
        
        // 4種類の
        for(int i = 0; i < (int)Card.suit.max; i++)
        {
            _suit = (Card.suit)i;
            // j =  0~12 = Spade
            // j = 13~25 = heart
            // j = 26~38 = diamond
            // j = 39~51 = club
            for (int j = 0;j< (int)Card.number.king; j++)
            {
                deck.Add(new Card.Trump());
                _number = (Card.number)j+1;
                if(j+1>=11) _isFeice=true;
                else _isFeice=false;
                    deck[_index] = new(_suit, _number, Card.State.deck, _isFeice);
                _index++;
            }
        }
        CardManager.instance.SetDeck(deck);
    }

    /// <summary>
    ///  CSV基準でデッキを作成する関数
    /// </summary>
    public void CreateDeck(List<List<int>> deckBlueprint)
    {

        // 4種類の
        for (int i = 0; i < (int)Card.suit.max; i++)
        {
            _suit = (Card.suit)i;
            // j =  0~12 = Spade
            // j = 13~25 = heart
            // j = 26~38 = diamond
            // j = 39~51 = club
            for (int j = 0; j < (int)Card.number.king;)
            {
                if (deckBlueprint[i][j] <= 0) { j++; continue; }

                deck.Add(new Card.Trump());
                _number = (Card.number)j + 1;
                if (j + 1 >= 11) _isFeice = true;
                else _isFeice = false;
                deck[_index] = new(_suit, _number, Card.State.deck, _isFeice);
                _index++;
                deckBlueprint[i][j]--;
            }
        }
        CardManager.instance.SetDeck(deck);
    }


}
