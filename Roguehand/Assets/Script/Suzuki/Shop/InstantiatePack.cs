using System;
using System.Collections.Generic;
using UnityEngine;
using static ScriptCountNumber;

/// <summary>
/// パックの生成
/// </summary>
public class InstantiatePack : MonoBehaviour
{
    [SerializeField] GameObject _pack;
    [SerializeField] Transform _packZone;
    [SerializeField] Transform _targetPos;
    [SerializeField] Transform _leftTargetPos;
    [SerializeField] Transform _rightTargetPos;
    [SerializeField] Transform _packItemLeftTargetPos;
    [SerializeField] Transform _packItemRightTargetPos;
    [SerializeField] Transform _packTrumpTargetPos;
    [SerializeField, Header("パック内のデフォルトオブジェクト")]
    GameObject card;
    /// <summary>
    /// パックのマテリアルを管理するクラス
    /// </summary>
    [SerializeField] private PackMaterialManager materialManager;
    private float distance = 0;
    private const int MAX_PACK = 3;
    private List<GameObject> _packs = new();
    private GameObject trumpPack;
    private bool _isInstantiate = false;

    public enum PackType
    {
        none = -1,
        joker,
        item,
        trump,
        max
    }

    private void Update()
    {
        Debug.Log(_packs.Count + "パックの数");

        CheckNotShop();
        if (!ShopManager.instance.IsShop()) return;
        PackCreateTrump();
        PackCreate();



    }

    private void CheckNotShop()
    {
        if (ShopManager.instance.IsShop()) return;
        if (!_isInstantiate) return;

        _packs.Clear();



        _isInstantiate = false;
    }
    /// <summary>
    /// ショップ入場時にパックが作成される
    /// </summary>
    private void PackCreateTrump()
    {
        if (_isInstantiate) return;


        // 置けるパック分生成

        PackType pack = PackType.trump;

        // 生成
        trumpPack=(Instantiate(_pack, _packZone));
        // クラスの付与
        trumpPack.AddComponent<AssignPack>();
        // このキャッシュは必須
        AssignPack obj = trumpPack.GetComponent<AssignPack>();
        obj.Initialize();
        obj.SetDefaultObject(card);
        // 今は固定値で作成数と選択数を置いている
        obj.Create(pack, 5, 2);


        materialManager.SetPackPaint(trumpPack, pack, 5);

        // 目標座標をセット
        SaleObjectManager.instance.ProductExplantion(obj.GetSaleValue());
        SaleObjectManager.instance.AddProducts(trumpPack,
            () => { obj.ShopSale(); },
            () => { obj.ShopExplantion(); },
            () =>
            {

                Debug.Log("パックを購入したよー");
                // パックの購入時の処理を描く
                PackManager.instance.SetIsBuyPack(true);
                GameObject domyy = trumpPack;
                // 選択されたパックオブジェクトをマネージャーに保存
                PackManager.instance.SetPickPack(domyy);
                SaleObjectManager.instance.Remove(domyy);


                switch (pack)
                {
                    case PackType.joker:
                        obj.Use(GetRandomJoker(5), GetPos(5));
                        break;
                    case PackType.item:
                        obj.Use(GetRandomItem(5), GetPos(5));
                        break;
                    case PackType.trump:
                        obj.Use(GetRandomTrump(5), GetPos(5));
                        break;
                }


            }
            , true
            );
        TrumpPackTrans();
    }
    private void PackCreate(PackType type = PackType.none, int count = MAX_PACK)
    {
        if (_isInstantiate) return;


        // 置けるパック分生成
        for (int i = 0; i < MAX_PACK; i++)
        {

            PackType pack = type == PackType.none ?
                (PackType)UnityEngine.Random.Range(0, (int)PackType.max - 1)
                : type;

            // 生成
            _packs.Add(Instantiate(_pack, _packZone));
            // クラスの付与
            _packs[i].AddComponent<AssignPack>();
            // このキャッシュは必須
            int cash = i;
            AssignPack obj = _packs[i].GetComponent<AssignPack>();
            obj.Initialize();
            obj.SetDefaultObject(card);
            // 今は固定値で作成数と選択数を置いている
            obj.Create(pack, 5, 2);


            materialManager.SetPackPaint(_packs[i], pack, 5);

            // 目標座標をセット
            SaleObjectManager.instance.ProductExplantion(obj.GetSaleValue());
            SaleObjectManager.instance.AddProducts(_packs[i],
                () => { obj.ShopSale(); },
                () => { obj.ShopExplantion(); },
                () =>
                {

                    Debug.Log("パックを購入したよー");
                    // パックの購入時の処理を描く
                    PackManager.instance.SetIsBuyPack(true);
                    GameObject domyy = _packs[cash];
                    // 選択されたパックオブジェクトをマネージャーに保存
                    PackManager.instance.SetPickPack(domyy);
                    SaleObjectManager.instance.Remove(domyy);
                    BuyTrans(cash);


                    switch (pack)
                    {
                        case PackType.joker:
                            obj.Use(GetRandomJoker(5), GetPos(5));
                            break;
                        case PackType.item:
                            obj.Use(GetRandomItem(5), GetPos(5));
                            break;
                        case PackType.trump:
                            obj.Use(GetRandomTrump(5), GetPos(5));
                            break;
                    }


                }
                , true
                );


        }
        Trans();
        _isInstantiate = true;
    }


