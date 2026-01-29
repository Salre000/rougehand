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


    private static readonly string Tutorial = "TutorialDeck";

    private static bool tutorialFlag=false;

    public static int seed = -1;

    /// <summary>
    ///  セーブデータがあるどうかを判断する関数
    /// </summary>
    /// <returns></returns>
    public static bool CheckSaveDeta() 
    {

        string path = Application.dataPath + FILE_PASS + FILE_NAME_KD + FILR_EXTENSION;
        if (!File.Exists(path)) return false;

        return true;


    }


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
    public static void Use(string flieName="")
    {

        
         


        if (flieName==string.Empty)
        {
            LoodLostData();
            instantMemory.Use();
        }
        else
        {
            instantMemory = new Memory(flieName);

            if (Tutorial == flieName) 
            {
                // TODO: チュートリアルの専用処理をかく
                BossManager.instance.CreateBoss(0);


                tutorialFlag = true;

            }

        }

        if (instantMemory.theSeed == -1) instantMemory.theSeed = SetSeed();

        Random.InitState(instantMemory.theSeed);

        seed = instantMemory.theSeed;

    }
    /// <summary>
    /// 過去のデータを消去する
    /// </summary>
    public static void Lost() 
    {
        instantMemory = null;
    }

    /// <summary>
    /// 過去のデータを作成する
    /// </summary>
    public static void Keep() 
    {
        if (GameRoot.instance.GetGameOver()) return;

        // リザルトが呼び出されているならば
        if (GameUtility.IsRoundResult()) 
        {
            ShopManager.instance.SetIsShop(true);

            int roundCount = GameUtility.GetRewardMaxCount();

            int addMoney = MasterData.instance.GetIntMaster(IDUtility.REWARD_ID+roundCount);

            addMoney += GameUtility.GetHandCount();

            GameUtility.SetMyMoney(GameUtility.GetMyMoney() + addMoney);


        }

        instantMemory=new Memory();

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.dataPath + FILE_PASS + FILE_NAME_KD + FILR_EXTENSION;
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        formatter.Serialize(stream, instantMemory);
        stream.Close();
    }

    /// <summary>
    /// チュートリアルかどうかを判断
    /// </summary>
    /// <returns></returns>
    public static bool GetTutorialFlag() {return tutorialFlag;}

    private static int SetSeed() 
    {
        int seed = 0;
        seed += Random.RandomRange(1, 9) * 10000;
        seed += Random.RandomRange(0, 9) * 1000;
        seed += Random.RandomRange(0, 9) * 100;
        seed += Random.RandomRange(0, 9) * 10;
        seed += Random.RandomRange(0, 9);

        return seed;

    }

}
