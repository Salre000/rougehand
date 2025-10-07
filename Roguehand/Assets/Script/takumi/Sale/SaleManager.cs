using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SaleManager : MonoBehaviour
{
    /// <summary>
    /// 売買のinterfaceのリスト
    /// </summary>
    private List<SaleInterface> _saleInterfaces = new List<SaleInterface>();

    /// <summary>
    /// 売買の対象のオブジェクト
    /// </summary>
    private List<GameObject> _saleObject= new List<GameObject>();   

    /// <summary>
    /// 売買の値段
    /// </summary>
    private List<int> _saleValue=new List<int>();

    public void Awake()
    {
        SaleUtility.instance = this;

    }

    public void OnGUI()
    {
        for (int i = 0; i < _saleInterfaces.Count; i++)
        {
            if (_saleInterfaces[i] == null) continue;

            Debug.Log("描画する");
            _saleInterfaces[i].SaleShow(_saleObject[i].transform.position, _saleValue[i]);

        }

    }


    /// <summary>
    /// セールの追加する関数
    /// </summary>
    /// <param name="saleInterface"></param>
    /// <param name="saleObject"></param>
    /// <param name="saleValue"></param>
    public void SetSale(SaleInterface saleInterface,GameObject saleObject,int saleValue) 
    {
        _saleInterfaces.Add(saleInterface);
        _saleObject.Add(saleObject);
        _saleValue.Add(saleValue);
    }



    /// <summary>
    /// リストの全消去
    /// </summary>
    public void Clear() { _saleInterfaces.Clear(); _saleObject.Clear();_saleValue.Clear(); }

}
