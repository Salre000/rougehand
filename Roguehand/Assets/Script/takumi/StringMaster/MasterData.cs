using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;

public class MasterData : MonoBehaviour
{
    public static MasterData instance;

    [SerializeField] TextMeshProUGUI proUGUI;

    /// <summary>
    /// マスターから引き出した文字列の格納先
    /// </summary>
    private Dictionary<int, string> _masters = new Dictionary<int, string>();

    public void Awake()
    {
        instance = this;
        // シーンの移行で破壊されない用に変更
        DontDestroyOnLoad(this.gameObject);

        Lood();

    }

    public void Update()
    {
    }

    /// <summary>
    /// マスターから引き出す関数
    /// </summary>
    private void Lood()
    {
        List<string> list = new List<string>();

        //読み込んだCSVファイルを格納
        List<string[]> csvDatas = new List<string[]>();

        //CSVファイルの行数を格納
        int height = 0;

        //ファイルパスとファイルの名前を繋げる
        StringBuilder builder = new StringBuilder();

        builder.Clear();
        builder.Append("takumi/StringMaster");

        //繋げたファイルパスを使いファイルのロードを行う
        TextAsset[] textAsset = Resources.LoadAll<TextAsset>(builder.ToString());

        for (int i = 0; i < textAsset.Length; i++)
        {

            //読み込んだテキストをString型にして格納
            StringReader reader = new StringReader(textAsset[i].text);

            while (reader.Peek() > -1)
            {
                string line = reader.ReadLine();
                // ,で区切ってCSVに格納
                csvDatas.Add(line.Split(','));
                height++; // 行数加算
            }

            for (int j = 0; j < csvDatas.Count; j++)
            {
                if (csvDatas[j][0] == String.Empty) continue;
                _masters.Add(int.Parse(csvDatas[j][0]), csvDatas[j][1]);
            }
            csvDatas.Clear();
        }
        GetStringMaster(-1);

    }


    /// <summary>
    /// マスターからID指定で文字列の取得
    /// 文字化けを仕込むならばこの行に追加をする
    /// </summary>
    /// <param name="ID"><文字ID/param>
    /// <returns><IDに対応した文字列/returns>
    public string GetStringMaster(int ID, bool backDoor = false)
    {
        if (ID == -1) 
        {
            int stop = 0;
        }


        string value = string.Empty;
        _masters.TryGetValue(ID, out value);

        if (value == null) value = string.Empty;

        return value.ErrorText(backDoor);

    }
    public void SetStringMaster(int ID,string value) 
    {
        _masters[ID]=value;
    }
    public void AddStringMaster(int key,string value) 
    {
        _masters.Add(key,value);
    }
    public int GetIntMaster(int ID)
    {

        string value = string.Empty;
        _masters.TryGetValue(ID, out value);

        if (value == null) value = string.Empty;

        try
        {
            return int.Parse(value);
        }
        catch
        {
            return -1;
        }

    }
}
