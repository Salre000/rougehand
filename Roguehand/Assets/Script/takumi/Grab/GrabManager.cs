using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GrabManager : MonoBehaviour
{

    /// <summary>
    /// メインカメラのオブジェクト
    /// </summary>
    private GameObject _camera;

    /// <summary>
    /// 掴んでいるオブジェクトのID
    /// </summary>
    [SerializeField] private int _grabID = -1;

    private enum status
    {
        None,
        //ここから下はつかまれている対象
        Card,
        Joker,
        Item
    }

    /// <summary>
    /// 現在の状態
    /// </summary>
    private status _status = status.None;

    /// <summary>
    /// 時間計測をする変数
    /// </summary>
    private float _time = 0;


    private void Awake()
    {
        //カメラのオブジェクトを取得
        _camera = Camera.main.gameObject;
    }

    private void Update()
    {

        _time += Time.deltaTime;

        Grab();
        Separate();


    }

    /// <summary>
    /// 掴み行動の関数
    /// </summary>
    private void Grab()
    {
        if (_status != status.None) return;
        //クリックしていないと返す
        if (!Input.GetMouseButton(0)) return;
        SaleUtility.Claer();
        //マウスの位置にrayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GetObjectType(hit.transform.gameObject);
        }

    }


    /// <summary>
    /// 離す関数
    /// </summary>
    private void Separate()
    {

        if (Input.GetMouseButton(0)) return;

        if (_status == status.None) return;

        //SaleUtility.Claer();

        //マウスの位置にrayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            SetGrabID(hit.transform.gameObject);


        }


        switch (_status)
        {
            case status.Card:
                CardObjectUtility.GrabChenge(_grabID, false);
                if (_time > 1) break;

                CardManager.instance.SetIsSelect(_grabID);

                break;
            case status.Joker:
                JokerObjectUtility.GrabChange(_grabID, false);
                break;
            case status.Item:
                ItemUtility.GrabChange(_grabID, false);
                break;
        }


        if (_status != status.Card&&_time<1) 
        {
            if(_status==status.Item)ItemUtility.SetSale(_grabID);
            if(_status==status.Joker)JokerUtility.SetSale(_grabID);

        }
        _time = 0;
        _status = status.None;

        _grabID = -1;

    }
    /// <summary>
    /// rayの対象のオブジェクトの種類を判別
    /// </summary>
    /// <param name="gameObject"></param>
    private void GetObjectType(GameObject gameObject)
    {
        
        SetGrabID(gameObject);

        //何かしらの可能性でIDを取得出来なかった時にリセット
        if (_grabID == -1) { _status = status.None; }

        switch (_status)
        {
            case status.Card:
                CardObjectUtility.GrabChenge(_grabID, true);
                break;
            case status.Joker:
                JokerObjectUtility.GrabChange(_grabID, true);
                break;
            case status.Item:
                ItemUtility.GrabChange(_grabID, true);
                break;
            case status.None:break;
        }

    }

    private void SetGrabID(GameObject gameObject)
    {
        _time = 0;
        //カードの可能性を判別
        CardObject cardObject = gameObject.GetComponent<CardObject>();

        if (cardObject != null) _status = status.Card;

        //ジョーカーの可能性を判別
        JokerObject jokerObject = gameObject.GetComponent<JokerObject>();

        if (jokerObject != null) _status = status.Joker;

        ItemObject itemObject = gameObject.GetComponent<ItemObject>();

        if (itemObject != null) _status = status.Item;


        //掴んだオブジェクトのIDを取得
        switch (_status)
        {
            case status.Card:
                _grabID = CardObjectUtility.GetCardObjectIndex(cardObject);
                break;
            case status.Joker:
                _grabID = JokerObjectUtility.GetJokerIndex(jokerObject);
                break;
            case status.Item:
                _grabID = ItemUtility.GetItemIndex(itemObject);
                break;
        }
    }

}