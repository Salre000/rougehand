using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

[System.Serializable]
public class Memory
{

    /// <summary>
    /// クラス作成時の基本コンストラクター
    /// </summary>
    public Memory()
    {

        //　現在のデッキデータを取得
        _trumps = CardManager.instance.deck;
            
        //  現在のジョーカーのデータを取得
        _jokers=JokerUtility.GetJokers();

        //　現在のアイテムのデータを取得
        _items=ItemUtility.GetItemBase();

        //  現在のお金のデータを取得
        _money = GameUtility.GetMyMoney();

        // 　現在のラウンドのカウントを取得
        _round=GameUtility.GetRoundCount();

        //  現在の役の使用数を取得
        _roleCount = RoleManager.instance.GetRolePlayCountList();

        //  現在の役のレベルを取得
        _roleLevelCount = RoleManager.instance.GetRoleLevels();

        //  現在のプレイ可能回数を取得
        _handCount=GameUtility.GetHandCount();

        //  現在のディスカードの可能回数を取得
        _discardCount = GameUtility.GetDiscardCount();
    }

    public Memory(string fileName) 
    {
        if (false) 
        {

        }


    }

    /// <summary>
    /// この保存データを使用する関数
    /// </summary>
    public void Use() 
    {



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
