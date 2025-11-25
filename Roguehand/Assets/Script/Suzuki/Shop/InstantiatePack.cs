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
    private float distance = 0;
    private int MAX_PACK = 3;
    private List<GameObject> _packs = new();
    private bool _isInstantiate = false;

    public enum PackType
    {
        none = -1,
        joker,
        item,
        spectrum,
        trump,
        max
    }

    private void Update()
    {
        Debug.Log(_packs.Count + "パックの数");

        CheckNotShop();
        if (!ShopManager.instance.IsShop()) return;
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
    private void PackCreate()
    {
        if (_isInstantiate) return;


        // 置けるパック分生成
        for (int i = 0; i < MAX_PACK; i++)
        {

            PackType pack = (PackType)UnityEngine.Random.Range(0, (int)PackType.max);

            // 生成
            _packs.Add(Instantiate(_pack, _packZone));
            // クラスの付与
            _packs[i].AddComponent<AssignPack>();
            // このキャッシュは必須
            int cash = i;
            AssignPack obj = _packs[i].GetComponent<AssignPack>();
            obj.Initialize();
            // 今は固定値で作成数と選択数を置いている
            obj.Create(pack, 5, 2);
            // 目標座標をセット
            SaleObjectManager.instance.ProductExplantion(obj.GetSaleValue());
            SaleObjectManager.instance.AddProducts(_packs[i],
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
                        case PackType.spectrum:
                            break;
                        case PackType.trump:
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
    /// アイテムのリストを返す関数
    /// </summary>
    /// <param name="createCount"></param>
    /// <returns></returns>
    private List<Card.TrumpClass> GetRandomTrump(int createCount)
    {
        List<Card.TrumpClass> itemBases = new List<Card.TrumpClass>();

        for (int i = 0; i < createCount; i++)
        {
            Card.Trump trump = new Card.Trump();

            trump.number = (Card.number)UnityEngine.Random.Range(1, (int)Card.number.max);
            trump.suit = (Card.suit)UnityEngine.Random.Range(0, (int)Card.suit.max);

            int buffCount = UnityEngine.Random.Range(0, 4);

            bool continueFlag = true;
            // 基本的にコンてにゅーで返すから上昇無し
            for (int j = 0; j < buffCount;)
            {
                int buffNum = UnityEngine.Random.Range(0, 4);

                switch (buffNum)
                {
                    //case 0:if (trump.sealBuff == Card.sealBuff.None) { } break;
                    case 1: break;
                    case 2: break;
                }

                if (continueFlag) continue;




            }

            i++;

        }

        return itemBases;

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
            poss.Add(_packItemLeftTargetPos.position + new Vector3(distance * (i + 1), 0, 0));

        return poss;

    }

}
