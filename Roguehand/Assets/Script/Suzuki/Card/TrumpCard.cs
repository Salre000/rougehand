
public class TrumpCard 
{


    void Test()
    {
        Card.Trump[] trump = new Card.Trump[52];
        // 1~13
        for(int i = 0; i < trump.Length; i++)
        {
            trump[i].Initialize(Card.suit.Spade,Card.number.ace);
        }
    }
}
