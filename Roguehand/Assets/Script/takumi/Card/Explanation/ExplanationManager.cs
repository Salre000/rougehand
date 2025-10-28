using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 説明などに使うGUIを纏めるマネージャー
/// </summary>
public class ExplanationManager : MonoBehaviour
{
    [SerializeField] GameObject Prefab;

    /// <summary>
    /// 説明をまとめた配列
    /// </summary>
    private List<GameObject> _GameObjectPool = new List<GameObject>();
    [SerializeField]private List<GameObject> _explanationInterface = new List<GameObject>();
    [SerializeField]private List<Vector2> _offsets = new List<Vector2>();

    private readonly Vector2 DEFAULT_SIZE = new Vector2(300, 20);
    private readonly Vector2 defaultSizeMini = new Vector2(130, 90);

    private readonly float DEFAULT_HEIGHT = 200.0f;
    private readonly float ONE_BUFF_HEIGHT = 50.0f;

    private readonly float HALF = 2f;

    public Vector2 _uiSize { private get; set; }
    public Vector2 _uiSizeMini { private get; set; }

    /// <summary>
    /// instanceをシングルトンで生成
    /// </summary>
    public static ExplanationManager instance;

    public void Awake()
    {
        instance = this;
        CreateObject();
        _uiSize = DEFAULT_SIZE;
        _uiSizeMini = defaultSizeMini;

    }

    public void Update()
    {
        for (int i = 0; i < _explanationInterface.Count; i++)
        {

            Vector2 pos = Camera.main.WorldToScreenPoint(_explanationInterface[i].transform.position);

            pos.x -= Screen.width / HALF;
            pos.y -= Screen.height / HALF;

            pos.y -= _GameObjectPool[i].GetComponent<RectTransform>().sizeDelta.y * _offsets[i].y;
            pos.x -= _GameObjectPool[i].GetComponent<RectTransform>().sizeDelta.x * _offsets[i].x;

            _GameObjectPool[i].GetComponent<RectTransform>().localPosition = pos;



        }
    }

    /// <summary>
    /// 説明を描画可能に変更する関数
    /// </summary>
    /// <param name="traget"></param>
    /// <param name="explanationInterface"></param>
    /// <param name="buff"></param>
    public void AddExplanation(GameObject traget, ExplanationInterface explanationInterface, int[] buff, Vector2 offset)
    {

        if (explanationInterface == null) return;

        _explanationInterface.Add(traget);
        GameObject gameObject = GetGameObject();

        if (gameObject == null) return;

        gameObject.GetComponent<RectTransform>().sizeDelta = _uiSize;

        ExplanationObject explanationObject=gameObject.GetComponent<ExplanationObject>();

        explanationObject.GetTextName().text = explanationInterface.GetName();
        explanationObject.GetTextExplanation().text= GetLineString(explanationInterface.GetExplanation(), explanationInterface.GetExplanation2());
        // 諸事情ありこのタイミングで文字化けの可能性を作成
        explanationObject.GetTextRarityText().text = Extra.ErrorText(explanationInterface.GetTypes());

        explanationObject.GetTextRarityColor().color = explanationInterface.GetTypes().GetJokerRarityColor();

        int addCount = 0;
        for (int i = 0; i < buff.Length; i++)
        {
            if (StringMaster.instance.GetMaster(buff[i], true) == string.Empty) continue;
            addCount++;

            // 名前検索以外に非アクティブオブジェクトに干渉できない為に
            // 苦渋の選択で名前検索にしている
            // オブジェクト指定からの名前検索なので負荷は最小限になっている
            explanationObject.GetBuffColorIcon(i).gameObject.SetActive(true);
            explanationObject.GetBuffTextIcon(i).text = StringMaster.instance.GetMaster(buff[i]);
            explanationObject.GetBuffColorIcon(i).color = StringMaster.instance.GetMaster(buff[i], true).GetBuffColor();

            explanationObject.GetBuffText(i).transform.parent.gameObject.SetActive(true);

            explanationObject.GetBuffName(i).text = Extra.ErrorText(StringMaster.instance.GetMaster(buff[i]));
            // テキストボックス
            explanationObject.GetBuffText(i).text = StringMaster.instance.GetMaster(buff[i] + 50);
               
            explanationObject.GetBuffText(i).transform.parent.gameObject.GetComponent<RectTransform>().sizeDelta = _uiSizeMini;

        }

        //初期値の定数分移動に補正をかける
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(_uiSize.x, DEFAULT_HEIGHT + ONE_BUFF_HEIGHT * addCount);

        explanationObject.GetTextRarityColor().transform.parent.parent.localPosition = new Vector3(-((-_uiSizeMini.x+defaultSizeMini.x)+(-_uiSize.x+DEFAULT_SIZE.x)),gameObject.GetComponent<RectTransform>().sizeDelta.y/2, 0);

        _offsets.Add(offset);

        Vector2 size = explanationObject.GetBuffParent().GetComponent<RectTransform>().sizeDelta;

        size.x = _uiSizeMini.x * 2;

        explanationObject.GetBuffParent().GetComponent<RectTransform>().sizeDelta = size;
        explanationObject.GetBuffParent().GetComponent<RectTransform>().localPosition = new Vector2(-_uiSize.x/HALF, 0);

        _uiSize = DEFAULT_SIZE;
        _uiSizeMini = defaultSizeMini;

    }

