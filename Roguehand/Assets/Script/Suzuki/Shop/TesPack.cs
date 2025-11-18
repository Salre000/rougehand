using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ScriptCountNumber;

/// <summary>
/// パックの生成
/// </summary>
public class TesPack : MonoBehaviour
{
    [SerializeField] GameObject _pack;
    [SerializeField] Transform _packZone;
    [SerializeField] Transform _targetPos;
    [SerializeField] Transform _leftTargetPos;
    [SerializeField] Transform _rightTargetPos;
    private int ID;
    private int MAX_PACK = 3;
    private List<GameObject> _packs = new();
    private bool _isInstantiate = false;

    public enum PackType
    {
        none = -1,
        joker,
        card,
        max
    }

    private void Update()
    {
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
            _packs[i].AddComponent<TesPackObject>();
            // このキャッシュは必須
            int cash = i;
            TesPackObject obj = _packs[i].GetComponent<TesPackObject>();
            SaleObjectManager.instance.ProductExplantion(obj.GetSaleValue());
            SaleObjectManager.instance.AddProducts(_packs[i],
                () => { obj.ShopExplantion(); },
                () =>
                {

                    Debug.Log("パックを購入したよー");
                    // パックの購入時の処理を描く

                    GameObject domyy = _packs[cash];
                    SaleObjectManager.instance.Remove(domyy);


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
        int num = _packs.Count + 1;
        for (int i = 0; i < _packs.Count; i++)
        {
            // 始点に乗らないように+1する(前を増やす)
            float dis = (float)(i + 1) / num;
            _packs[i].transform.position = Vector3.Lerp(_leftTargetPos.position, _rightTargetPos.position, dis);
        }
    }

}
