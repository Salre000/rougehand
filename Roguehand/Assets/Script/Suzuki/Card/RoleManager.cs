using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Card;

public class RoleManager : MonoBehaviour
{
    public static RoleManager instance;

    List<int> indexList = new();    // 役の条件にはまっているカードの要素数が入る
    private Role _role=Role.None;
    private bool _isCheck=false;
    private List<int> _roleLevelList=new(17);

    public enum Role
    {
        None = -1,
        revolution,
        flashFive,
        flashHouse,
        faceFive,
        fiveCard,
        faceFour,
        faceThree,
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
        max,
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        for (int i = 0; i < (int)Role.max; i++)
        {
            _roleLevelList.Add(1);
        }
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
        else if (FaceFiveCard(cards) != Role.None) return Role.faceFive;
        else if (FiveCard(cards) != Role.None) return Role.fiveCard;
        else if (FaceFourCard(cards) != Role.None) return Role.faceFour;
        else if (FaceThreeCard(cards) != Role.None) return Role.faceThree;
        else if (RoyalFlush(cards) != Role.None) return Role.royalFlush;
        else if (StraightFlash(cards) != Role.None) return Role.straightFlush;
        else if (FourCard(cards) != Role.None) return Role.fourCard;
        else if (FullHouse(cards) != Role.None) return Role.fullHouse;
        else if (Flash(cards) != Role.None) return Role.flash;
        else if (Straight(cards) != Role.None) return Role.straight;
        else if (ThreeCard(cards) != Role.None) return Role.threeCard;
        else if (TwoPair(cards) != Role.None) return Role.twoPair;
        else if (OnePair(cards) != Role.None) return Role.onePair;
        else return HighCard(cards);

    }

    #region 役

