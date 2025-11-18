using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static ScriptCountNumber;
/// <summary>
/// ショップ時の購入売却などを行うクラス
/// </summary>
public class SaleObjectManager : MonoBehaviour
{
    private enum shoptype
    {
        joker,
        item,
        trump,
        max
    }

    /// <summary>
    /// もうこれでいいや
    /// </summary>
    public static SaleObjectManager instance;
    /// <summary>
    /// 購入可能なオブジェクトのリスト
    /// </summary>
    [SerializeField] private List<GameObject> _products = new List<GameObject>();
    [SerializeField] private List<System.Action> _productsSaleShow = new List<System.Action>();
    [SerializeField] private List<float> _productsSaleValue = new List<float>();
    [SerializeField] private List<System.Action> _productsBuy = new List<System.Action>();
    [SerializeField] private List<bool> _isPack = new List<bool>();

    [SerializeField] private GameObject _valuePrefab;
    [SerializeField]private List<UISaleValueObject>  _valuePool = new List<UISaleValueObject>();
    [SerializeField] Canvas shopCanvas;

    [SerializeField] private Transform _shopLeftPos;
    [SerializeField] private Transform _shopRightPos;
    [SerializeField] private Button _reroolButton;
    private readonly Vector3 _SHOP_ANGLE = new Vector3(-90, 0, 0);
    private readonly Vector3 UI_VALUE_OFFSET = new Vector3(0,130,0);
    float RENGE = 916;

    public void Awake()
    {
        instance = this;

        Initializ();

        _reroolButton.onClick.AddListener(()=> { ClearCard(); CreateRondom(); });
    }
    public void Update()
    {
        SetShopObjectPos();
    }

    private void Initializ() 
    {
        GameObject pollParent = new GameObject("UIValueParent");
        pollParent.transform.parent = shopCanvas.transform;
        for (int i = 0; i < 10; i++) 
        {
            _valuePool.Add(Instantiate(_valuePrefab, pollParent.transform).GetComponent<UISaleValueObject>());
        }
        ValueUISetActiveFalse();
    }
    private void SetShopObjectPos()
    {

        ValueUISetActiveFalse();

        float renge = RENGE / (_isPack.GetCount(flag=>!flag) + 1);

        int packCount = 0;

        for (int i = 0; i < _products.Count; i++)
        {


            // UIを描画する
            UISaleValueObject uISale = GetValue();

            uISale.SetValue(_productsSaleValue[i]);

            uISale.transform.position = Camera.main.WorldToScreenPoint(_products[i].transform.position) + UI_VALUE_OFFSET;

            if (_isPack[i]) { packCount++; continue; }
            _products[i].transform.eulerAngles = _SHOP_ANGLE;
            _products[i].transform.position = _shopLeftPos.position + new Vector3(renge * (i + 1- packCount), 0, 0);
        }
    }

    private void ValueUISetActiveFalse() 
    {
        for(int i=0;i<_valuePool.Count;i++)
            _valuePool[i].transform.gameObject.SetActive(false);
    }
    private UISaleValueObject GetValue() 
    {
        for(int i = 0; i < _valuePool.Count; i++) 
        {
            if (_valuePool[i].gameObject.activeSelf) continue;
            _valuePool[i].gameObject.SetActive(true);
            return _valuePool[i];
        }

        return null;
    }

    /// <summary>
    /// ランダムに引数の数だけショップにオブジェクトを並べる
    /// </summary>
    /// <param name="count"></param>
    public void CreateRondom(int count = 2)
    {
        for (int i = 0; i < count; i++)
        {
            shoptype type = (shoptype)Random.Range(0, (int)shoptype.max);
            switch (type)
            {
                case shoptype.joker:
                    CreateJoker();
                    break;
                case shoptype.item:
                    CreateItem();
                    break;
                case shoptype.trump:
                    // バウンチャーを取っていなければ戻す
                    if (true) { i--; continue; }
                    // トランプの生成

                    break;
            }


        }


        // 位置を修正
        SetShopObjectPos();

    }

    public void CreateItem(int ID = -1)
    {
        if (ID < 0) ID = Random.Range(0, (int)ALLItem.ALLItemEnum._MAX);

        ItemUtility.ShopItem(() => ALLItem.GetItem((ALLItem.ALLItemEnum)ID));


    }

    public void CreateJoker(int ID = -1)
    {
        if (ID < 0) ID = Random.Range(0, (int)ALLJoker._allJokerEnum.MAX);

        JokerUtility.ShopJoker(() => ALLJoker.GetJoker((ALLJoker._allJokerEnum)ID));

    }

    public void AddProducts(GameObject product, System.Action action,System.Action buy,bool isPack=false)
    {
        _products.Add(product);

        _productsSaleShow.Add(action);

        _productsBuy.Add(buy);

        _isPack.Add(isPack);
    }

    public void ProductExplantion(float value) 
    {
        _productsSaleValue.Add(value);
    }

    public void IndexBuy(int index) 
    { 
        _productsBuy[index](); 
    }

    public void Remove(GameObject gameObject) 
    {
        int index = -1;//_products.IndexOf(gameObject);
        int ID = 0; 

        //かなり非効率な事にをしているが他の方法を今の手持ちでは行えない
        _products.GetAction(product =>
        {
            if (Vector3.Distance(product.transform.position, gameObject.transform.position) < EPSILON) index = ID;
            ID++;
            return product;

        });

        if (index < 0) return;

        BreakUtility.StartBreak(_products[index]);
        Destroy(_products[index]);
        _products.RemoveAt(index);
        _productsSaleShow.RemoveAt(index);
        _productsBuy.RemoveAt(index);
        _isPack.RemoveAt(index);

        int i = 0;
        _valuePool.GetAction(value =>
        {
            if (!value.gameObject.activeSelf) return value;

            if (i == index) value.gameObject.SetActive(false);

            i++;

            return value;

        });
    }

    public int GetIndex(GameObject gameObject) {  return _products.IndexOf(gameObject); }

    public void SetSale(int index)
    {
        _productsSaleShow[index]();


    }

    /// <summary>
    /// 保存したリストを全て初期化
    /// </summary>
    public void Clear() 
    {
        
        _productsSaleValue.Clear();
        _productsSaleShow.Clear();
        for (int i = 0; i < _products.Count; i++)
            Destroy(_products[i]);
        _products.Clear();
        _isPack.Clear();
    }

    /// <summary>
    /// 保存したリストのパックを消さずに残りを消す
    /// </summary>
    public void ClearCard() 
    {
        for(int i = 0; i < _isPack.Count; i++) 
        {
            if (_isPack[i]) continue;
            _productsSaleValue.RemoveAt(i);
            _productsSaleShow.RemoveAt(i);
            _isPack.RemoveAt(i);

            GameObject gameObject = _products[i];
            _products.RemoveAt(i);
            Destroy(gameObject);

            i--;

        }


    }

}
