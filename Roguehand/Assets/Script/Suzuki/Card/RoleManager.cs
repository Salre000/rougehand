using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Card;

public class RoleManager : MonoBehaviour
{
    public static RoleManager instance;

    private bool _isPlay = false;
    private int _roleNumber = -1;   // 中で一番強い役を数字で表す
    List<int> indexList = new();    // 役の条件にはまっているカードの要素数が入る


    public enum Role
    {
        None = -1,
        revolution,
        flashFive,
        flashHouse,
        faceFiveCard,
        fiveCard,
        faceFourCard,
        faceThreeCard,
        royalFlush,
        straightFlush,
        fourCard,
        fullHouse,
        flash,
        straight,
        threeCard,
        twoPair,
        onePair,
        highCard,
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    /// <summary>
    /// そろっている役があるか確認する
    /// </summary>
    /// <returns></returns>
    public Role RoleCheck(List<Card.Trump> cards)
    {
        // 役の強い順に判定
        if (Revolution(cards) != Role.None) return Role.revolution;
        else if (FlashFive(cards) != Role.None) return Role.flashFive;
        else if (flashHouse(cards) != Role.None) return Role.flashHouse;
        else if (FaceFiveCard(cards) != Role.None) return Role.faceFiveCard;
        else if (FiveCard(cards) != Role.None) return Role.fiveCard;
        else if (FaceFourCard(cards) != Role.None) return Role.faceFourCard;
        else if (FaceThreeCard(cards) != Role.None) return Role.faceThreeCard;
        else if (RoyalFlush(cards) != Role.None) return Role.royalFlush;
        else if (StraightFlash(cards) != Role.None) return Role.straightFlush;
        else if (FourCard(cards) != Role.None) return Role.fourCard;
        else if (FullHouse(cards) != Role.None) return Role.fullHouse;
        else if (Flash(cards) != Role.None) return Role.flash;
        else if (Straight(cards) != Role.None) return Role.straight;
        else if (ThreeCard(cards) != Role.None) return Role.threeCard;
        else if (TwoPair(cards) != Role.None) return Role.twoPair;
        else if (OnePair(cards) != Role.None) return Role.onePair;

        return Role.highCard;
    }

    // ※革命
    private Role Revolution(List<Card.Trump> cards)
    {
        for(int i = 0; i<cards.Count; i++)
        {
            if (cards[i].number!=Card.number.two) continue;
            indexList.Add(i);
        }

        if (indexList.Count >= 5) return Role.revolution;

        return Role.None;
    }

    // ※フラッシュファイブ
    private Role FlashFive(List<Card.Trump> cards)
    {
        // 同スートチェック
        indexList = JastSuitCheck(cards);
        if (indexList == null) return Role.None;
        List<Card.Trump> checkList = new();
        for (int i = 0; i < indexList.Count; i++)
            checkList.Add(cards[indexList[i]]);
        // 同ナンバーチェック
        if (JastNumberCheck(checkList, 5) == null) return Role.None;

        return Role.flashFive;
    }

    // ※フラッシュハウス
    private Role flashHouse(List<Card.Trump> cards)
    {
        // 同スートチェック
        indexList = JastSuitCheck(cards);
        if (indexList == null) return Role.None;
        List<Card.Trump> checkList = new();
        for (int i = 0; i < indexList.Count; i++)
            checkList.Add(cards[indexList[i]]);

        // フルハウスチェック
        List<Card.Trump> checkList2 = new();
        List<Card.Trump> checkList3 = new();
        for (int i = 0; i < checkList.Count; i++)
        {
            if (checkList[0].number == checkList[i].number)
                checkList2.Add(checkList[i]);
            else
                checkList3.Add(checkList[i]);
        }
        //フルハウスチェック
        if (checkList2.Count == 3)
        {
            if (JastNumberCheck(checkList2, 3) == null) return Role.None;
            if (JastNumberCheck(checkList3, 2) == null) return Role.None;
        }
        else
        {
            if (JastNumberCheck(checkList2, 2) == null) return Role.None;
            if (JastNumberCheck(checkList3, 3) == null) return Role.None;
        }

        return Role.flashHouse;
    }
    // ※フェイスファイブカード
    private Role FaceFiveCard(List<Card.Trump> cards)
    {
        // フェイスカードが揃っているかを確認
        indexList = FaceCheck(cards);
        // 揃っていなければ役は不成立となる
        if (indexList == null) return Role.None;
        // 確認できたフェイスカードを確認用リストに入れる
        List<Card.Trump> checkList = new();
        for (int i = 0; i < indexList.Count; i++)
            checkList.Add(cards[indexList[i]]);
        // フェイスでそろっていたカードがナンバーもそろっているか確認する
        indexList = JastNumberCheck(checkList);
        if (indexList == null) return Role.None;

        return Role.faceFiveCard;
    }

