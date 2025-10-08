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

            //このキャッシュは必須
            //この時点でnumの中身を決定する必要あり
            int num = i;

            Debug.Log("描画する");
            _saleInterfaces[i].SaleShow(_saleObject[i].transform.position, _saleValue[i],()=> 
                {
                    if (_saleObject.Count < num || _saleObject[num] == null) return;

                    GetType(_saleObject[num]);
                });

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



    /// <summary>
    /// 何の情報を開示しているかを返す関数
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    private GrabManager.status GetType(GameObject gameObject) 
    {
        GrabManager.status _status = GrabManager.status.None;

        //カードの可能性を判別
        CardObject cardObject = gameObject.GetComponent<CardObject>();

        if (cardObject != null) _status = GrabManager.status.Card;

        //ジョーカーの可能性を判別
        JokerObject jokerObject = gameObject.GetComponent<JokerObject>();

        if (jokerObject != null) _status = GrabManager.status.Joker;

        ItemObject itemObject = gameObject.GetComponent<ItemObject>();

        if (itemObject != null) _status = GrabManager.status.Item;



        switch (_status)
        {
            case GrabManager.status.Joker:


                JokerUtility.SaleAction(JokerObjectUtility.GetJokerIndex(jokerObject));

                JokerUtility.Remove(JokerObjectUtility.GetJokerIndex(jokerObject));



                break;
            case GrabManager.status.Item:
                ItemUtility.Remove(ItemUtility.GetItemIndex(itemObject));

                break;
        }


        return _status;

    }

    private void TypeSale(GrabManager.status status,GameObject gameObject) 
    {

        switch (status)
        {
            case GrabManager.status.Joker:

                break;
            case GrabManager.status.Item:

                break;
        }


    }

}
