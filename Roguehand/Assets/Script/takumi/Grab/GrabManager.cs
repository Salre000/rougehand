using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GrabManager : MonoBehaviour
{

    public static GrabManager instance;

    private bool grabFlag = true;

    /// <summary>
    /// メインカメラのオブジェクト
    /// </summary>
    private GameObject _camera;

    /// <summary>
    /// 掴んでいるオブジェクトのID
    /// </summary>
    [SerializeField] private int _grabID = -1;

    private System.Action continuationAction = null;

    public enum status
    {
        None,
        //ここから下はつかまれている対象
        Card,
        Joker,
        Item,
        // ショップの物
        Sale,
        //デッキ
        Deck
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
        instance = this;
    }

    private void Update()
    {
        if (!grabFlag) return;

        // リザルト画面が開いていたら触れられなくする
        if (ResultUIManager.Instance.resultFlag) return;

        // ランの詳細が見えるときは触れられなくする
        if (RunDetailsManager.instance.IsOpen()) return;

        // プレイ途中でカードなどに触れなくする
        if (CardObjectUtility.IsPlaying()) return;

        if (continuationAction != null) continuationAction();

        _time += Time.deltaTime;
        MouseOver();
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
        //マウスの位置にrayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GetObjectType(hit.transform.gameObject);


        }

    }

    GameObject mouseOverObject;
    private void MouseOver()
    {


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (mouseOverObject != hit.transform.gameObject) 
            {
                ExplanationManager.instance.Remove(); 
                mouseOverObject = hit.transform.gameObject; 
            }
            else return;

            SetGrabID(hit.transform.gameObject);

            switch (_status)
            {
                case status.Card:
                    CardObjectUtility.ShowExplanation(CardManager.instance.GetHand()[_grabID], _grabID);

                    break;
                case status.Joker:
                    JokerUtility.ShowExplanation(_grabID);
                    break;
                case status.Item:
                    ItemUtility.ShowExplanation(_grabID);
                    break;
                case status.None:

                    break;

            }

            _status = status.None;
            _grabID = -1;


        }
        else
        {
            
                ExplanationManager.instance.Remove(); 
        }
    }


    /// <summary>
    /// 離す関数
    /// </summary>
    private void Separate()
    {

        if (Input.GetMouseButton(0)) return;

        if (_status == status.None) return;

        SaleUtility.Claer();

        //マウスの位置にrayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            SetGrabID(hit.transform.gameObject);


        }

        if (_grabID < 0) _status = status.None;

        ExplanationManager.instance.Remove();
        switch (_status)
        {
            case status.Card:
                CardObjectUtility.GrabChenge(_grabID, false);

                break;
            case status.Joker:
                JokerObjectUtility.GrabChange(_grabID, false);
                break;
            case status.Item:
                ItemUtility.GrabChange(_grabID, false);
                break;

        }


        if (_time < 1)
        {
            switch (_status)
            {
                case status.None:
                    break;
                case status.Card:
                    CardManager.instance.SetIsSelect(_grabID);
                    break;
                case status.Joker:
                    JokerUtility.SetSale(_grabID);
                    break;
                case status.Item:
                    ItemUtility.SetSale(_grabID);
                    break;
                case status.Sale:
                    SaleObjectManager.instance.SetSale(_grabID);
                    continuationAction = () =>
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            if (SaleObjectManager.instance.GetIndex(hit.transform.gameObject) < 0)
                            {
                                continuationAction = null;
                                ExplanationManager.instance.Remove();
                            }

                        }
                        else
                        {
                            continuationAction = null;
                            ExplanationManager.instance.Remove();
                        }
                    };
                    break;

            }

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
                // カードの情報をUIとして描画する

                if (gameObject.transform.localEulerAngles.y < 300) return;
                CardObjectUtility.ShowExplanation(CardManager.instance.GetHand()[_grabID], _grabID);

                break;
            case status.Joker:
                JokerObjectUtility.GrabChange(_grabID, true);
                JokerUtility.ShowExplanation(_grabID);
                break;
            case status.Item:
                ItemUtility.GrabChange(_grabID, true);
                ItemUtility.ShowExplanation(_grabID);
                break;
            case status.None:

                break;

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

        int saleIndex = SaleObjectManager.instance.GetIndex(gameObject);

        if (!(saleIndex < 0)) _status = status.Sale;

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
            case status.Sale:
                _grabID = saleIndex;
                break;
            default: break;
        }
    }

    public void SetGrabFlag(bool flag) { grabFlag = flag; }
    public bool GetGrabFlag() { return grabFlag; }

}