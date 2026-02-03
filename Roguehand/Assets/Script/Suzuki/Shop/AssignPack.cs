using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static Extra;
using static ScriptCountNumber;
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

    private GameObject defaultObject;


    /// <summary>s
    /// 初期化処理
    /// </summary>
    public void Initialize()
    {
    }

    public int GetSaleValue() { return saleValue; }

    public void SetDefaultObject(GameObject gameObject) 
    {
        defaultObject = gameObject;
    }

    /// <summary>
    /// 説明を描画する関数
    /// </summary>
    public void ShopExplantion()
    {

        // バフがないからこれで騙す
        int[] dommyBuff = new int[0];

        ExplanationManager.instance.AddExplanation(gameObject, this, dommyBuff, SHOP_UI_OFFSET);

    }

    public void ShopSale() 
    {
        SaleUtility.SetSale(this, gameObject, 0, false);
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

        saleValue = _packCardCount * _packGetCount;
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
            GameObject card = Instantiate(defaultObject);

            card.GetComponent<Rigidbody>().useGravity = false;


            card.GetComponent<Rigidbody>().constraints = 
                RigidbodyConstraints.FreezePosition| 
                RigidbodyConstraints.FreezeRotation;


            card.AddComponent<PackInObject>().SetTragetPos(poss[i]);

            // マテリアルの貼り付け
            GetTypeMaterial(_type, values[i],card);

            cards.Add(card);

            System.Action buy = TypeBay(values[i],card);
            System.Action explation = ShopExplamtion(card, values[i]);
            System.Action show = ShopSaleShow(card, values[i]);

            SaleObjectManager.instance.ProductExplantion(0);

            SaleObjectManager.instance.AddProducts(card,
                show,
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
        StringBuilder sb = new StringBuilder();
        int ExplanationRate = 500;
        

        // +１がメガの文字
        if (_packCardCount > 4) sb.Append(MasterData.instance.GetStringMaster(IDUtility.PACK_ID + 1+ ExplanationRate));
        else sb.Append(MasterData.instance.GetStringMaster(IDUtility.PACK_ID+ ExplanationRate));

        return sb.ToString();

    }

    public string GetExplanation2()
    {
        StringBuilder sb = new StringBuilder();

        int ExplanationRate = 10;

        // +2がからが種類の文字
        sb.Append(MasterData.instance.GetStringMaster(IDUtility.PACK_ID + 2 + (int)_type+ ExplanationRate));
        return sb.ToString();
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
    private void GetTypeMaterial<T>(InstantiatePack.PackType type, T t,GameObject gameObject)
    {


        switch (type)
        {
            case InstantiatePack.PackType.joker:
                JokerBase joker = t as JokerBase;

                JokerObjectUtility.PaintJokerObject(joker, gameObject);
                break;
            case InstantiatePack.PackType.item:

                ItemBase item = t as ItemBase;
                ItemUtility.PaintItemObject(item, gameObject);
                break;
        }





        return ;
    }

    private System.Action TypeBay<T>(T t,GameObject card)
    {
       
        switch (_type)
        {
            case InstantiatePack.PackType.joker:
                JokerBase joker = t as JokerBase;
                return () => 
                {
                    JokerUtility.AddJoker(joker.GetID()-IDUtility.JOKER_ID-1);

                    SaleObjectManager.instance.PackSekect(card);

                    JokerObjectUtility.JokerObjectALLAction(
                        joker => { joker.gameObject.SetActive(false); return joker; });


                };
            case InstantiatePack.PackType.item:
                ItemBase itemBase = t as ItemBase;
                return () => 
                {

                    ItemUtility.AddItem(
                        itemBase.GetID()<(int)ConstellationItem.ConstellationType.MAX?0:itemBase.GetID()- ((int)ConstellationItem.ConstellationType.MAX-1));

                    ItemUtility.SetItemID(itemBase.GetID());

                    SaleObjectManager.instance.PackSekect(card);
                    ItemUtility.ItemALLAction(
                        item => { item.gameObject.SetActive(false); return item; });


                };
            case InstantiatePack.PackType.trump:
                Card.TrumpClass trumpClass= t as Card.TrumpClass;
                return () =>
                {
                    CardObjectUtility.AddTrump(trumpClass.trump);
                    SaleObjectManager.instance.PackSekect(card);

                };


        }


        return () => { };
    }
    // まだじょーかにしか対応していない
    private System.Action ShopSaleShow<T>(GameObject gameObject, T t)
    {
        List<System.Action> actions = new List<System.Action>();

        switch (_type)
        {
            case InstantiatePack.PackType.joker:

                JokerBase joker = t as JokerBase;
                actions.Add(() => { SaleUtility.SetSale(joker, gameObject, 0, false); });


                break;
            case InstantiatePack.PackType.item:
                ItemBase itemBase =t as ItemBase;

                actions.Add(() => { SaleUtility.SetSale(itemBase, gameObject, 0, false); });
                break;

            case InstantiatePack.PackType.trump:
                Card.TrumpClass trumpClass = t as Card.TrumpClass;
                DommySaleObject doomy = new DommySaleObject();
                actions.Add(() => { SaleUtility.SetSale(doomy, gameObject, 0, false); });

                break;  
        }

        return () => { for (int i = 0; i < actions.Count; i++) actions[i](); };

    }
    private System.Action ShopExplamtion<T>(GameObject gameObject, T t)
    {
        List<System.Action> actions = new List<System.Action>();

        switch (_type)
        {
            case InstantiatePack.PackType.joker:

                JokerBase joker = t as JokerBase;
                actions.Add(() => { JokerUtility.ShowExplanation(gameObject, joker, SHOP_UI_OFFSET); });


                break;
            case InstantiatePack.PackType.item:
                ItemBase itemBase =t as ItemBase;

                actions.Add(() => {ItemUtility.ShowExplanation(gameObject, itemBase, SHOP_UI_OFFSET); });
                break;

            case InstantiatePack.PackType.trump:
                Card.TrumpClass trumpClass = t as Card.TrumpClass;
                DommySaleObject doomy = new DommySaleObject();
                actions.Add(() => { CardObjectUtility.ShowExplanation(trumpClass.trump, gameObject, SHOP_UI_OFFSET); });

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

}