    // ※ファイブカード
    private Role FiveCard(List<Card.Trump> cards)
    {
        indexList = JastNumberCheck(cards);
        if (indexList == null) return Role.None;
        return Role.faceFiveCard;
    }
    // ※フェイスフォーカード
    private Role FaceFourCard(List<Card.Trump> cards)
    {
        indexList = FaceCheck(cards, 4);
        if (indexList == null) return Role.None;
        List<Card.Trump> checkList = new();
        for (int i = 0; i < indexList.Count; i++)
            checkList.Add(cards[indexList[i]]);
        indexList = JastNumberCheck(checkList, 4);
        if (indexList == null) return Role.None;
        return Role.faceFourCard;
    }
    // ※フェイススリーカード
    private Role FaceThreeCard(List<Card.Trump> cards)
    {
        List<Card.Trump> checkList = new();

        indexList = FaceCheck(cards, 3);
        if (indexList == null) return Role.None;
        for (int i = 0; i < indexList.Count; i++)
            checkList.Add(cards[indexList[i]]);
        indexList = JastNumberCheck(checkList, 3);
        if (indexList == null) return Role.None;
        return Role.faceThreeCard;
    }
    // ロイヤルフラッシュ
    private Role RoyalFlush(List<Card.Trump> cards)
    {
        List<Card.Trump> jastList = new();
        List<bool> jastNumberBool = new();
        bool ace = false;
        bool king = false;
        bool queen = false;
        bool jack = false;
        bool ten = false;

        // スートが揃っているかチェック
        indexList = JastSuitCheck(cards);
        // 揃っていなければ役は不成立となる
        if (indexList == null) return Role.None;

        // そろっている五つのスートが1~10になっているか
        for (int j = 0; j < indexList.Count; j++)
        {
            // 同スートカード情報がjastListの中に入る
            jastList.Add(cards[indexList[j]]);
        }
        // 中に入れたカード情報はスートが揃っていることが分かっているので
        // 数字の照らし合わせを行い見事五つ揃えばロイヤルフラッシュが認められる
        for (int j = 0; j < jastList.Count; j++)
        {
            if (ace != true && Card.number.ace == jastList[j].number)
            {
                ace = true;
                jastNumberBool.Add(true);
            }
            else if (king != true && Card.number.king == jastList[j].number)
            {
                king = true;
                jastNumberBool.Add(true);
            }
            else if (queen != true && Card.number.queen == jastList[j].number)
            {
                queen = true;
                jastNumberBool.Add(true);
            }
            else if (jack != true && Card.number.jack == jastList[j].number)
            {
                jack = true;
                jastNumberBool.Add(true);
            }
            else if (ten != true && Card.number.ten == jastList[j].number)
            {
                ten = true;
                jastNumberBool.Add(true);
            }
            // .Countが5になることで揃っていることが証明される
            if (jastNumberBool.Count >= 5) return Role.royalFlush;
        }
        return Role.None;
    }

    // ストレートフラッシュ
    private Role StraightFlash(List<Card.Trump> cards)
    {

        // スートが揃っているかチェック
        indexList = JastSuitCheck(cards);
        // 揃っていなければ役は不成立となる
        if (indexList == null) return Role.None;
        List<Card.Trump> checkList = new();
        for (int i = 0; i < indexList.Count; i++)
            checkList.Add(cards[indexList[i]]);
        // ストレートかをチェック
        indexList = StraightCheck(checkList);
        if (indexList == null) return Role.None;

        return Role.straightFlush;
    }

    // フォーカード
    private Role FourCard(List<Card.Trump> cards)
    {
        indexList = JastNumberCheck(cards, 4);
        if (indexList == null) return Role.None;
        return Role.fourCard;
    }

