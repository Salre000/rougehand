using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using static TextUIManager;

/// <summary>
/// 選択したハンドの役をテキストに反映させる
/// </summary>
public class SerectCardRole : MonoBehaviour
{
    private StringBuilder _builder = new StringBuilder();

    private void Update()
    {
        if(!RoleManager.instance.IsCheck()) return;
        CheckRole();
    }

    private void CheckRole()
    {
        StringBuild();
        instance.SetRoleText(_builder.ToString());
        RoleManager.instance.SetIsCheck(false);
    }

    private void StringBuild()
    {
        _builder.Clear();
        if (CardManager.instance.GetPick().Count <= 0)
        {
            _builder.Append("");
            return;
        }
        // 役
        RoleManager.Role role = RoleManager.instance.GetRole();
        string name = StringMaster.instance.GetMaster(3000+(int)role);
        _builder.Append(name);
        if (_builder.Length >= 9)
            instance.GetRoleText().fontSize=30.1f;
        else if (_builder.Length >= 7)
            instance.GetRoleText().fontSize = 34.1f;
        else
            instance.GetRoleText().fontSize = 41.1f;
        // 文字サイズ
        name = StringMaster.instance.GetMaster(5000);
        _builder.Append(name);
        // レベルカラー
        int level = RoleManager.instance.GetRoleLevel(role);
        name = StringMaster.instance.GetMaster(5000+level);
        _builder.Append(name);
        // レベル
        name= StringMaster.instance.GetMaster(4000+level);
        _builder.Append(name);
        // 基本スコアと倍率

    }

}
