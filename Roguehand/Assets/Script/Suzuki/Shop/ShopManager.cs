using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    // ショップ状態フラグ
    private bool _isShop = false;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }





    // ショップ状態を変更
    public void SetIsShop(bool flag) { _isShop = flag; }
    // ショップ状態を取得
    public bool IsShop() { return _isShop; }
}
