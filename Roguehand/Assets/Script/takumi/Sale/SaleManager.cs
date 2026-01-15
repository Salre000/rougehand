using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
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
    private List<GameObject> _saleObject = new List<GameObject>();

    /// <summary>
    /// 売買の値段
    /// </summary>
    private List<int> _saleValue = new List<int>();

    /// <summary>
    /// 購入なのか売却なのか
    /// </summary>
    private List<bool> _saletype = new List<bool>();

    GUIStyle style;

    public void Awake()
    {
        SaleUtility.instance = this;





    }



    public void OnGUI()
    {
        Debug.Log(_saleInterfaces.Count + "数");
        if (style == null)
        {
            style = new GUIStyle(GUI.skin.button);

            style.richText = true;

        }

        for (int i = 0; i < _saleInterfaces.Count; i++)
        {
            if (_saleInterfaces[i] == null) continue;

            //このキャッシュは必須
            //この時点でnumの中身を決定する必要あり
            int num = i;
            GameObject cashObject = _saleObject[num];


            if (_saletype[i])
            {
                _saleInterfaces[i].SaleShow(_saleObject[i].transform.position, _saleValue[i], () =>
                    {
                        if (_saleObject.Count < num || _saleObject[num] == null) return;

                        int index = _saleObject.IndexOf(cashObject);
                        GrabManager.status status= GetType(_saleObject[num]);

                        Remove(index);

                        //売却をした事をジョーカーに知らせる
                        JokerUtility.SetTraget(JokerActionUseEnum.JokerActionTarget.sale);

                        i=_saleInterfaces.Count;
                    });

            }
            else
            {
                _saleInterfaces[i].BuyShow(_saleObject[i].transform.position, _saleValue[i], () =>
                {
                    if (_saleObject.Count < num || _saleObject[num] == null) return;
                    int index = _saleObject.IndexOf(cashObject);

                    SaleObjectManager.instance.AddCardBuyCount();
                    //購入処理を書く
                    int saleIndex = SaleObjectManager.instance.GetIndex(cashObject);

                    SaleObjectManager.instance.IndexBuy(saleIndex);



                    i = _saleInterfaces.Count;


                });

            }
        }
    }


    /// <summary>
    /// セールの追加する関数
    /// </summary>
    /// <param name="saleInterface"></param>
    /// <param name="saleObject"></param>
    /// <param name="saleValue"></param>
    public void SetSale(SaleInterface saleInterface, GameObject saleObject, int saleValue, bool type)
    {
        _saleInterfaces.Add(saleInterface);
        _saleObject.Add(saleObject);
        _saleValue.Add(saleValue);
        _saletype.Add(type);

    }

    public GUIStyle GetStyle() { return style; }
    /// <summary>
    /// リストの全消去
    /// バックドア有
    /// バックドアがtrueのは問答無用で全消去
    /// </summary>
    public void Clear(bool backdoor = false)
    {
        //int index = 0;  
        //for (int i = 0; i < _saleInterfaces.Count; i++)
        //{
        //    // 必要無くなった
        // //   if (!backdoor && !_saletype[index] && ShopManager.instance.IsShop()) { index++; continue; }
        //    _saleInterfaces.RemoveAt(index);
        //    _saleObject.RemoveAt(index);
        //    _saleValue.RemoveAt(index);
        //    _saletype.RemoveAt(index);
        //}
        _saleInterfaces.Clear();
        _saleObject.Clear();
        _saleValue.Clear();
        _saletype.Clear();
    }



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

    /// <summary>
    /// 購入した時の処理
    /// </summary>
    /// <param name="gameObject"></param>
    private void Buy(GameObject gameObject)
    {

    }

    private void Remove(int index)
    {
        _saleInterfaces.RemoveAt(index);
        _saleObject.RemoveAt(index);
        _saleValue.RemoveAt(index);
        _saletype.RemoveAt(index);

    }

}
