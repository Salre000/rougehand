using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private List<bool> _isNotMove = new List<bool>();

    [SerializeField] private GameObject _valuePrefab;
    [SerializeField] private List<UISaleValueObject> _valuePool = new List<UISaleValueObject>();
    [SerializeField] Canvas shopCanvas;

    [SerializeField] private Transform _shopLeftPos;
    [SerializeField] private Transform _shopRightPos;
    [SerializeField] private Button _reroolButton;
    [SerializeField] private TextMeshProUGUI _reroolText;
    [SerializeField] private Button _packModeButton;

    [SerializeField, Header("デバック")] private bool _isPackMode = false;
    [SerializeField, Header("デバック")] private int _packSelectCount = 0;
    private readonly Vector3 _SHOP_ANGLE = new Vector3(-90, 0, 0);
    private readonly Vector3 UI_VALUE_OFFSET = new Vector3(0, 130, 0);
    float RENGE = 916;
    private readonly int START_REROOL = 3;
    private readonly int ADD_REROOL = 1;

    private int nowRerool = 0;

    /// <summary>
    /// ゲーム中に加わる処理のリスト
    /// </summary>
    private List<System.Action> dynamicAction = new();


    public void Awake()
    {
        instance = this;

        Initializ();
        nowRerool = START_REROOL;

        _reroolButton.onClick.AddListener(() =>
        {
            if (nowRerool > GameUtility.GetMyMoney()) return;

            GameUtility.SetMyMoney(GameUtility.GetMyMoney() - nowRerool);

            nowRerool += ADD_REROOL;

            ClearCard(); CreateRondom();
        });

        _packModeButton.onClick.AddListener(() => { _packSelectCount = 0; });

        _packModeButton.gameObject.SetActive(false);
    }
    public void Update()
    {
        IsShop();
        CheckPackModeEnd();
        SetShopObjectPos();
        ReroolSet();

        // 動的に実装される関数を実行
        for (int i = 0; i < dynamicAction.Count; i++) dynamicAction[i]();
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
        // パックモードのときは描画しない
        if (_isPackMode) { return; }

        ValueUISetActiveFalse();

        float renge = Vector2.Distance(_shopLeftPos.position, _shopRightPos.position) / (_isNotMove.GetCount(flag => !flag == true) + 1);

        int packCount = 0;

        for (int i = 0; i < _products.Count; i++)
        {
            if (!_products[i].activeSelf) continue;

            // UIを描画する
            UISaleValueObject uISale = GetValue();

            uISale.SetValue(_productsSaleValue[i]);

            uISale.transform.position = Camera.main.WorldToScreenPoint(_products[i].transform.position) + UI_VALUE_OFFSET;

            if (_isNotMove[i]) { packCount++; continue; }
            _products[i].transform.eulerAngles = _SHOP_ANGLE;
            _products[i].transform.position = _shopLeftPos.position + new Vector3(renge * (i + 1 - packCount), 0, 0);
        }
    }

    /// <summary>
    /// パックモードを終わるかどうかを確認する関数
    /// </summary>
    private void CheckPackModeEnd()
    {
        // パックモードでなければ返す
        if (!_isPackMode) return;


        if (_packSelectCount > 0) return;



        // パックモードを終了
        ChengePackMode(false);
        PackManager.instance.SetIsBuyPack(false);
        // パックモードの時に描画しているオブジェクトを削除
        for (int i = 0; i < _products.Count; i++)
        {
            if (!_products[i].activeSelf) continue;

            GameObject dommyObject = _products[i].gameObject;

            Remove(dommyObject);

            Destroy(dommyObject);

            i--;

        }
        SaleUtility.Claer();

        ALLActive();
    }

    private bool oneFlag = false;
    private void IsShop()
    {
        if (!ShopManager.instance.IsShop()) return;
        if (oneFlag) return;
        oneFlag = true;

        CreateRondom();

    }

    private void ReroolSet() 
    {

        _reroolText.text = nowRerool.ToString();
    }


    private void ValueUISetActiveFalse()
    {
        for (int i = 0; i < _valuePool.Count; i++)
            _valuePool[i].transform.gameObject.SetActive(false);
    }
    private UISaleValueObject GetValue()
    {
        for (int i = 0; i < _valuePool.Count; i++)
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

    public void AddProducts(GameObject product, System.Action action, System.Action buy, bool isPack = false)
    {
        _products.Add(product);

        _productsSaleShow.Add(action);

        _productsBuy.Add(buy);

        _isNotMove.Add(isPack);
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

        SaleUtility.Claer();

        //かなり非効率な事にをしているが他の方法を今の手持ちでは行えない
        _products.GetAction(product =>
        {
            if (Vector3.Distance(product.transform.position, gameObject.transform.position) < EPSILON) index = ID;
            ID++;
            return product;

        });

        if (index < 0) return;

        //BreakUtility.StartBreak(_products[index]);
        Destroy(_products[index]);
        _products.RemoveAt(index);
        _productsSaleShow.RemoveAt(index);
        _productsBuy.RemoveAt(index);
        _isNotMove.RemoveAt(index);
        _productsSaleValue.RemoveAt(index);
        int i = 0;
        _valuePool.GetAction(value =>
        {
            if (!value.gameObject.activeSelf) return value;

            if (i == index) value.gameObject.SetActive(false);

            i++;

            return value;

        });
    }

    public int GetIndex(GameObject gameObject) { return _products.IndexOf(gameObject); }

    public void SetSale(int index)
    {
        _productsSaleShow[index]();


    }

    /// <summary>
    /// 保存したリストを全て初期化
    /// </summary>
    public void Clear()
    {
        SaleUtility.Claer();
        _productsSaleValue.Clear();
        _productsSaleShow.Clear();
        for (int i = 0; i < _products.Count; i++)
            Destroy(_products[i]);
        _products.Clear();
        _isNotMove.Clear();
        _productsBuy.Clear();

        oneFlag = false;

        nowRerool = START_REROOL;
    }

    /// <summary>
    /// 保存したリストのパックを消さずに残りを消す
    /// </summary>
    public void ClearCard()
    {
        for (int i = 0; i < _isNotMove.Count; i++)
        {
            if (_isNotMove[i]) continue;
            _productsSaleValue.RemoveAt(i);
            _productsSaleShow.RemoveAt(i);
            _isNotMove.RemoveAt(i);

            GameObject gameObject = _products[i];
            _products.RemoveAt(i);
            Destroy(gameObject);

            i--;

        }


    }

    public System.Func<int> AddDynamicAction(System.Action action)
    {
        dynamicAction.Add(action);
        return () => dynamicAction.IndexOf(action);
    }

    public void RemoveDynamicAction(int index)
    {
        dynamicAction.RemoveAt(index);
    }


    public void ALLActive() { _products.GetAction(product => { product.SetActive(true); return product; }); }

    public void AllInactive() { _products.GetAction(product => { product.SetActive(false); return product; }); }

    public void ChengePackMode(bool flag)
    {
        _isPackMode = flag; 
        ValueUISetActiveFalse();
        _packModeButton.gameObject.SetActive(_isPackMode);

        JokerObjectUtility.JokerObjectALLAction(joker => { joker.gameObject.SetActive(!_isPackMode); return joker; });
        ItemUtility.ItemALLAction(item => { item.gameObject.SetActive(!_isPackMode); return item; });

    }

    public void SetPackSelectCount(int count) { _packSelectCount = count; }

    public void PackSekect(GameObject gameObject)
    {
        Remove(gameObject);

        _packSelectCount--;
    }

}
