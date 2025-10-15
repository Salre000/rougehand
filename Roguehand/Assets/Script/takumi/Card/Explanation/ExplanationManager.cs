using System.Collections;
using System.Collections.Generic;
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
    private List<GameObject> _GameObjectPool=new List<GameObject>();
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
        for(int i = 0; i < _explanationInterface.Count; i++) 
        {

            Vector2 pos = Camera.main.WorldToScreenPoint(_explanationInterface[i].transform.position);

            pos.x -= Screen.width / 2f;
            pos.y -= Screen.height / 2f;

            pos.y -= 200;

            _GameObjectPool[i].GetComponent<RectTransform>().localPosition = pos;



        }
    }

    public void AddExplanation(GameObject traget, ExplanationInterface explanationInterface) 
    {

        if (explanationInterface == null) return;

        GameObject gameObject = GetGameObject();

        if (gameObject == null) return;

        _explanationInterface.Add(traget);

        gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = explanationInterface.GetName();
        gameObject.transform.GetChild(1).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = explanationInterface.GetExplanation();
        gameObject.transform.GetChild(1).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = explanationInterface.GetExplanation2();
        gameObject.transform.GetChild(1).transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = explanationInterface.GetTypes();

        gameObject.transform.GetChild(1).transform.GetChild(3).GetComponent<Image>().color= explanationInterface.GetTypes().GetJokerRarityColor();
    }

    public void Remove() 
    {

        _explanationInterface.Clear();

        for(int i = 0; i < 10; i++) 
        {
            _GameObjectPool[i].SetActive(false);

        }


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

        for(int i = 0; i < 10; i++) 
        {
            GameObject image=Instantiate(Prefab, Object.transform);

            image.SetActive(false);

            _GameObjectPool.Add(image);



        }



    }



    private GameObject GetGameObject() 
    {
        for(int i = 0; i < _GameObjectPool.Count; i++) 
        {
            if (_GameObjectPool[i].activeSelf) continue;

            _GameObjectPool[i].SetActive(true);

            return _GameObjectPool[i];


        }

        return null;


    }

}
