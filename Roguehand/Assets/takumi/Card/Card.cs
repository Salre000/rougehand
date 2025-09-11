public class Card
{
    public enum suit
    {
        None = -1,
        Spade,
        heart,
        diamond,
        club
    }

    public enum number
    {
        None=-1,
        two = 2, 
        three,
        four,
        five,
        six,
        seven,
        eight,
        nine,
        ten,
        jack,
        queen, 
        king,
        ace = 1
    }

    /// <summary>
    /// 他のバフと競合しないバフなtype
    /// </summary>
    public enum sealBuff
    {
        None = -1
    }
    /// <summary>
    /// あらゆるカードにバフが可能なtype
    /// </summary>
    public enum cardBuff
    {
        None = -1
    }
    /// <summary>
    /// デッキのカードにバフが可能なtype
    /// </summary>
    public enum deckBuff
    {
        None = -1
    }
    /// <summary>
    /// ジョーカーのみにバフが可能なtype
    /// </summary>
    public enum JokerBuff
    {
        None = -1,

    }
    /// <summary>
    /// 一枚のカードの情報
    /// </summary>
    public struct Trump
    {
        suit _suit;
        number _number;
        sealBuff _sealBuff;
        cardBuff _cardBuff;
        deckBuff _deckBuff;
    }


}
