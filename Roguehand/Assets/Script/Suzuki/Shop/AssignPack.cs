using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// パック一つ一つに付与されるクラス
/// </summary>
public class AssignPack : MonoBehaviour, SaleInterface, ExplanationInterface
{
    /// <summary>
    /// パックの購入にかかるお金の量
    /// </summary>
    private int saleValue = 0;

    private readonly Vector2 SHOP_UI_OFFSET = new Vector2(0.8f, 0);

    /// <summary>
    /// パックの取得可能なカードの種類
    /// </summary>
    private TesPack.PackType _type;

    private int _packCardCount = 0;

    private int _packGetCount = 0;

    public static bool isPack = false;

    /// <summary>s
    /// 初期化処理
    /// </summary>
    public void Initialize()
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

    /// <summary>
    /// パックを生成した時の処理 initializと同じような扱い
    /// </summary>
    /// <param name="createCount"><何枚生成するかどうか/param>
    /// <param name="getCount"><何枚獲得できるかどうか/param>
    public void Create(InstantiatePack.PackType packType, int createCount, int getCount)
    {
        _type = packType;
        _packCardCount = createCount;
        _packGetCount = getCount;
    }

    /// <summary>
    /// パックを開けたときの処理
    /// </summary>
    public void Use<T>(System.Func<List<T>> values=null)
    {
        if (values == null)
        {
            switch (_type)
            {
                case TesPack.PackType.joker:

                    break;
                case TesPack.PackType.card:

                    break;
            }
        }

        List<GameObject> cards = new();
        // 購入時のアクション

        for (int i = 0; i < values().Count; i++)
        {
            GameObject card = Instantiate(this.gameObject);

            // 自分自身のクラスを破棄
            Destroy(card.GetComponent<TesPackObject>());

            // マテリアルの貼り付け
            //card.GetComponent<MeshRenderer>().materials = GetTypeMaterial(_type, values()[i]);

            cards.Add(card);

            System.Action buy = TypeBay(values()[i]);
            System.Action explation = ShopExplamtion(card, values()[i]);

            SaleObjectManager.instance.AddProducts(card,
                explation,
                buy,
                true);
        }

        // カードのオブジェクトの座標を移動させる関数を
        //saleObjectmanagerに渡す


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

    private void PackTime(List<GameObject> cards)
    {
        if (!isPack) return;


    }

    private readonly int onlyMaterialCount = 4;
    private Material[] GetTypeMaterial<T>(TesPack.PackType type, T t)
    {

        Material[] materials = new Material[onlyMaterialCount];

        switch (type)
        {
            case TesPack.PackType.joker:
                JokerBase joker = t as JokerBase;

                break;
            case TesPack.PackType.card:


                break;
        }





        return materials;
    }

    private System.Action TypeBay<T>(T t)
    {
        switch (_type)
        {
            case TesPack.PackType.joker:

                return () => { };
            case TesPack.PackType.card:
                return () => { };


        }


        return () => { };
    }
    // まだじょーかにしか対応していない
    private System.Action ShopExplamtion<T>(GameObject gameObject, T t)
    {
        List<System.Action> actions = new List<System.Action>();

        switch (_type)
        {
            case TesPack.PackType.joker:

                JokerBase joker = t as JokerBase;
                actions.Add(() => { SaleUtility.SetSale(joker, gameObject, 0, true); });
                actions.Add(() => { JokerUtility.ShowExplanation(gameObject, joker); });


                break;
            case TesPack.PackType.card:
                break;
        }

        return () => { for (int i = 0; i < actions.Count; i++) actions[i](); };

    }

    private int[] GetBuffs<T>(T t)
    {
        switch (_type)
        {
            case TesPack.PackType.joker:
                JokerBase joker = t as JokerBase;
                return joker.JokerBuffs();
                //case TesPack.PackType.card:

        }

        return new int[0];

    }

}
