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
    private const int _ROLE_ID = 3000;
    private const int _SCORE_ID = 3000;
    private const int _LEVEL_ID = 4000;
    private const int _RICHTEXT_ID = 5000;

    private const int _TEXT_MAX_LENGT_TYPE1 = 9;
    private const int _TEXT_MAX_LENGT_TYPE2 = 7;
    private const float _FONT_SIZE_TYPE1 = 27.1f/*30.1f*/;
    private const float _FONT_SIZE_TYPE2 = 34.1f;
    private const float _FONT_SIZE_TYPE3 = 41.1f;
    private const int _NOT_CARD_PICK_COUNT = 0;
    private const int _NO_SCORE = 0;
    // 役
    RoleManager.Role role;

    private void Update()
    {
        if(!RoleManager.instance.IsCheck()) return;
        CheckRole();
    }

    private void CheckRole()
    {
        // 役の名前とレベルの反映
        StringBuildRoleNameLevel();
        // 役のスコア倍率の反映
        StringBuildScore();
        // 役の更新の終了をお知らせ
        RoleManager.instance.SetIsCheck(false);
    }

    private void StringBuildRoleNameLevel()
    {
        _builder.Clear();
        // 選択カードが無い場合
        if (CardManager.instance.GetPick().Count <= _NOT_CARD_PICK_COUNT)
        {
            _builder.Append("");
            // Textの変更
            instance.SetRoleText(_builder.ToString());
            return;
        }
        // 役
        role = RoleManager.instance.GetRole();
        string name = MasterData.instance.GetStringMaster(_ROLE_ID + (int)role);
        _builder.Append(name);
        if (_builder.Length >= _TEXT_MAX_LENGT_TYPE1)
            instance.GetRoleText().fontSize= _FONT_SIZE_TYPE1;
        else if (_builder.Length >= _TEXT_MAX_LENGT_TYPE2)
            instance.GetRoleText().fontSize = _FONT_SIZE_TYPE2;
        else
            instance.GetRoleText().fontSize = _FONT_SIZE_TYPE3;
        // 文字サイズ
        name = MasterData.instance.GetStringMaster(_RICHTEXT_ID);
        _builder.Append(name);
        // レベルカラー
        int level = RoleManager.instance.GetRoleLevel(role);
        name = MasterData.instance.GetStringMaster(_RICHTEXT_ID + level);
        _builder.Append(name);
        // レベル
        name= MasterData.instance.GetStringMaster(_LEVEL_ID+level);
        _builder.Append(name);
        // Textの変更
        instance.SetRoleText(_builder.ToString());

    }

    private void StringBuildScore()
    {
        _builder.Clear();
        // 選択カードが無い場合
        if (CardManager.instance.GetPick().Count <= _NOT_CARD_PICK_COUNT)
        {
            _builder.Append(_NO_SCORE);
            // Textの変更
            instance.SetBasicScoreText(_builder.ToString());
            instance.SetMagnificationText(_builder.ToString());
            return;
        }
        // 基本スコアと倍率
        // 基本スコア
        int basic = ScoreMaster.instance.GetBasicScore(_SCORE_ID + (int)role);

        // 役によって変わる上昇幅を獲得
        int addBasicLevel= ScoreMaster.instance.GetAddBasicScore(_SCORE_ID + (int)role);
        // プレイされた役の現レベルを獲得
        int level = RoleManager.instance.GetRoleLevel(role);
        // レベルに応じてスコアを上昇
        for (int i = 1; i < level; i++)
            basic += addBasicLevel;

        // 共有
        ScoreManager.instance.SetBasic(basic);
        _builder.Append(basic);
        // Textの変更
        instance.SetBasicScoreText(_builder.ToString());



        _builder.Clear();
        // 倍率
        int magnifi = ScoreMaster.instance.GetBasicMagnification(_SCORE_ID + (int)role);

        // 役によって変わる上昇幅を獲得
        int addMagniLevel = ScoreMaster.instance.GetAddBasicMagnification(_SCORE_ID + (int)role);
        // レベルに応じてスコアを上昇
        for (int i = 1; i < level; i++)
            magnifi += addMagniLevel;

        // 共有
        ScoreManager.instance.SetMagnification(magnifi);
        _builder.Append(magnifi);
        // Textの変更
        instance.SetMagnificationText(_builder.ToString());


    }

    // カードがスコアされたら結果を役の名前のほうに、倍率たちをゼロにする
    // 使わない関数です
    private void PlayScore()
    {
        _builder.Clear();

        #region スコアと倍率のリセット

        _builder.Append(_NO_SCORE);
        // Textの変更
        instance.SetBasicScoreText(_builder.ToString());
        instance.SetMagnificationText(_builder.ToString());
        _builder.Clear();

        #endregion

        

    }

}
