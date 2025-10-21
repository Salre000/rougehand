using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KeepData
{


    /// <summary>
    /// キープデータの作成
    /// </summary>
    public static void KeepDataBackup() { }


    /// <summary>
    /// キープデータの読み込み
    /// </summary>
    public static void KeepDataLood() { }

    /// <summary>
    /// キープデータの初期化
    /// </summary>
    public static void KeepDataInitializ() { }


    /// <summary>
    /// データを保持するインナークラス
    /// </summary>
    
    private class Data
    {
        /// <summary>
        /// 保存するデッキ
        /// </summary>
        public List<Card.Trump> deck=new List<Card.Trump>();

        /// <summary>
        /// 保存するジョーカー
        /// </summary>
        public List<JokerBase> jokers=new List<JokerBase>();

        /// <summary>
        /// 保存するアイテム
        /// </summary>
        public List<ItemBase> items=new List<ItemBase>();






    }


}