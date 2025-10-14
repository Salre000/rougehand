using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StringMaster : MonoBehaviour
{
    public static StringMaster instance;

    /// <summary>
    /// マスターから引き出した文字列の格納先
    /// </summary>
    private Dictionary<int,string> _stringMasters = new Dictionary<int,string>();

    public void Awake()
    {
        // シーンの移行で破壊されない用に変更
        DontDestroyOnLoad(this.gameObject);

        instance = this;

        Lood();

    }

    /// <summary>
    /// マスターから引き出す関数
    /// </summary>
    private void Lood() 
    {
        // ディレクトリパス
        string path = Application.dataPath+ "/Resources/takumi/StringMaster";
        // DirectoryInfoのインスタンスを生成する
        DirectoryInfo di = new DirectoryInfo(path);

        FileInfo[] fiPatterns = di.GetFiles("*Master*");
        foreach (FileInfo f in fiPatterns)
        {
            Debug.Log(f.DirectoryName);


        }

    }


    /// <summary>
    /// マスターからID指定で文字列の取得
    /// </summary>
    /// <param name="ID"><文字ID/param>
    /// <returns><IDに対応した文字列/returns>
    public string GetMaster(int ID) { string value; _stringMasters.TryGetValue(ID, out value);return value; }

}
