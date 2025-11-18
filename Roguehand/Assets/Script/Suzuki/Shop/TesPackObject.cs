using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// パック一つ一つに付与されるクラス
/// </summary>
public class TesPackObject : MonoBehaviour,SaleInterface,ExplanationInterface
{
    /// <summary>
    /// パックの購入にかかるお金の量
    /// </summary>
    private int saleValue = 0;

    private readonly Vector2 SHOP_UI_OFFSET = new Vector2(0.8f, 0);

    /// <summary>s
    /// 初期化処理
    /// </summary>
    public void Initializ() 
    {

    }

    public int GetSaleValue() { return saleValue; }

    /// <summary>
    /// 説明を描画する関数
    /// </summary>
    public void ShopExplantion() 
    {
        SaleUtility.SetSale(this, gameObject, GetSaleValue(), false);

        // バフがないからこれで騙す
        int[] dommyBuff = new int[0];

        ExplanationManager.instance.AddExplanation(gameObject, this, dommyBuff, SHOP_UI_OFFSET);

    }

    public string GetName()
    {
        return "名前";
    }

    public string GetExplanation()
    {
        return "説明１";
    }

    public string GetExplanation2()
    {
        return "説明２";
    }

    public string GetTypes()
    {
        return "パック";
    }
}
