using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ScriptCountNumber;
public interface SaleInterface
{
    /// <summary>
    /// 売却額の描画する関数
    /// </summary>
    public void SaleShow(Vector3 pos, int saleValue, System.Action action)
    {
        Vector2 ButtonPos = Camera.main.WorldToScreenPoint(pos);
        if (GUI.Button(new Rect(ButtonPos.x + 75, Screen.height - ButtonPos.y - 30, 70, 90),
            ("<size=25><color=#ffffff>売却\n$" + saleValue.ToString() + "</color></size>"), SaleUtility.GetStyle()))
        {
            action();

            //お金を増やす処理
            GameUtility.SetMyMoney(GameUtility.GetMyMoney() + saleValue);

            VolumeManager.instance.PlayMoneySE();
        }
    }

    /// <summary>
    /// 購入時の描画をする関数
    /// </summary>
    public void BuyShow(Vector3 pos, int saleValue, System.Action action)
    {
        Vector2 ButtonPos = Camera.main.WorldToScreenPoint(pos);

        float BUY_WIDHT = 100;

        if (!AddFlag())
        {
            NotAddButton(ButtonPos);

            return;
        }

        if (GUI.Button(new Rect(ButtonPos.x - (BUY_WIDHT /HALF), Screen.height - ButtonPos.y + 100, BUY_WIDHT, 60),
            ("<size=30><color=#ffffff>" + Extra.ErrorText("購入") + "</color></size>"), SaleUtility.GetStyle()))
        {
            // お金が足りているかどうかの判断
            if (GameUtility.GetMyMoney() < saleValue) { Debug.Log("お金が足りない"); return; }

            // お金を減らす処理
            GameUtility.SetMyMoney(GameUtility.GetMyMoney() - saleValue);

            action();

            VolumeManager.instance.PlayMoneyShop();
        }
    }

    /// <summary>
    /// 追加が可能かどうかを判断する関数
    /// </summary>
    /// <returns></returns>
    public bool AddFlag() {  return true; }

    /// <summary>
    /// 追加をできないときのボタン
    /// </summary>
    public void NotAddButton(Vector2 ButtonPos) { }
}
