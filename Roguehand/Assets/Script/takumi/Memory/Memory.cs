using System.Collections.Generic;
using System.IO;
using System.Text;

using UnityEngine;

[System.Serializable]
public class Memory
{
    private readonly string FREE_PASS = "takumi/";
    private readonly string BASE_FILE = "DeckBase/";
    private readonly string BASE_DECK = "BaseDeck";
    private readonly string extension = ".csv";
    /// <summary>
    /// クラス作成時の基本コンストラクター
    /// </summary>
    public Memory()
    {

        //　現在のデッキデータを取得
        _trumps = CardManager.instance.deck;

        //  現在のジョーカーのデータを取得
        _jokers = JokerUtility.GetJokers();

        //　現在のアイテムのデータを取得
        _items = ItemUtility.GetItemBase();

        //  現在のお金のデータを取得
        _money = GameUtility.GetMyMoney();

        // 　現在のラウンドのカウントを取得
        _round = GameUtility.GetRoundCount();

        // 　現在のアンティのカウントを取得
        _ante = GameUtility.GetAnteCount();

        //  現在の役の使用数を取得
        _roleCount = RoleManager.instance.GetRolePlayCountList();

        //  現在の役のレベルを取得
        _roleLevelCount = RoleManager.instance.GetRoleLevels();

        //  現在のプレイ可能回数を取得
        _handCount = GameUtility.GetHandCount();

        //  現在のディスカードの可能回数を取得
        _discardCount = GameUtility.GetDiscardCount();
    }

    public Memory(string fileName)
    {
        if (fileName == string.Empty) fileName = BASE_DECK;

        //読み込んだCSVファイルを格納
        List<string[]> csvDatas = new List<string[]>();

        //CSVファイルの行数を格納
        int height = 0;

        //ファイルパスとファイルの名前を繋げる
        StringBuilder builder = new StringBuilder();
        builder.Clear();
        builder.Append(FREE_PASS);
        builder.Append(BASE_FILE);
        builder.Append(BASE_DECK);


        //繋げたファイルパスを使いファイルのロードを行う
        TextAsset textAsset = Resources.Load<TextAsset>(builder.ToString());

        //読み込んだテキストをString型にして格納
        StringReader reader = new StringReader(textAsset.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            // ,で区切ってCSVに格納
            csvDatas.Add(line.Split(','));
        }


        CreateDeck(csvDatas);
        height += (int)Card.suit.max;
        CreateJoker(csvDatas[height]);
        height++;
        CreateItem(csvDatas[height]);
        height++;
        CreateMoney(csvDatas[height]);
        height++;
        CreateAnte(csvDatas[height]);
        height++;
        CreateRound(csvDatas[height]);
        CreateScore();
        height++;
        CreateRoleCount(csvDatas[height]);
        height++;
        CreateRoleLevel(csvDatas[height]);
        height++;
        CreateHandCount(csvDatas[height]);
        height++;
        CreateDiscard(csvDatas[height]);
    }

    /// <summary>
    /// この保存データを使用する関数
    /// </summary>
    public void Use()
    {



    }

    private void CreateDeck(List<string[]> deta)
    {
        List<List<int>> dommy = new List<List<int>>();
        for (int i = 0; i < deta.Count; i++)
        {
            dommy.Add(new List<int>());
            for (int j = 1; j < deta[i].Length; j++)
            {
                if (!int.TryParse(deta[i][j], out int tryInt)) continue;
                dommy[i].Add(tryInt);
            }
        }
        TrumpCard trumpCard = new TrumpCard();

        trumpCard.CreateDeck(dommy);



    }

    private void CreateJoker(string[] data)
    {
        for (int i = 1; i < data.Length; i++)
        {
            if (!int.TryParse(data[i], out int tryInt)) return;

            JokerUtility.Addjoker(tryInt);
        }

    }
    private void CreateItem(string[] data)
    {
        for (int i = 1; i < data.Length; i++)
        {
            if (!int.TryParse(data[i], out int tryInt)) return;

            ItemUtility.AddItem(tryInt);
        }

    }
    private void CreateMoney(string[] data)
    {
        int moneyPoint = 1;
        if (!int.TryParse(data[moneyPoint], out int tryInt)) return;
        GameUtility.SetMyMoney(tryInt);

    }

    private void CreateAnte(string[] data)
    {
        int antePoint = 1;
        if (!int.TryParse(data[antePoint], out int tryInt)) return;
        GameUtility.SetAnteCount(tryInt);
        TextUIManager.instance.SetAnteText(tryInt.ToString());


    }
    private void CreateRound(string[] data)
    {
        int roundPoint = 1;
        if (!int.TryParse(data[roundPoint], out int tryInt)) return;
        GameUtility.SetRoundCount(tryInt);
        TextUIManager.instance.SetRoundText(tryInt.ToString());


    }

    private void CreateScore()
    {
        int anteRate = 3;

        GameUtility.SetAllRoundCount(GameUtility.GetRoundCount() + (GameUtility.GetAnteCount() - 1) * anteRate);

        StringBuilder _builder = new StringBuilder();
        int id = IDUtility.TARGET_SCORE_ID + GameUtility.GetAllRoundCount();
        _builder.Append(MasterData.instance.GetIntMaster(id));
        TextUIManager.instance.SetLowestScoreText(_builder.ToString());

    }
    private void CreateRoleCount(string[] data)
    {
        List<int> roleCount = new(17);
        for (int i = 1; i < data.Length; i++)
        {
            if (!int.TryParse(data[i], out int tryInt)) return;
            roleCount.Add(tryInt);
        }

        RoleManager.instance.SetRoleCount(roleCount);
    }

    private void CreateRoleLevel(string[] data)
    {
        List<int> roleLevel = new(17);
        for (int i = 1; i < data.Length; i++)
        {
            if (!int.TryParse(data[i], out int tryInt)) return;
            roleLevel.Add(tryInt);
        }

        RoleManager.instance.SetRoleLevel(roleLevel);

    }

    private void CreateHandCount(string[] data) 
    {
        int handCountPoint = 1;
        if (!int.TryParse(data[handCountPoint], out int tryInt)) return;
        GameUtility.SetHandCount(tryInt);
        TextUIManager.instance.SetHandText(tryInt.ToString());

    }
    private void CreateDiscard(string[] data) 
    {
        int discardPoint = 1;
        if (!int.TryParse(data[discardPoint], out int tryInt)) return;
        GameUtility.SetDiscardCount(tryInt);
        TextUIManager.instance.SetDiscardText(tryInt.ToString());

    }

    /// <summary>
    /// カードの保存先
    /// </summary>
    private List<Card.Trump> _trumps;

    /// <summary>
    /// ジョーカーの保存先
    /// </summary>
    private List<JokerBase> _jokers;

    /// <summary>
    /// アイテムの保存先
    /// </summary>
    private List<ItemBase> _items;

    /// <summary>
    /// お金の保存先
    /// </summary>
    private int _money;

    /// <summary>
    /// アンティの保存先
    /// </summary>
    private int _ante;
    /// <summary>
    /// ラウンドの保存先
    /// </summary>
    private int _round;

    /// <summary>
    /// 役の使用回数の保存先
    /// </summary>
    private List<int> _roleCount;
    /// <summary>
    /// 役のレベルの保存先
    /// </summary>
    private List<int> _roleLevelCount;

    /// <summary>
    /// プレイ回数の保存先
    /// </summary>
    private int _handCount;

    /// <summary>
    /// ディスカードの回数
    /// </summary>
    private int _discardCount;





}
