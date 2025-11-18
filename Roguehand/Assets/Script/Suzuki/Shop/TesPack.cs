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
    [SerializeField] Transform _targetPos;
    private int ID;
    private int MAX_PACK=2;
    private List<GameObject> _packs = new();
    private bool _isInstantiate = false;

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
        if(_isInstantiate)return;
        // 置けるパック分生成
        for(int i = 0; i < MAX_PACK; i++)
        {
            // 生成
            _packs.Add(Instantiate(_pack,_targetPos));
            // クラスの付与
            _packs[i].AddComponent<TesPackObject>();
        }
        _isInstantiate=true;
    }

}