    // ※革命
    private Role Revolution(List<Card.Trump> cards)
    {
        indexList.Clear();
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].number != Card.number.two) continue;
            indexList.Add(i);
        }

        if (indexList.Count >= 5) return Role.revolution;

        return Role.None;
    }

    // ※フラッシュファイブ
    private Role FlashFive(List<Card.Trump> cards)
    {
        indexList.Clear();
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
            checkList2.Clear();
            checkList3.Clear();
            for (int j = 0; j < checkList.Count; j++)
            {
                if (checkList[i].number == checkList[j].number)
                    checkList2.Add(checkList[j]);
                else
                    checkList3.Add(checkList[j]);
            }
            if (checkList2.Count >= 2) break;
        }
        //フルハウスチェック
        if (checkList2.Count == 3)
        {
            if (JastNumberCheck(checkList2, 3) == null || JastNumberCheck(checkList3, 2) == null)
                return Role.None;
        }
        else
        {
            if (JastNumberCheck(checkList2, 2) == null || JastNumberCheck(checkList3, 3) == null)
                return Role.None;
        }


        return Role.flashHouse;
    }
    // ※フェイスファイブカード
    private Role FaceFiveCard(List<Card.Trump> cards)
    {
        // フェイスでそろっていたカードがナンバーもそろっているか確認する
        indexList = JastNumberCheck(cards);
        if (indexList == null) return Role.None;
        // 確認できたフェイスカードを確認用リストに入れる
        List<Card.Trump> checkList = new();
        for (int i = 0; i < indexList.Count; i++)
            checkList.Add(cards[indexList[i]]);
        // フェイスカードが揃っているかを確認
        indexList = FaceCheck(checkList);
        // 揃っていなければ役は不成立となる
        if (indexList == null) return Role.None;

        return Role.faceFive;
    }

    // ※ファイブカード
    private Role FiveCard(List<Card.Trump> cards)
    {
        indexList = JastNumberCheck(cards);
        if (indexList == null) return Role.None;
        return Role.faceFive;
    }
    // ※フェイスフォーカード
    private Role FaceFourCard(List<Card.Trump> cards)
    {
        indexList = JastNumberCheck(cards, 4);
        if (indexList == null) return Role.None;
        List<Card.Trump> checkList = new();
        for (int i = 0; i < indexList.Count; i++)
            checkList.Add(cards[indexList[i]]);
        indexList = FaceCheck(checkList, 4);
        if (indexList == null) return Role.None;
        return Role.faceFour;
    }
    // ※フェイススリーカード
    private Role FaceThreeCard(List<Card.Trump> cards)
    {
        // スリーカードチェック
        indexList = JastNumberCheck(cards, 3);
        if (indexList == null) return Role.None;
        // スリーカード抜き出し
        List<Card.Trump> checkList = new();
        for (int i = 0; i < indexList.Count; i++)
            checkList.Add(cards[indexList[i]]);
        // フェイスチェック
        indexList = FaceCheck(checkList, 3);
        if (indexList == null) return Role.None;
        return Role.faceThree;
    }
    // ロイヤルフラッシュ
    private Role RoyalFlush(List<Card.Trump> cards)
    {
        List<Card.Trump> jastList = new();

        // スートが揃っているかチェック
        indexList = JastSuitCheck(cards);
        // 揃っていなければ役は不成立となる
        if (indexList == null) return Role.None;

        // 揃っているカードをjastListに引き抜く
        for (int j = 0; j < indexList.Count; j++)
        {
            // 同スートカード情報がjastListの中に入る
            jastList.Add(cards[indexList[j]]);
        }

        //// 中に入れたカード情報はスートが揃っていることが分かっているので
        //// 数字の照らし合わせを行い、見事並べばロイヤルフラッシュが認められる
        if (StraightCheck(jastList, 5, false, true) != null) return Role.royalFlush;

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
            checkList2.Clear();
            checkList3.Clear();
            for (int j = 0; j < cards.Count; j++)
            {
                if (cards[i].number == cards[j].number)
                    checkList2.Add(cards[j]);
                else
                    checkList3.Add(cards[j]);
            }
            if (checkList2.Count >= 2) break;
        }
        if (checkList2.Count == 3)
        {
            if (JastNumberCheck(checkList2, 3) == null || JastNumberCheck(checkList3, 2) == null)
                return Role.None;
        }
        else
        {
            if (JastNumberCheck(checkList2, 2) == null || JastNumberCheck(checkList3, 3) == null)
                return Role.None;
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
        {
            checkList2.Clear();
            checkList3.Clear();
            for (int j = 0; j < cards.Count; j++)
            {
                if (cards[i].number == cards[j].number)
                    checkList2.Add(cards[j]);
                else
                    checkList3.Add(cards[j]);
            }
            if (checkList2.Count >= 2) break;
        }
        if (JastNumberCheck(checkList2, 2) == null || JastNumberCheck(checkList3, 2) == null)
            return Role.None;


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
                if (i == j) continue;
                if (cards[i].number != cards[j].number) continue;
                indexList.Add(i);
                indexList.Add(j);
            }
            if (indexList.Count == 2) return Role.onePair;
        }

        return Role.None;
    }

    // ハイカード
    private Role HighCard(List<Card.Trump> cards)
    {
        int num = -1;
        for (int i = 0; i < cards.Count; i++)
        {
            if (num < (int)cards[i].number)
            {
                num = (int)cards[i].number;
                indexList.Clear();
                indexList.Add(i);
            }
        }

        return Role.highCard;
    }

    #endregion

    /// <summary>
    /// スートが揃っているか判定します。
    /// </summary>
    /// <param name="cards">チェックしたいカードリスト</param>
    /// <param name="jastSuitCount">何枚揃っていれば良いか デフォルト:5</param>
    /// <param name="suit">指定したいスート デフォルト:None</param>
    /// <returns>どこの要素数に揃っているスートがあるかをLsit　intで返します。揃ってなかったらnullを返します。</returns>
    private List<int> JastSuitCheck(List<Card.Trump> cards, int jastSuitCount = 5, Card.suit suit = Card.suit.None)
    {
        // 受け取ったcardsの何番に条件を満たすものがあるかがintListされる
        List<int> jastNum = new List<int>();
        for (int i = 0; i < (int)Card.suit.max; i++)
        {
            // 指定スートがある時は、指定されたスートのみを検索する
            if (suit != Card.suit.None)
                if (suit != (Card.suit)i) continue;

            jastNum.Clear();

            // 同じスートを探す
            for (int j = 0; j < cards.Count; j++)
            {
                if (cards[j].suit != (Card.suit)i) continue;
                jastNum.Add(j);
            }

            // 欲しい数揃っているならここで返す
            if (jastNum.Count >= jastSuitCount) return jastNum;


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
        indexList = new();
        int jastNumber = 1;
        // ナンバー指定がある場合そのナンバーのみを探す
        if (number != Card.number.None)
            jastNumber = (int)number;

        for (int i = jastNumber; i < (int)Card.number.max; i++)
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
    /// <param name="isRoyal">A~10の連続した値かどうかだけ調べたいならtrueを引数に加える デフォルト:false</param>
    /// <returns>どこの要素に連続した値があるかを返します。欲しい数揃っていなければnullを返します。</returns>
    private List<int> StraightCheck(List<Card.Trump> cards, int straightCount = 5, bool oneSkipFlag = false, bool isRoyal = false)
    {
        // 受け取ったcardsの何番に条件を満たすものがあるかがintListされる
        List<int> jastNum = new List<int>();

        bool ace = false;
        bool king = false;
        bool queen = false;
        bool jack = false;
        bool ten = false;

        // A~10のストレートの場合だけ先に判定を行う
        for (int i = 0; i < cards.Count; i++)
        {
            if (ace != true && Card.number.ace == cards[i].number)
            {
                ace = true;
                jastNum.Add(i);
            }
            else if (king != true && Card.number.king == cards[i].number)
            {
                king = true;
                jastNum.Add(i);
            }
            else if (queen != true && Card.number.queen == cards[i].number)
            {
                queen = true;
                jastNum.Add(i);
            }
            else if (jack != true && Card.number.jack == cards[i].number)
            {
                jack = true;
                jastNum.Add(i);
            }
            else if (ten != true && Card.number.ten == cards[i].number)
            {
                ten = true;
                jastNum.Add(i);
            }
            if (jastNum.Count >= 5) return jastNum;
        }

        // この役を判定しているのが A~10のストレートの場合だけみたいなら以降の処理はしない
        if (isRoyal) return null;

        // Number順(13～1)に並べなおす
        List<Card.Trump> jastCards = new(cards);
        cards = CardManager.instance.NumberSort(cards);
        jastNum.Clear();
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards.Count - 1 == i) break;

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
            // 自分と一個上の数字が一緒の時
            else if (cards[i].number == cards[i + 1].number)
                continue;
            // 連続も一緒にもなっていないとき
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

    // 現在の役をローカル変数にセットします
    public void SetRole(Role role) { _role = role; }
    // 現在判定されている役を返します
    public Role GetRole() {return _role; }
    // 要素のどこに役になるカードがあるかを渡します
    public List<int> GetIndex() { return indexList; }
    // 役を判定したかをローカル変数にセットします
    public void SetIsCheck(bool isCheck) {  _isCheck = isCheck; }
    // 役が判定済みかを返します
    public bool IsCheck() {  return _isCheck; }
    // 引数に対応した役のレベルを返します
    public int GetRoleLevel(Role role) { return _roleLevelList[(int)role]; }

}
