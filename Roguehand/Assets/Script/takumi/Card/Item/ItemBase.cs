using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムの抽象クラス
/// </summary>
public abstract class ItemBase
{


    /// <summary>
    /// 売却額の変数  
    /// </summary>
    protected int _returnMoney = 0;


    /// <summary>
    /// クラスの初期化処理
    /// </summary>
    public abstract void Initializ();


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
    public void AddReturnMoney(int add) {  _returnMoney += add; }






}
