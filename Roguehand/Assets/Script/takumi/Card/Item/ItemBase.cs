using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムの抽象クラス
/// </summary>
public abstract class ItemBase : SaleInterface,ExplanationInterface
{

    /// <summary>
    /// アイテムのID
    /// </summary>
    private int itemID = -1;

    public int GetID() {  return itemID; }
    /// <summary>
    /// 売却額の変数  
    /// </summary>
    protected int _returnMoney = 0;


    /// <summary>
    /// クラスの初期化処理
    /// </summary>
    public abstract void Initializ();

    /// <summary>
    /// 売却に使う関数に変更を加えて使用を可能にした
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="saleValue"></param>
    /// <param name="action"></param>
    void SaleInterface.SaleShow(Vector3 pos, int saleValue, Action action)
    {

        Vector2 ButtonPos = Camera.main.WorldToScreenPoint(pos);
        if (GUI.Button(new Rect(ButtonPos.x + 75, Screen.height - ButtonPos.y - 90, 60, 90),
            ("<size=25><color=#ffffff>売却\n$" + saleValue.ToString() + "</color></size>"), SaleUtility.GetStyle()))
        {

            action();

            //お金を増やす処理


        }

        if (GUI.Button(new Rect(ButtonPos.x + 75, Screen.height - ButtonPos.y, 60, 90),
            ("<size=25><color=#ffffff>使用\n</color></size>"), SaleUtility.GetStyle()))
        {
            action();
            //ジョーカーにアイテムの使用を知らせる
            JokerUtility.SetTraget(JokerActionUseEnum.JokerActionTarget.item);
            Use();


        }
    }


    /// <summary>
    /// アイテムの使用時の関数
    /// </summary>
    public abstract void Use();


    /// <summary>
    /// 売却時のお金の量を返す関数
    /// </summary>
    /// <returns></returns>
    public int ReturnMoney() { return _returnMoney; }

    /// <summary>
    /// 売却時のお金の量を増やす関数
    /// </summary>
    /// <param name="add"></param>
    public void AddReturnMoney(int add) { _returnMoney += add; }

    public void SetItemID(int ID) { itemID = ID; }

    public string GetName()
    {
        //アイテム係数1000
        return MasterData.instance.GetStringMaster(1000+itemID);
    }

    public string GetExplanation()
    {
        // アイテムの説明係数1500
        return MasterData.instance.GetStringMaster(1500 + itemID);
    }

    public string GetExplanation2()
    {
        return string.Empty;
    }

    public string GetTypes()
    {
        // 予定は未定
        return Extra.ErrorText("アイテム");
    }
}
