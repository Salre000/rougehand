using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 星座カードを纏めてこのクラス  
/// </summary>
public class ConstellationItem : ItemBase
{
    /// <summary>
    /// どんな星座なのかを表す変数
    /// </summary>
    private int _constellationID = -1;

    public override void Initializ()
    {
        // いまのところ特になし


    }

    public override void Use()
    {

        Debug.Log("星座カードが使用されたよ");
        // IDを使用して星座のレベルを上昇させる処理をかく



    }


    /// <summary>
    /// どんな星座かを確定する関数
    /// </summary>
    /// <param name="ID"></param>
    public void SetConstellationID(int ID) { _constellationID = ID; }


}
