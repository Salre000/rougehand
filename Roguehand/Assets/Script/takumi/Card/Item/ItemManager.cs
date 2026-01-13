using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

/// <summary>
/// アイテムを纏めるクラス
/// </summary>
public class ItemManager : MonoBehaviour
{

    [SerializeField] private GameObject _prefab;

    private readonly Vector3 _SHOP_ANGLE = new Vector3(-90, 0, 0);
    private readonly Vector3 _NORMAL_ANGLE = new Vector3(0, 0, 0);


    /// <summary>
    /// アイテムの本体
    /// </summary>
    private List<ItemBase> _itemList = new List<ItemBase>();

    /// <summary>
    /// アイテムのオブジェクト
    /// </summary>
    [SerializeField] private List<ItemObject> _itemObjectList = new List<ItemObject>();

    /// <summary>
    /// アイテムの左端の座標
    /// </summary>
    [SerializeField] private Transform _leftTransform;
    /// <summary>
    /// アイテムの右端の座標
    /// </summary>
    [SerializeField] private Transform _rightTransform;
    /// <summary>
    /// ショップ時のアイテムの左端の座標
    /// </summary>
    [SerializeField] private Transform _leftShopTransform;
    /// <summary>
    /// ショップ時のアイテムの右端の座標
    /// </summary>
    [SerializeField] private Transform _rightShopTransform;

    private readonly int ITEM_MAX_COUNT = 2;

    private bool _isGrab = false;
    private int _isGrabID = -1;

    public void Awake()
    {
        ItemUtility.instance = this;
    }

    public void Update()
    {
        // ショップ時の処理
        if (ShopManager.instance.IsShop())
        {
            SetShopPosition();
            CheckOrder();
            return;
        }

        SetPosition();
        CheckOrder();
    }

    public void ItemALLAction(System.Func<ItemObject,ItemObject> func) { _itemObjectList.GetAction(func); }

    /// <summary>
    /// 何かしらの効果によってアイテムの最大値を変更する値
    /// </summary>
    private int negativeItemCounter = 0;
    /// <summary>
    /// アイテムを追加する関数
    /// </summary>
    /// <param name="ID"></param>
    public void AddItem(int ID)
    {

        if (_itemList.Count >= ITEM_MAX_COUNT+ negativeItemCounter) return;


        _itemList.Add(ALLItem.GetItem((ALLItem.ALLItemEnum)ID));
        _itemList[_itemList.Count-1].Initializ();

        _itemObjectList.Add(Instantiate(_prefab, transform).AddComponent<ItemObject>());
    }
    public void SetItemID(int ID) { _itemList[_itemList.Count - 1].SetItemID(ID); }
    public void Remove(ItemBase itemBase)
    {

        bool flag = _itemList.Contains(itemBase);

        int index = _itemList.FindIndex(item => item == itemBase);

        _itemList.Remove(itemBase);

        //　オブジェクトをキャッシュ
        GameObject gameObject = _itemObjectList[index].gameObject;

        // オブジェクトを配列から消去
        _itemObjectList.RemoveAt(index);

        BreakUtility.StartBreak(gameObject);

    }
    public void Remove(int itemBase)
    {

        _itemList.RemoveAt(itemBase);

        //　オブジェクトをキャッシュ
        GameObject gameObject = _itemObjectList[itemBase].gameObject;

        // オブジェクトを配列から消去
        _itemObjectList.RemoveAt(itemBase);

        BreakUtility.StartBreak(gameObject);
        Destroy(gameObject);

    }

    public bool ItemAddCheck() {  return _itemList.Count<ITEM_MAX_COUNT; }

    public void ChengeOrder(int lostID, int NextID)
    {

        _itemObjectList = Extra.ChengeOrder(_itemObjectList, lostID, NextID);
        _itemList = Extra.ChengeOrder(_itemList, lostID, NextID);

        for (int i = 0; i < _itemObjectList.Count; i++)
            _itemObjectList[i].ResetTime();

    }
    public void GrabChange(int ID, bool flag)
    {
        _isGrab = flag;
        _isGrabID = ID;
        _itemObjectList[ID].SetGrab(flag);

    }

    public void SetSale(int ID)
    {

        SaleUtility.SetSale(_itemList[ID], _itemObjectList[ID].gameObject, _itemList[ID].ReturnMoney());



    }

