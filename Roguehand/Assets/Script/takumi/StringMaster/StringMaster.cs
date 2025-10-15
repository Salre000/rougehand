using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class StringMaster : MonoBehaviour
{
    public static StringMaster instance;

    /// <summary>
    /// マスターから引き出した文字列の格納先
    /// </summary>
    private Dictionary<int, string> _stringMasters = new Dictionary<int, string>();

    public void Awake()
    {
        instance = this;
        // シーンの移行で破壊されない用に変更
        DontDestroyOnLoad(this.gameObject);

        Lood();

    }

    /// <summary>
    /// マスターから引き出す関数
    /// </summary>
    private void Lood()
    {
        // ディレクトリパス
        string path = Application.dataPath + "/Resources/takumi/StringMaster";

        string check = ".meta";

        List<string> list = new List<string>();

        // DirectoryInfoのインスタンスを生成する
        DirectoryInfo di = new DirectoryInfo(path);

        FileInfo[] fiPatterns = di.GetFiles("*Master*");
        foreach (FileInfo f in fiPatterns)
        {

            string name = f.Name;

            //メタファイルを除外
            if (name.Contains(check)) continue;

            //拡張子を削除
            name = name.Substring(0, name.Length - 4);
            list.Add(name);
        }



        //読み込んだCSVファイルを格納
        List<string[]> csvDatas = new List<string[]>();

        //CSVファイルの行数を格納
        int height = 0;

        //ファイルパスとファイルの名前を繋げる
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < list.Count; i++)
        {
            builder.Clear();
            builder.Append("takumi/StringMaster/");
            builder.Append(list[i]);

            //繋げたファイルパスを使いファイルのロードを行う
            TextAsset textAsset = Resources.Load<TextAsset>(builder.ToString());

            //読み込んだテキストをString型にして格納
            StringReader reader = new StringReader(textAsset.text);

            while (reader.Peek() > -1)
            {
                string line = reader.ReadLine();
                // ,で区切ってCSVに格納
                csvDatas.Add(line.Split(','));
                height++; // 行数加算
            }

            for (int j = 0; j < csvDatas.Count; j++)
            {
                _stringMasters.Add(int.Parse(csvDatas[j][0]), csvDatas[j][1]);
            }
            csvDatas.Clear();
        }
        GetMaster(-1);

    }


    /// <summary>
    /// マスターからID指定で文字列の取得
    /// 文字化けを仕込むならばこの行に追加をする
    /// </summary>
    /// <param name="ID"><文字ID/param>
    /// <returns><IDに対応した文字列/returns>
    public string GetMaster(int ID) { string value; _stringMasters.TryGetValue(ID, out value); return value; }

}
