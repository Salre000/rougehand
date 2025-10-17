using System.Collections;
using System.Collections.Generic;
using System.Text;
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
    public static List<T> ChengeOrder<T>(List<T> list,int lostID,int nextID) 
    {
        List<T> dummyList= new List<T>();   
        //ネクストの一個前まで追加
        for (int i = 0; i < nextID; i++) { dummyList.Add(list[i]); }

        int startPos = 0;

        if (nextID < lostID) 
        {
            dummyList.Add(list[lostID]);
            dummyList.Add(list[lostID-1]);
            startPos = lostID + 1;
           }
        else
        {

            dummyList.RemoveAt(lostID);
            dummyList.Add(list[lostID+1]);
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
        string name=string.Empty;

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

        Color color=new Color();

        switch (jokerRarity)
        {
            case "コモン":
                color = new Color(0,255,227);
                break;
            case "アンコモン":
                color = new Color(8,192,0);
                break;
            case "レア":
                color = new Color(0,72,255);
                break;
            case "レジェンダリー":
                color = new Color(255,0,25);
                break;
        }

        return color;


    }

    public static string GetRedString(this string _string) 
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append("<color=#FF0000>");
        stringBuilder.Append(_string);
        stringBuilder.Append("</color>");

        return stringBuilder.ToString();

    }

}