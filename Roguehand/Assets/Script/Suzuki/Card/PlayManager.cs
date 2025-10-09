using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public static PlayManager instance;

    private bool _isPlay = false;
    private int _roleNumber = -1;   // 中で一番強い役を数字で表す

    public enum Role
    {
        None = -1,
        revolution,
        royalFlush,
        straightFlush,
        faceFiveCard,
        fiveCard,
        faceFourCard,
        fourCard,
        flashHouse,
        fullHouse,
        flash,
        straight,
        faceThreeCard,
        threeCard,
        twoPare,
        onePare,
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
    public int RoleCheck(List<Card.Trump> cards)
    {
        // cardsの上からそろっているものを確認していく
        // ナンバーのチェック
        for (int i = 0; i < cards.Count; i++)
        {
            foreach (Card.Trump card in cards)
            {

            }
        }

        return _roleNumber;
    }

    // 革命
    private Role Revolution(List<Card.Trump> cards)
    {
        List<Card.Trump> copy = new(cards);

        int twoIndex = 0;
        List<int> revoList = new List<int>();
        // 革命がそろっているか探します
        foreach (Card.Trump card in cards)
        {
            twoIndex++;
            if (card.number != Card.number.two) continue;
            // 2のある場所を記録
            revoList.Add(twoIndex);
        }

        if (revoList.Count < 5) return Role.None;

        return Role.revolution;
    }

    // ロイヤルフラッシュ
    private Role RoyalFlush(List<Card.Trump> cards)
    {
        List<int> jastNums = new();
        List<Card.Trump> jastList = new();
        List<bool> jastNumberBool = new();
        bool ace = false;
        bool king = false;
        bool queen = false;
        bool jack = false;
        bool ten = false;

        // スートが揃っているかチェック
        jastNums = JastSuitCheck(cards);
        // 揃っていなければ役は不成立となる
        if (jastNums == null) return Role.None;

        // そろっている五つのスートが1~10になっているか
        for (int j = 0; j < jastNums.Count; j++)
        {
            // 同スートカード情報がjastListの中に入る
            jastList.Add(cards[jastNums[j]]);
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
        List<int> jastNums = new();

        // スートが揃っているかチェック
        jastNums = JastSuitCheck(cards);
        for(int i = 0; i < jastNums.Count; i++)
        {
            
        }
        // 揃っていなければ役は不成立となる
        if (jastNums == null) return Role.None;


        return Role.None;
    }



    /// <summary>
    /// 引数2数分のスートが揃っているか確認する
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
            if (oneSkipFlag ? cards[i].number == cards[i + 1].number + 1|| cards[i].number == cards[i + 1].number + 2 : cards[i].number == cards[i + 1].number + 1)
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


    // 役の強さ順
    // ※隠し役

    // ※革命                       同じスート２の数字のカードを５枚プレイする
    //                                ↑ ラウンド中役の倍率を強いのと弱いのを入れ替える
    // 　ロイヤルフラッシュ         同じスートの１～１０をプレイする
    // 　ストレートフラッシュ       同じスートの連番の５枚をプレイする
    // ※フェイスファイブカード     同じフェイスカードを５枚プレイする
    // ※ファイブカード             同じ数字のカードを５枚プレイする
    // ※フェイスフォーカード       同じフェイスカードを４枚プレイする
    //　 フォーカード               同じ数字のカードを４枚プレイする
    // ※フラッシュフルハウス       フラッシュとフルハウスの条件を同時に揃えてプレイする
    //　 フルハウス                 同じ数字を２枚と３枚でプレイする
    //　 フラッシュ                 同じスートを５枚でプレイする
    //　 ストレート                 連続した数字５枚でプレイする
    // ※フェイススリーカード       同じフェイスカードを３枚プレイする
    //　 スリーカード               同じ数字のカードを３枚でプレイする
    //　 ツーペア                   同じ数字のカードを２枚とずつプレイする
    //　 ワンペア                   同じ数字のカードを２枚でプレイする
    //　 ハイカード                 以上の役が一つも成立しないとき



    public void SetIsPlay(bool isPlay) { _isPlay = isPlay; }
    public bool IsPlay() { return _isPlay; }
}
