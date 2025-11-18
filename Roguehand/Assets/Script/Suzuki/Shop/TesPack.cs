using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// パックを選択時
/// </summary>
public class TesPack : SaleInterface
{
    /// <summary>
    /// 購入時
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
            //Use();


        }
    }

}