    public void ShowExplanation(int ID)
    {
        int[] test = new int[0];
        ExplanationManager.instance.AddExplanation(_itemObjectList[ID].gameObject, _itemList[ID], test, new Vector2(0, 1));



    }
    public void ShowExplanation(GameObject gameObject,ItemBase itemBase,Vector2 offset)
    {
        int[] test = new int[0];
        ExplanationManager.instance.AddExplanation(gameObject, itemBase, test, offset);



    }

    public void ShopItemAdd(System.Func<ItemBase> func=null) 
    {
        if (func == null) func = () => ALLItem.GetItem((ALLItem.ALLItemEnum)UnityEngine.Random.Range(0, (int)ALLItem.ALLItemEnum._MAX));

        ItemBase item = func();

        GameObject saleObjecet = Instantiate(_prefab, transform);
        ItemObject itemObject = saleObjecet.AddComponent<ItemObject>();

        item.Initializ();

        //オブジェクトの物理演算を停止
        saleObjecet.GetComponent<Rigidbody>().isKinematic = true;



        SaleObjectManager.instance.ProductExplantion(item.ReturnMoney());
        SaleObjectManager.instance.AddProducts(saleObjecet,
            () => { ShopExplamtion(saleObjecet, item); },
            () =>
            {
                AddItem(item.GetID()<(int)ConstellationItem.ConstellationType.MAX?0:item.GetID()- (int)ConstellationItem.ConstellationType.MAX);
                _itemList[_itemList.Count-1].SetItemID(item.GetID());
                GameObject domyy = saleObjecet;
                SaleObjectManager.instance.Remove(domyy);


            }

            );


    }

    public List<ItemBase> GetItemBases() { return _itemList; }

    private readonly Vector2 SHOP_UI_OFFSET = new Vector2(1, 0);
    private readonly int[] SHOP_DOMMY_BUFF = new int[0];

    private void ShopExplamtion(GameObject gameObject, ItemBase itembase)
    {
        SaleUtility.SetSale(itembase, gameObject, itembase.ReturnMoney(), false);



        ExplanationManager.instance.AddExplanation(gameObject, itembase, SHOP_DOMMY_BUFF, SHOP_UI_OFFSET);

    }


    private void CheckOrder()
    {

        if (!_isGrab) return;


        //ジョーカー同士の距離
        float renge = Vector3.Distance(_leftTransform.transform.position, _rightTransform.transform.position) / (_itemObjectList.Count + 1);


        float Cardrenge = (_leftTransform.transform.position.x + renge * (_isGrabID + 1)) - _itemObjectList[_isGrabID].transform.position.x;


        //横方向への移動距離が小さかったら順番の変更を加えない
        if (Mathf.Abs(Cardrenge) + 30 < renge) return;

        //移動方向を調整
        int count = 1;
        if (Cardrenge > 1) count = -1;

        if (_isGrabID + count >= _itemObjectList.Count || _isGrabID + count < 0) return;

        //ジョーカーの順番を入れ替える関数を呼ぶ
        ItemUtility.ChengeOrder(_isGrabID, _isGrabID + count);

        _isGrabID = _isGrabID + count;





    }
    public int GetItemIndex(ItemObject itemObject)
    {
        return _itemObjectList.FindIndex(item => item == itemObject);
    }

    /// <summary>
    /// アイテムの位置を元の場所に戻す処理
    /// </summary>
    private void SetPosition()
    {

        float renge = Vector3.Distance(_leftTransform.position, _rightTransform.position) / (_itemObjectList.Count + 1);




        for (int i = 0; i < _itemObjectList.Count; i++) 
        {
            _itemObjectList[i].MovePos(_leftTransform.position + new Vector3(renge * (i + 1), 0, 0));

            _itemObjectList[i].transform.eulerAngles = _NORMAL_ANGLE;
        }

    }
    /// <summary>
    /// ショップのときのアイテムの位置を元の場所に戻す処理
    /// </summary>
    private void SetShopPosition()
    {

        float renge = Vector3.Distance(_leftShopTransform.position, _rightShopTransform.position) / (_itemObjectList.Count + 1);




        for (int i = 0; i < _itemObjectList.Count; i++) 
        {
            _itemObjectList[i].MovePos(_leftShopTransform.position + new Vector3(renge * (i + 1), 0, 0));

            _itemObjectList[i].transform.eulerAngles = _SHOP_ANGLE;

        }

    }



}
