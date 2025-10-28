using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
/// <summary>
/// 拡張クラス
/// </summary>
public static class Extra
{

    /// <summary>
    /// 拡張関数
    /// </summary>
    /// <typeparam name="T"><リストの一要素/typeparam>
    /// <param name="list"><リストの本体このリスト自体に変更を入れるわけではない/param>
    /// <param name="lostID"><移動元の要素番号/param>
    /// <param name="nextID"><移動先の要素番号/param>
    public static List<T> ChengeOrder<T>(List<T> list, int lostID, int nextID)
    {
        List<T> dummyList = new List<T>();
        //ネクストの一個前まで追加
        for (int i = 0; i < nextID; i++) { dummyList.Add(list[i]); }

        int startPos = 0;

        if (nextID < lostID)
        {
            dummyList.Add(list[lostID]);
            dummyList.Add(list[lostID - 1]);
            startPos = lostID + 1;
        }
        else
        {

            dummyList.RemoveAt(lostID);
            dummyList.Add(list[lostID + 1]);
            dummyList.Add(list[lostID]);
            startPos = nextID + 1;
        }

        for (int i = startPos; i < list.Count; i++) { dummyList.Add(list[i]); }


        return dummyList;


    }


    /// <summary>
    /// レアリティの文字列を返す関数
    /// </summary>
    /// <param name="jokerRarity"></param>
    /// <returns></returns>
    public static string GetJokerRarityNema(this JokerActionUseEnum.JokerRarity jokerRarity)
    {
        string name = string.Empty;

        switch (jokerRarity)
        {
            case JokerActionUseEnum.JokerRarity.Common:
                name = "コモン";
                break;
            case JokerActionUseEnum.JokerRarity.Uncommon:
                name = "アンコモン";
                break;
            case JokerActionUseEnum.JokerRarity.Rare:
                name = "レア";
                break;
            case JokerActionUseEnum.JokerRarity.Legendary:
                name = "レジェンダリー";
                break;
        }

        return name;

    }

    /// <summary>
    /// 調整必要
    /// </summary>
    /// <param name="jokerRarity"></param>
    /// <returns></returns>
    public static Color GetJokerRarityColor(this string jokerRarity)
    {

        Color color = new Color();

        switch (jokerRarity)
        {
            case "コモン":
                color = new Color(0, 255, 227);
                break;
            case "アンコモン":
                color = new Color(8, 192, 0);
                break;
            case "レア":
                color = new Color(0, 72, 255);
                break;
            case "レジェンダリー":
                color = new Color(255, 0, 25);
                break;
        }

        return color;


    }

    /// <summary>
    /// バフの内容に応じた色を返す関数
    /// </summary>
    /// <param name="jokerbuff"></param>
    /// <returns></returns>
    public static Color GetBuffColor(this string jokerbuff)
    {

        Color color = new Color();

        // 文字が長い場合は文字のサイズを小さくすること対応
        switch (jokerbuff)
        {
            case "フォイル": color = new Color(200, 200, 200); break;
            case "ホログラム": color = new Color(200, 200, 200); break;
            case "ポリクローム": color = new Color(200, 200, 200); break;
            case "マウスジャマー": color = new Color(200, 200, 200); break;
            case "ボーナス": color = new Color(200, 200, 200); break;
            case "倍率": color = new Color(200, 200, 200); break;
            case "ワイルド": color = new Color(200, 200, 200); break;
            case "グラズ": color = new Color(200, 200, 200); break;
            case "スチール": color = new Color(200, 200, 200); break;
            case "ゴールド": color = new Color(200, 200, 200); break;
            case "ラッキー": color = new Color(200, 200, 200); break;
            case "ランダム": color = new Color(200, 200, 200); break;
            case "ブラインド": color = new Color(200, 200, 200); break;
            case "ネガティブ": color = new Color(200, 200, 200); break;
            case "セピア": color = new Color(200, 200, 200); break;
            case "オブジェクトムーブ": color = new Color(200, 200, 200); break;

            default: color = new Color(100, 100, 100); break;
        }

        return color;


    }

    public static string GetBuffExplanation(this string buff)
    {
        int index = 0;

        // 文字が長い場合は文字のサイズを小さくすること対応
        switch (buff)
        {
            case "フォイル": break;
            case "ホログラム": break;
            case "ポリクローム": break;
            case "マウスジャマー": break;
            case "ボーナス": break;
            case "倍率": break;
            case "ワイルド": break;
            case "グラズ": break;
            case "スチール": break;
            case "ゴールド": break;
            case "ラッキー": break;
            case "ランダム": break;
            case "ブラインド": break;
            case "ネガティブ": break;
            case "セピア": break;
            case "オブジェクトムーブ": break;
        }

        return StringMaster.instance.GetMaster(index);
    }

    public static string GetRedString(this string _string)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append("<color=#FF0000>");
        stringBuilder.Append(_string);
        stringBuilder.Append("</color>");

