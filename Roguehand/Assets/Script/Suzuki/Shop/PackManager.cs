using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackManager : MonoBehaviour
{
    public static PackManager instance;

    private bool _isBuyPack = false;
    private bool _isArrival = false;
    private GameObject _pickPack;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // パックを購入したか
    public void SetIsBuyPack(bool flag) { _isBuyPack = flag; }
    public bool IsBuyPack() { return _isBuyPack; }
    // パックが目的地に到着したか
    public void SetIsArrival(bool flag) { _isArrival = flag; }
    public bool IsArrival() { return _isArrival; }
    // 購入したパックオブジェクト
    public void SetPickPack(GameObject transform) { _pickPack = transform; }
    public GameObject GetPickPack() { return _pickPack; }

}
