using System.Collections;
using System.Collections.Generic;
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



}