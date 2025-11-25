using System;
using System.Collections;
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
        Debug.Log(_packs.Count);

        if (!ShopManager.instance.IsShop()) return;
        PackCreate();


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
            // 生成
            _packs.Add(Instantiate(_pack, _packZone));
            // クラスの付与
            _packs[i].AddComponent<AssignPack>();
            // このキャッシュは必須
            int cash = i;
            AssignPack obj = _packs[i].GetComponent<AssignPack>();
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
        int num=0;
        for(int i=0; i < _packs.Count; i++)
        {
            if (_packs[i]==null)continue;
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
            float dis = (float)(i- minus + 1) / num;
            _packs[i].transform.position = Vector3.Lerp(_leftTargetPos.position, _rightTargetPos.position, dis);
        }
    }

    /// <summary>
    /// パック購入時並び替えをする
    /// </summary>
    /// <param name="ID">購入されたパックのID</param>
    private void BuyTrans(int ID)
    {
        _packs[ID]=null;
        Trans();

    }

}
