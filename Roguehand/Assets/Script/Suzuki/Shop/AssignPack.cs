using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ScriptCountNumber;
using static Extra;
using System.Text;
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
    private InstantiatePack.PackType _type;

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
    public void Use<T>(List<T> values,List<Vector3>poss)
    {
        // パックの中身を選択中は他のショップのカードを削除
        SaleObjectManager.instance.AllInactive();
        SaleObjectManager.instance.SetPackSelectCount(_packGetCount);
        SaleObjectManager.instance.ChengePackMode(true);

        List<GameObject> cards = new();
        // 購入時のアクション

        for (int i = 0; i < values.Count; i++)
        {
            GameObject card = Instantiate(this.gameObject);

            // 自分自身のクラスを破棄
            Destroy(card.GetComponent<AssignPack>());

            card.AddComponent<PackInObject>().SetTragetPos(poss[i]);

            // マテリアルの貼り付け
            //card.GetComponent<MeshRenderer>().materials = GetTypeMaterial(_type, values()[i]);

            cards.Add(card);

            System.Action buy = TypeBay(values[i],card);
            System.Action explation = ShopExplamtion(card, values[i]);

            SaleObjectManager.instance.ProductExplantion(0);

            SaleObjectManager.instance.AddProducts(card,
                explation,
                buy,
                false);
        }

        // カードのオブジェクトの座標を移動させる関数を
        //saleObjectmanagerに渡す

        Debug.Log("パックの使用");

    }

    public string GetName()
    {
        StringBuilder　sb = new StringBuilder();

        // +１がメガの文字
        if (_packCardCount > 4) sb.Append(MasterData.instance.GetStringMaster( IDUtility.PACK_ID + 1));

        // +2がからが種類の文字
        sb.Append(MasterData.instance.GetStringMaster(IDUtility.PACK_ID + 2+(int)_type));

        // パックの名前
        sb.Append(MasterData.instance.GetStringMaster(IDUtility.PACK_ID));

        return sb.ToString();
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
        return (MasterData.instance.GetStringMaster(IDUtility.PACK_ID + 2 + (int)_type)+ (MasterData.instance.GetStringMaster(IDUtility.PACK_ID)));
    }

    private void PackTime(List<GameObject> cards)
    {
        if (!isPack) return;


    }

    private readonly int onlyMaterialCount = 4;
    private Material[] GetTypeMaterial<T>(InstantiatePack.PackType type, T t)
    {

        Material[] materials = new Material[onlyMaterialCount];

        switch (type)
        {
            case InstantiatePack.PackType.joker:
                JokerBase joker = t as JokerBase;

                break;
            case InstantiatePack.PackType.item:


                break;
        }





        return materials;
    }

    private System.Action TypeBay<T>(T t,GameObject card)
    {
        switch (_type)
        {
            case InstantiatePack.PackType.joker:
                JokerBase joker = t as JokerBase;
                return () => 
                {
                    JokerUtility.Addjoker(joker.GetID()-IDUtility.JOKER_ID-1);

                    SaleObjectManager.instance.PackSekect(card);   


                };
            case InstantiatePack.PackType.item:
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
            case InstantiatePack.PackType.joker:

                JokerBase joker = t as JokerBase;
                actions.Add(() => { SaleUtility.SetSale(joker, gameObject, 0, false); });
                actions.Add(() => { JokerUtility.ShowExplanation(gameObject, joker, SHOP_UI_OFFSET); });


                break;
            case InstantiatePack.PackType.item:
                break;
        }

        return () => { for (int i = 0; i < actions.Count; i++) actions[i](); };

    }

    private int[] GetBuffs<T>(T t)
    {
        switch (_type)
        {
            case InstantiatePack.PackType.joker:
                JokerBase joker = t as JokerBase;
                return joker.JokerBuffs();
                //case TesPack.PackType.card:

        }

        return new int[0];

    }


    void SaleInterface.BuyShow(Vector3 pos, int saleValue, System.Action action)
    {
        Vector2 ButtonPos = Camera.main.WorldToScreenPoint(pos);

        float BUY_WIDHT = 100;

        if (GUI.Button(new Rect(ButtonPos.x - BUY_WIDHT / HALF, Screen.height - ButtonPos.y + 100, BUY_WIDHT, 60),
            ("<size=30><color=#ffffff>" + Extra.ErrorText("購入") + "</color></size>"), SaleUtility.GetStyle()))
        {
            action();

        }
    }


}
