using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
    private List<GameObject> _explanationInterface = new List<GameObject>();

    /// <summary>
    /// instanceをシングルトンで生成
    /// </summary>
    public static ExplanationManager instance;

    public void Awake()
    {
        instance = this;
        CreateObject();
    }

    public void Update()
    {
        for (int i = 0; i < _explanationInterface.Count; i++)
        {

            Vector2 pos = Camera.main.WorldToScreenPoint(_explanationInterface[i].transform.position);

            pos.x -= Screen.width / 2f;
            pos.y -= Screen.height / 2f;

            pos.y -= _GameObjectPool[i].GetComponent<RectTransform>().sizeDelta.y;

            _GameObjectPool[i].GetComponent<RectTransform>().localPosition = pos;



        }
    }

    public void AddExplanation(GameObject traget, ExplanationInterface explanationInterface, int[] buff)
    {

        if (explanationInterface == null) return;

        _explanationInterface.Add(traget);
        GameObject gameObject = GetGameObject();

        if (gameObject == null) return;


        gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = explanationInterface.GetName();
        gameObject.transform.GetChild(1).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = explanationInterface.GetExplanation();

        gameObject.transform.GetChild(1).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = explanationInterface.GetExplanation2();
        // 諸事情ありこのタイミングで文字化けの可能性を作成
        gameObject.transform.GetChild(1).transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Extra.ErrorText(explanationInterface.GetTypes());

        gameObject.transform.GetChild(1).transform.GetChild(3).GetComponent<Image>().color = explanationInterface.GetTypes().GetJokerRarityColor();

        int addCount = 0;
        for (int i = 0; i < buff.Length; i++)
        {
            if (StringMaster.instance.GetMaster(buff[i],true) == string.Empty) continue;
            addCount++;
            gameObject.transform.GetChild(1).transform.Find("BuffColor" + addCount.ToString()).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).transform.Find("BuffColor" + addCount.ToString()).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = StringMaster.instance.GetMaster(buff[i]);
            gameObject.transform.GetChild(1).transform.Find("BuffColor" + addCount.ToString()).GetComponent<Image>().color = StringMaster.instance.GetMaster(buff[i], true).GetBuffColor();

            GameObject explanation = gameObject.transform.GetChild(2).transform.Find("BuffUI" + addCount.ToString()).gameObject;
            explanation.SetActive(true);

            explanation.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Extra.ErrorText(StringMaster.instance.GetMaster(buff[i]));
            explanation.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = StringMaster.instance.GetMaster(buff[i]+50);


        }

        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 200 + 50 * addCount);


    }

    public void Remove()
    {

        _explanationInterface.Clear();

        for (int i = 0; i < 10; i++)
        {

            _GameObjectPool[i].transform.GetChild(1).transform.Find("BuffColor1").gameObject.SetActive(false);
            _GameObjectPool[i].transform.GetChild(1).transform.Find("BuffColor2").gameObject.SetActive(false);
            _GameObjectPool[i].transform.GetChild(1).transform.Find("BuffColor3").gameObject.SetActive(false);
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
        GameObject cav = GameObject.Find("RunCanvas");

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

}