    public void Remove()
    {
        //return;
        _explanationInterface.Clear();
        _offsets.Clear();
        for (int i = 0; i < 10; i++)
        {
            _GameObjectPool[i].GetComponent<RectTransform>().sizeDelta = DEFAULT_SIZE;

            _GameObjectPool[i].transform.GetChild(1).transform.Find("BuffColor1").gameObject.SetActive(false);
            _GameObjectPool[i].transform.GetChild(1).transform.Find("BuffColor2").gameObject.SetActive(false);
            _GameObjectPool[i].transform.GetChild(1).transform.Find("BuffColor3").gameObject.SetActive(false);
            _GameObjectPool[i].transform.GetChild(2).transform.Find("BuffUI1").GetComponent<RectTransform>().sizeDelta = defaultSizeMini;
            _GameObjectPool[i].transform.GetChild(2).transform.Find("BuffUI2").GetComponent<RectTransform>().sizeDelta = defaultSizeMini;
            _GameObjectPool[i].transform.GetChild(2).transform.Find("BuffUI3").GetComponent<RectTransform>().sizeDelta = defaultSizeMini;
            _GameObjectPool[i].transform.GetChild(2).transform.Find("BuffUI1").gameObject.SetActive(false);
            _GameObjectPool[i].transform.GetChild(2).transform.Find("BuffUI2").gameObject.SetActive(false);
            _GameObjectPool[i].transform.GetChild(2).transform.Find("BuffUI3").gameObject.SetActive(false);
            _GameObjectPool[i].SetActive(false);


        }


    }

    /// <summary>
    /// 引数のオブジェクトに関係するオブジェクトを返す関数
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public GameObject RelatedObject(GameObject gameObject)
    {
        int index = _GameObjectPool.IndexOf(gameObject);

        return index < 0 ? null : _explanationInterface[index];
    }

    /// <summary>
    /// オブジェクトプールを作成
    /// </summary>
    private void CreateObject()
    {
        //キャンバスを検索
        GameObject cav = GameObject.Find("UICanvas");

        GameObject Object = new GameObject("ExplanationObjects");
        Object.transform.parent = cav.transform;

        Object.transform.localPosition = Vector3.zero;

        for (int i = 0; i < 10; i++)
        {
            GameObject image = Instantiate(Prefab, Object.transform);

            image.SetActive(false);

            _GameObjectPool.Add(image);



        }



    }



    private GameObject GetGameObject()
    {
        for (int i = 0; i < _GameObjectPool.Count; i++)
        {
            if (_GameObjectPool[i].activeSelf) continue;

            _GameObjectPool[i].SetActive(true);

            return _GameObjectPool[i];


        }

        return null;


    }

    private string GetLineString(string _string1,string _string2)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append(_string1);
        stringBuilder.Append("\n");
        stringBuilder.Append(_string2);

        return stringBuilder.ToString();
    }

}
