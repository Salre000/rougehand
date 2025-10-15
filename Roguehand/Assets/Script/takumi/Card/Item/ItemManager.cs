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

    /// <summary>
    /// アイテムの本体
    /// </summary>
    private List<ItemBase> _itemList = new List<ItemBase>();

    /// <summary>
    /// アイテムのオブジェクト
    /// </summary>
    [SerializeField]private List<ItemObject> _itemObjectList = new List<ItemObject>();

    /// <summary>
    /// アイテムの左端の座標
    /// </summary>
    [SerializeField]private Transform _leftTransform;
    /// <summary>
    /// アイテムの右端の座標
    /// </summary>
    [SerializeField] private Transform _rightTransform;

    private bool _isGrab = false;
    private int _isGrabID = -1;

    public void Awake()
    {
        ItemUtility.instance = this;
    }

    public void Update()
    {
        SetPosition();
        CheckOrder();
    }


    /// <summary>
    /// アイテムを追加する関数
    /// </summary>
    /// <param name="ID"></param>
    public void AddItem(int ID)
    {
        _itemList.Add(ALLItem.GetItem((ALLItem.ALLItemEnum)ID));
        _itemObjectList.Add(Instantiate(_prefab, transform).AddComponent<ItemObject>());
    }
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


    public void ChengeOrder(int lostID,int NextID) 
    {

        _itemObjectList= Extra.ChengeOrder(_itemObjectList, lostID, NextID);
        _itemList= Extra.ChengeOrder(_itemList, lostID, NextID);

        for(int i=0;i<_itemObjectList.Count;i++)
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

        float renge = Vector3.Distance(_leftTransform.position, _rightTransform.position)/(_itemObjectList.Count+1);




        for (int i = 0; i < _itemObjectList.Count; i++)
            _itemObjectList[i].MovePos(_leftTransform.position+new Vector3(renge*(i+1), 0,0));


    }



}
