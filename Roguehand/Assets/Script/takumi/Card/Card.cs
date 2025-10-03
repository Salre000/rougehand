public class Card
{

    public enum suit
    {
        None = -1,
        Spade,
        heart,
        diamond,
        club,
        max
    }

    public enum number
    {
        None = -1,
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
        ace=1,
    }
    /// <summary>
    /// カードの状態を示す
    /// </summary>
    public enum State
    {
        None=-1,
        deck,
        hand,
        trash,
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
    public enum trumpMaterial
    {
        None = -1,
        main,
        beck,
        buff,
    }

    /// <summary>
    /// 一枚のカードの情報
    /// </summary>
    public struct Trump
    {
        public suit suit;
        public number number;
        public sealBuff sealBuff;
        public cardBuff cardBuff;
        public deckBuff deckBuff;
        public State state;
        public bool isFeice;
        public bool isSelect;

        public Trump(suit suit,number number, State state, bool isFeice = false, sealBuff sealBuff=sealBuff.None,
            cardBuff cardBuff=cardBuff.None,deckBuff deckBuff=deckBuff.None, bool isSelect=false)
        {
            this.suit = suit;
            this.number = number;
            this.state = state;
            this.sealBuff = sealBuff;
            this.cardBuff = cardBuff;
            this.deckBuff = deckBuff;
            this.isFeice=isFeice;
            this.isSelect=isSelect;
            return;
        }

        public State SetState(State state)
        {
            return state;
        }
    }


}