    // フルハウス
    private Role FullHouse(List<Card.Trump> cards)
    {
        // フルハウスチェック
        List<Card.Trump> checkList2 = new();
        List<Card.Trump> checkList3 = new();
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[0].number == cards[i].number)
                checkList2.Add(cards[i]);
            else
                checkList3.Add(cards[i]);
        }
        int count = checkList2.Count;
        //フルハウスチェック
        if (count == 3)
        {
            if (JastNumberCheck(checkList2, 3) == null) return Role.None;
            if (JastNumberCheck(checkList3, 2) == null) return Role.None;
        }
        else
        {
            if (JastNumberCheck(checkList2, 2) == null) return Role.None;
            if (JastNumberCheck(checkList3, 3) == null) return Role.None;
        }

        return Role.fullHouse;
    }

    // フラッシュ
    private Role Flash(List<Card.Trump> cards)
    {
        // 同スートチェック
        indexList = JastSuitCheck(cards);
        if (indexList == null) return Role.None;

        return Role.flash;
    }

    // ストレート
    private Role Straight(List<Card.Trump> cards)
    {
        indexList = StraightCheck(cards);
        if (indexList == null) return Role.None;
        return Role.straight;
    }

    // スリーカード
    private Role ThreeCard(List<Card.Trump> cards)
    {
        indexList = JastNumberCheck(cards, 3);
        if (indexList == null) return Role.None;

        return Role.threeCard;
    }

    // ツーペア
    private Role TwoPair(List<Card.Trump> cards)
    {
        List<Card.Trump> checkList2 = new();
        List<Card.Trump> checkList3 = new();
        for (int i = 0; i < cards.Count; i++)
            if (cards[0].number == cards[i].number)
                checkList2.Add(cards[i]);
            else
                checkList3.Add(cards[i]);

        if (checkList2.Count <= 1)
        {
            checkList2.Clear();
            checkList3.Clear();
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[1].number == cards[i].number)
                    checkList2.Add(cards[i]);
                else
                    checkList3.Add(cards[i]);
            }
        }
        if (JastNumberCheck(checkList2, 2) == null) return Role.None;
        if (JastNumberCheck(checkList3, 2) == null) return Role.None;

        return Role.twoPair;
    }

    // ワンペア
    private Role OnePair(List<Card.Trump> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            indexList.Clear();
            for (int j = 0; j < cards.Count; j++)
            {
                if (cards[i].number != cards[j].number) continue;
                indexList.Add(j);
            }
        }

        if (indexList.Count <= 1) return Role.None;

        return Role.onePair;
    }

    /// <summary>
    /// スートが揃っているか判定します。
    /// </summary>
    /// <param name="cards">チェックしたいカードリスト</param>
    /// <param name="jastSuitCount">何枚揃っていれば良いか デフォルト:5</param>
    /// <param name="suit">指定したいスート デフォルト:None</param>
    /// <returns>どこの要素数に揃っているスートがあるかをLsit　intで返します。揃ってなかったらnullを返します。</returns>
    private List<int> JastSuitCheck(List<Card.Trump> cards, int jastSuitCount = 5, Card.suit suit = Card.suit.None)
    {
        int jastIndex = 0;
        // 受け取ったcardsの何番に条件を満たすものがあるかがintListされる
        List<int> jastNum = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            // 指定スートがある時は、指定されたスートのみを検索する
            if (suit != Card.suit.None)
                if (suit != (Card.suit)i) continue;

            jastIndex = 0;
            jastNum.Clear();

            // 同じスートを探す
            foreach (Card.Trump card in cards)
            {
                jastIndex++;
                if (card.suit != (Card.suit)i) continue;
                // 同じスートが見つかったらindex番号を入れていく
                jastNum.Add(jastIndex);
            }
            // 欲しい数分揃っていなければなら別のスートで探すために一番上に戻る
            if (jastNum.Count <= jastSuitCount) continue;
            // 揃っていれば返す
            return jastNum;

        }
        return null;
    }

    /// <summary>
    /// ナンバーが揃っているかを判定します。
    /// </summary>
    /// <param name="cards">チェックしたいカードリスト</param>
    /// <param name="jastNumberCount">何枚揃っていれば良いか デフォルト:5</param>
    /// <param name="number">欲しいナンバーが決まっているなら選択する デフォルト:None</param>
    /// <returns>どこの要素数に揃っているスートがあるかを返します。揃っていなければnullを返します。</returns>
    private List<int> JastNumberCheck(List<Card.Trump> cards, int jastNumberCount = 5, Card.number number = Card.number.None)
    {
        int jastNumber = 0;
        // ナンバー指定がある場合そのナンバーのみを探す
        if (number != Card.number.None)
            jastNumber = (int)number;

        for (int i = jastNumber; i < (int)Card.number.king; i++)
        {
            indexList.Clear();

            // 同じナンバーを探す
            for (int j = 0; j < cards.Count; j++)
            {
                if (cards[j].number != (Card.number)i) continue;
                indexList.Add(j);
                if (indexList.Count >= jastNumberCount) return indexList;
            }

            // ナンバーを指定していて、欲しい数揃っていなければnullを返す
            if (number != Card.number.None)
                return null;
        }
        return null;

    }

    /// <summary>
    /// 数字が連続的に並んでいるか判定します。
    /// </summary>
    /// <param name="cards">チェックしたいカードリスト</param>
    /// <param name="straightCount">何枚連続していればいいか デフォルト:5</param>
    /// <param name="oneSkipFlag">ストレートの条件が一つ飛ばしでも良い状態か デフォルト:false</param>
    /// <returns>どこの要素に連続した値があるかを返します。欲しい数揃っていなければnullを返します。</returns>
    private List<int> StraightCheck(List<Card.Trump> cards, int straightCount = 5, bool oneSkipFlag = false)
    {
        // 受け取ったcardsの何番に条件を満たすものがあるかがintListされる
        List<int> jastNum = new List<int>();
        List<Card.Trump> jastCards = new(cards);

        // Number順(13～1)に並べなおす
        cards = CardManager.instance.NumberSort(cards);

        for (int i = 0; i < cards.Count; i++)
        {
            if (cards.Count == i) break;

            // 一個上が連続した数値かどうか
            if (oneSkipFlag ? cards[i].number == cards[i + 1].number + 1 || cards[i].number == cards[i + 1].number + 2 : cards[i].number == cards[i + 1].number + 1)
            {
                // 元のカードリストと同じものを見つける
                for (int j = 0; j < jastCards.Count; j++)
                {
                    if (CardManager.instance.JastCardCheck(jastCards[j], cards[i]))
                    {
                        // 要素のある値を追加
                        jastNum.Add(j);
                    }
                }
            }
            else
                jastNum.Clear();

            // 連続したカードが5枚以上見つかっているならreturn
            if (jastNum.Count >= straightCount) return jastNum;
        }

        return null;
    }

    /// <summary>
    /// フェイスカードかどうか判定します。
    /// </summary>
    /// <param name="cards">チェックしたいカードリスト</param>
    /// <param name="faceCount">何枚揃っていればいいか デフォルト:5</param>
    /// <returns>どこの要素数にフェイスカードがあるかを返します。欲しい数揃っていなければnullを返します。</returns>
    private List<int> FaceCheck(List<Card.Trump> cards, int faceCount = 5)
    {
        List<int> indexLists = new List<int>();
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].isFeice != true) continue;
            indexLists.Add(i);
            if (indexLists.Count >= faceCount) return indexLists;
        }

        return null;
    }

    // 役の強さ順
    // ※隠し役

    // ※革命                       同じスート２の数字のカードを５枚プレイする
    //                                ↑ ラウンド中役の倍率を強いのと弱いのを入れ替える
    // ※フラッシュファイブ         同じスートで同じ数字
    // ※フラッシュハウス           フラッシュとフルハウスの条件を同時に揃えてプレイする
    // ※フェイスファイブカード     同じフェイスカードを５枚プレイする
    // ※ファイブカード             同じ数字のカードを５枚プレイする
    // ※フェイスフォーカード       同じフェイスカードを４枚プレイする
    // ※フェイススリーカード       同じフェイスカードを３枚プレイする
    // 　ロイヤルフラッシュ         同じスートの１～１０をプレイする
    // 　ストレートフラッシュ       同じスートの連番の５枚をプレイする
    //　 フォーカード               同じ数字のカードを４枚プレイする
    //　 フルハウス                 同じ数字を２枚と３枚でプレイする
    //　 フラッシュ                 同じスートを５枚でプレイする
    //　 ストレート                 連続した数字５枚でプレイする
    //　 スリーカード               同じ数字のカードを３枚でプレイする
    //　 ツーペア                   同じ数字のカードを２枚とずつプレイする
    //　 ワンペア                   同じ数字のカードを２枚でプレイする
    //　 ハイカード                 以上の役が一つも成立しないとき


    public List<int> GetIndex() { return indexList; }
    public void SetIsPlay(bool isPlay) { _isPlay = isPlay; }
    public bool IsPlay() { return _isPlay; }
}