        return stringBuilder.ToString();

    }
    public static string GetBlueString(this string _string)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append("<color=#0000FF>");
        stringBuilder.Append(_string);
        stringBuilder.Append("</color>");

        return stringBuilder.ToString();

    }

    /// <summary>
    /// 文字化けをする可能性を作成
    /// バックドアを追加
    /// </summary>
    /// <param name="_string"></param>
    /// <returns></returns>
    public static string ErrorText(this string _string, bool backDoor = false)
    {
        //バックドアが有効の場合は何もせずに返す色なども使えない
        if (backDoor) return _string;

        int count = 0;

        List<Card.Trump> trumps = CardManager.instance.GetDeck();

        for (int i = 0; i < trumps.Count; i++)
        {
            if (trumps[i].sealBuff != Card.sealBuff.Black) continue;
            count++;
        }

        ////デバック用に固定
        count = 0;

        char[] chars = _string.ToCharArray();

        StringBuilder stringBuilder = new StringBuilder();

        bool colorFlag = false;

        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] == '”')
            {

                colorFlag = !colorFlag;

                if (colorFlag)
                {
                    i++;
                    stringBuilder.Append(GetColor(chars[i]));
                }
                else stringBuilder.Append("</color>");

                continue;

            }

            if (i % 5 >= count) { stringBuilder.Append(chars[i]); continue; }

            byte[] utf8Bytes = Encoding.UTF8.GetBytes(new char[] { chars[i] });
            stringBuilder.Append(Encoding.GetEncoding("shift_jis").GetString(utf8Bytes).ToCharArray()[0]);

        }

        return stringBuilder.ToString();
    }


    /// <summary>
    /// リストの中に条件を入れてその条件にあった物の個数を返す関数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static int GetCount<T>(this List<T> values, System.Func<T, bool> func)
    {
        int count = 0;

        for (int i = 0; i < values.Count; i++)
        {
            if (!func(values[i])) continue;
            count++;
        }
        return count;
    }


    /// <summary>
    /// 下の関数の更に派生関数
    /// 使いやすいように変更
    /// </summary>
    public static string TextString(this TextMeshProUGUI text,string _string,bool backDoor=false) 
    {

        string cash = _string.TextString(text.GetRectTransformSize(),1,backDoor);

        return cash;
    }


    /// <summary>
    /// テキストボックスの大きさを返す関数
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static Vector2 GetRectTransformSize(this TextMeshProUGUI text) 
    {
        float w = Mathf.Abs(text.GetComponent<RectTransform>().offsetMin.x) + Mathf.Abs( text.GetComponent<RectTransform>().offsetMax.x);
        float h = Mathf.Abs(text.GetComponent<RectTransform>().offsetMin.y) + Mathf.Abs( text.GetComponent<RectTransform>().offsetMax.y);
        return new Vector2(w, h);
    }


    /// <summary>
    /// テキストボックスに入れる文字の簡略化の関数
    /// </summary>
    /// <param name="_string"><すでに文字化けを通した文字列/param>
    /// <param name="textBoxSize"></param>
    /// <param name="Line"></param>
    /// <param name="backDoor"></param>
    /// <returns></returns>
    public static string TextString(this string _string, Vector2 textBoxSize, int Line = 1, bool backDoor = false) 
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(_string.GetMaxSizeString(textBoxSize));
        stringBuilder.Append(ErrorText(_string, backDoor));
        stringBuilder.Append(EndSize());

        return stringBuilder.ToString();


    }

    /// <summary>
    /// テキストボックスに入る最大の大きさになるように
    /// リッチテキストを返す関数
    /// </summary>
    /// <param name="_string"></param>
    /// <param name="textBoxSize"><テキストボックスの大きさ/param>
    /// <param name="Line"><行の数/param>
    /// <returns></returns>
    public static string GetMaxSizeString(this string _string, Vector2 textBoxSize, int Line = 1)
    {
        // 一行の文字の数
        int stringCount = _string.GetStringCount()/Line;

        
        // XとY方向に許容可能な最大値を取得
        float sizeX = ((textBoxSize.x / stringCount)-1);
        float sizeY = (textBoxSize.y / Line);

        // 小さい方を採用
        float size=Mathf.Min(sizeX, sizeY);

        string text = "<size=" + size.ToString() + ">";

        if (sizeX < 12)text= GetMaxSizeString(_string,textBoxSize,Line+1);

        return text;
    }

    /// <summary>
    /// リッチテキストの文字の大きさの調整を修正する
    /// </summary>
    /// <returns></returns>
    public static string EndSize() 
    {
        return "</size>";
    }

    private static string GetColor(char C)
    {
        switch (C)
        {


            case 'R': return "<color=#FF0000>";
            case 'B': return "<color=#0000FF>";
            case 'Y': return "<color=#BFBF00>";

        }

        return string.Empty;

    }

    private static int GetStringCount(this string _string) 
    {

        char[] chars = _string.ToCharArray();

        StringBuilder stringBuilder = new StringBuilder();

        bool colorFlag = false;

        int count = 0;

        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] == '<')
            {

                colorFlag = true;

                continue;

            }
            if (chars[i] == '>')
            {

                colorFlag = false;

                continue;

            }

            if(!colorFlag) count++;

        }

        return count;


    }

}