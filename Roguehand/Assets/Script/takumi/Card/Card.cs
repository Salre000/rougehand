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
        None = -1,//バフ無し
        Red,   // 再発動
        Bule,　//ラウンドの終了時に手札にあると
        Purple,//ディスカードした時に発動する
        Green, //プレイした時に発動する　手札からランダムに破壊
        Orange,//プレイした時に発動する　ジョーカーの売値を＋２
        Black,//文字化け　永続　倍率×　段階×２（段階５）
    }
    /// <summary>
    /// あらゆるカードにバフが可能なtype
    /// </summary>
    public enum cardBuff
    {
        None = -1,
        Foil,//基本スコアに追加５０
        Hologram,//倍率に追加１０
        Polychrome,//倍率に×1.5
        MouseJammer,// マウスの干渉　マウスが一定の距離を移動するとマウスの座標がずれる　　永続　倍率　５＋全ての段階を足した値　（段階５）　

    }
    /// <summary>
    /// デッキのカードにバフが可能なtype
    /// </summary>
    public enum deckBuff
    {
        None = -1,
        Bonus,//基本スコアに追加２０
        Magnification,//倍率に追加２０
        Wild,//スートを無視
        Glass,//確率破壊
        Steel,//手札にあると倍率に×1.5
        Gold,//手札にあるとお金＋３
        Lucky,//確立でお金か倍率追加
        Random,//　手札に来た時にスートとナンバーが確定する　手札に来た時に　残り２つのばふがランダムにつく
        BlindScore,//獲得から破壊までの間　画面左の数字が妨害で見にくい ジョーカーの前に発動 倍率×段階 (段階５)

    }
    /// <summary>
    /// ジョーカーのみにバフが可能なtype
    /// </summary>
    public enum JokerBuff
    {
        None = -1,
        Negative,//ジョーカーの枠を追加１
        Sepia,//基本スコアと倍率を入れ替える　セピアー＞コウイカー＞イカサマー＞入れ替え
        ObjectMove　//オブジェクトの位置が変わる　　倍率×段階　（段階５）
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
