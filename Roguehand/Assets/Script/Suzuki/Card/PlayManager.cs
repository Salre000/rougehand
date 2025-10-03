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
        int jastIndex = 0;
        List<int> jastNum = new();
        List<Card.Trump> jastList = new();
        List<bool> jastNumberBool = new();
        bool ace = false;
        bool king = false;
        bool queen = false;
        bool jack = false;
        bool ten = false;
        for (int i = 0; i < 4; i++)
        {
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
            // 同じスートが五枚そろっていることを確認
            // 四枚以下なら別のスートで探すために一番上に戻る
            if (jastNum.Count <= 4) continue;

            // そろっている五つのスートが1~10になっているか
            for (int j = 0; j < jastNum.Count; j++)
            {
                // 同スートカード情報がjastListの中に入る
                jastList.Add(cards[jastNum[j]]);
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

        }
        return Role.None;
    }

    // ストレートフラッシュ
    private Role StraightFlash(List<Card.Trump> cards)
    {
        
            return Role.None;
    }



    /// <summary>
    /// 引数2数分のスートが揃っているか確認する
    /// </summary>
    /// <param name="cards">チェックしたいカードリスト</param>
    /// <param name="jastSuitCount">何枚揃っていれば良いか</param>
    /// <returns>どこの要素数に揃っているスートがあるかをLsit　intで返します。</returns>
    private List<int> JastSuitCheck(List<Card.Trump> cards, int jastSuitCount)
    {
        int jastIndex = 0;
        List<int> jastNum = new List<int>();
        for (int i = 0; i < 4; i++)
        {
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


        }
        return jastNum;
    }

    // 役の強さ順
    // ※隠し役

        // ※革命                       同じスート２の数字のカードを５枚プレイする
        //                                ↑ ラウンド中役の倍率を強いのと弱いのを入れ替える
        // 　ロイヤルフラッシュ         同じスートの１～１０をプレイする
        // 　ストレートフラッシュ       同じくスートの連番の５枚をプレイする
        // ※フェイスファイブカード     同じくフェイスカードを５枚プレイする
        // ※ファイブカード             同じ数字のカードを５枚プレイする
        // ※フェイスフォーカード       同じくフェイスカードを４枚プレイする
        //　 フォーカード               同じ数字のカードを４枚プレイする
        // ※フラッシュフルハウス       フラッシュとフルハウスの条件を同時に揃えてプレイする
        //　 フルハウス                 同じ数字を２枚と３枚でプレイする
        //　 フラッシュ                 同じスートを５枚でプレイする
        //　 ストレート                 連続した数字５枚でプレイする
        // ※フェイススリーカード       同じくフェイスカードを３枚プレイする
        //　 スリーカード               同じ数字のカードを３枚でプレイする
        //　 ツーペア                   同じ数字のカードを２枚とずつプレイする
        //　 ワンペア                   同じ数字のカードを２枚でプレイする
        //　 ハイカード                 以上の役が一つも成立しないとき



    public void SetIsPlay(bool isPlay) { _isPlay = isPlay; }
    public bool IsPlay() { return _isPlay; }
}
