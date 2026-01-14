using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MemoryManager
{
    public static Memory instantMemory = null;

    private static readonly string FILE_PASS = "/Resources/";

    public const string FILE_NAME_KD = "SaveData";

    private static readonly string FILR_EXTENSION = ".txt";



    public static void CreateMemory()
    {
        instantMemory = new Memory();
    }

    /// <summary>
    /// 過去のデータを読み込む
    /// </summary>
    public static bool LoodLostData() 
    {
        string path = Application.dataPath + FILE_PASS + FILE_NAME_KD + FILR_EXTENSION;
        if (!File.Exists(path)) return false;


        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        Memory data = formatter.Deserialize(stream) as Memory;
        stream.Close();

        instantMemory = data;

        File.Delete(Application.dataPath + FILE_PASS + FILE_NAME_KD + FILR_EXTENSION);

        return true;

    }

    /// <summary>
    /// データを使いゲームを構築する
    /// </summary>
    public static void Use()
    {
        if (LoodLostData())
        {
            instantMemory.Use();
        }
        else
        {
            instantMemory = new Memory(string.Empty);
        }
    }

    /// <summary>
    /// 過去のデータを作成する
    /// </summary>
    public static void Keep() 
    {
        instantMemory=new Memory();

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.dataPath + FILE_PASS + FILE_NAME_KD + FILR_EXTENSION;
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        formatter.Serialize(stream, instantMemory);
        stream.Close();
    }


}
