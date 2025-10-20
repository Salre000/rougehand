using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

/// <summary>
/// 選択したハンドの役をテキストに反映させる
/// </summary>
public class SerectCardRole : MonoBehaviour
{
    private TextMeshProUGUI _roleText = null;
    private StringBuilder _builder = new StringBuilder();
    private RolePrediction _rich = new();

    private void Awake()
    {
        _roleText = GameObject.Find("RoleText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(!RoleManager.instance.IsCheck()) return;
        CheckRole();
    }

    private void CheckRole()
    {
        StringBuild();
        _roleText.text = _builder.ToString();
        RoleManager.instance.SetIsCheck(false);
    }

    private void StringBuild()
    {
        _builder.Clear();
        // 役
        RoleManager.Role role = RoleManager.instance.GetRole();
        string name = StringMaster.instance.GetMaster(3000+(int)role);
        _builder.Append(name);
        if (_builder.Length >= 9)
            _roleText.fontSize = 30.1f;
        else if (_builder.Length >= 7)
            _roleText.fontSize = 34.1f;
        else
            _roleText.fontSize = 41.1f;
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
    }

}