    /// <summary>
    /// 並び替え
    /// </summary>
    private void Trans()
    {
        // leftとrightから直線を作り、線を分割することで中心点を出す
        // 終点に乗らないように+1する(後ろを増やす)
        int num = 0;
        for (int i = 0; i < _packs.Count; i++)
        {
            if (_packs[i] == null) continue;
            num++;
        }
        num += 1;
        int minus = 0;
        for (int i = 0; i < _packs.Count; i++)
        {
            if (_packs[i] == null)
            {
                minus++;
                continue;
            }
            // 始点に乗らないように+1する(前を増やす)
            // 購入済みパックは考慮させないようminusをはさむ
            float dis = (float)(i - minus + 1) / num;
            _packs[i].transform.position = Vector3.Lerp(_leftTargetPos.position, _rightTargetPos.position, dis);
        }
    }

    private void TrumpPackTrans() 
    {
        if (trumpPack == null) return;

        trumpPack.transform.position = _packTrumpTargetPos.position;

    }

    /// <summary>
    /// パック購入時並び替えをする
    /// </summary>
    /// <param name="ID">購入されたパックのID</param>
    private void BuyTrans(int ID)
    {
        _packs[ID] = null;
        Trans();

    }

    /// <summary>
    /// ジョーカーのリストを返す関数
    /// </summary>
    /// <param name="createCount"></param>
    /// <returns></returns>
    private List<JokerBase> GetRandomJoker(int createCount)
    {
        List<JokerBase> jokerBases = new List<JokerBase>();

        for (int i = 0; i < createCount; i++)
            jokerBases.Add(ALLJoker.GetJoker((ALLJoker._allJokerEnum)UnityEngine.Random.Range(0, (int)ALLJoker._allJokerEnum.MAX)));

        return jokerBases;

    }
    /// <summary>
    /// アイテムのリストを返す関数
    /// </summary>
    /// <param name="createCount"></param>
    /// <returns></returns>
    private List<ItemBase> GetRandomItem(int createCount)
    {
        List<ItemBase> itemBases = new List<ItemBase>();

        for (int i = 0; i < createCount; i++)
        {
            itemBases.Add(ALLItem.GetItem((ALLItem.ALLItemEnum)UnityEngine.Random.Range(0, (int)ALLItem.ALLItemEnum._MAX)));

            itemBases[i].Initializ();

        }

        return itemBases;

    }
    /// <summary>
    /// トランプのリストを返す関数
    /// </summary>
    /// <param name="createCount"></param>
    /// <returns></returns>
    private List<Card.TrumpClass> GetRandomTrump(int createCount)
    {
        List<Card.TrumpClass> trumps = new List<Card.TrumpClass>();

        for (int i = 0; i < createCount; i++)
        {
            Card.Trump trump = new Card.Trump();

            trump.number = (Card.number)UnityEngine.Random.Range(1, (int)Card.number.max);
            trump.suit = (Card.suit)UnityEngine.Random.Range(0, (int)Card.suit.max);
            trump.sealBuff = Card.sealBuff.None;
            trump.cardBuff = Card.cardBuff.None;
            trump.deckBuff = Card.deckBuff.None;

            int buffCount = UnityEngine.Random.Range(0, 4);

            bool continueFlag = true;
            // 基本的にコンてにゅーで返すから上昇無し
            for (int j = 0; j < buffCount;)
            {
                int buffNum = 0;//UnityEngine.Random.Range(0, 4);

                switch (buffNum)
                {
                    case 0:
                        if (trump.sealBuff == Card.sealBuff.None)
                        {
                            j = 10; ;
                            trump.sealBuff = Card.sealBuff.Red;
                            
                            //trump.sealBuff = (Card.sealBuff)UnityEngine.Random.Range(0, (int)Card.sealBuff.MAX);
                        }
                        break;
                    case 1:
                        if (trump.cardBuff == Card.cardBuff.None)
                        {
                            j++;
                            trump.cardBuff = (Card.cardBuff)UnityEngine.Random.Range(0, (int)Card.cardBuff.MAX);
                        }
                        break;
                    case 2:
                        if (trump.deckBuff == Card.deckBuff.None)
                        {
                            j++;
                            trump.deckBuff = (Card.deckBuff)UnityEngine.Random.Range(0, (int)Card.deckBuff.MAX);
                        }
                        break;
                }

                if (continueFlag) continue;
            }

            trumps.Add(new Card.TrumpClass(trump));

        }
        return trumps;

    }

    /// <summary>
    /// 座標を返す関数
    /// </summary>
    /// <param name="createCount"></param>
    /// <returns></returns>
    private List<Vector3> GetPos(int createCount)
    {
        List<Vector3> poss = new List<Vector3>();
        //　パックな中身の距離を取得
        distance = Vector3.Distance(_packItemLeftTargetPos.position, _packItemRightTargetPos.position) / (createCount + 1);

        for (int i = 0; i < createCount; i++)
            poss.Add(_packItemLeftTargetPos.position + new Vector3(distance * (i + 1), -10, 0));

        return poss;

    }

}
