using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoleObject : MonoBehaviour
{
    /// <summary>
    /// レベルを入れるテキスト
    /// </summary>
    [SerializeField] TextMeshProUGUI _level;
    /// <summary>
    /// 名前を入れるテキスト
    /// </summary>
    [SerializeField] TextMeshProUGUI _name;
    /// <summary>
    /// 基本スコアを入れるテキスト
    /// </summary>
    [SerializeField] TextMeshProUGUI _score;
    /// <summary>
    /// 倍率を入れるテキスト
    /// </summary>
    [SerializeField] TextMeshProUGUI _magnification;
    /// <summary>
    /// 使用回数を入れるテキスト
    /// </summary>
    [SerializeField] TextMeshProUGUI _playCount;

    /// <summary>
    /// デバックように見える化
    /// このオブジェクトがみせているロール
    /// </summary>
    [SerializeField] RoleManager.Role _role;



    public void Show(RoleManager.Role role)
    {
        _role = role;

        // この変数はデバックようにレベルを固定する物
        int level = 1;

        int roleID = IDUtility.ROLE_ID + (int)_role;

        _level.text = MasterData.instance.GetStringMaster(IDUtility.LEVEL_ID + level);

        _name.text = MasterData.instance.GetStringMaster(roleID);

        _score.text = ScoreMaster.instance.GetBasicScore(roleID).ToString();
        _magnification.text = ScoreMaster.instance.GetBasicMagnification(roleID).ToString();

        // 使用回数
        _playCount.text = RoleManager.instance.GetRolePlayCountList()[(int)_role].ToString();

    }
}
