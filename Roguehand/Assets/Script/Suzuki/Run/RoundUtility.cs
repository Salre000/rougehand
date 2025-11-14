using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class RoundUtility
{

    private static StringBuilder _builder = new StringBuilder();

    /// <summary>
    /// 
    /// </summary>
    /// <returns>false 不正値:true 正常</returns>
    public static bool NextStartRound()
    {
        int roundCount = GameUtility.GetRoundCount();
        // 目標スコアの取得
        int targetScore = MasterData.instance.GetIntMaster(IDUtility.TARGET_SCORE_ID + roundCount);
        // 不正値が返ってきたならreturn
        if (targetScore < 0) return false;
        // 合計スコアと比較
        float roundScore = ScoreManager.instance.GetRoundScore();
        roundScore = ScoreManager.instance.Rounding(roundScore, 1f);
        if (targetScore > roundScore) return true;

        // リザルトのスコアとハンドの設定
        _builder.Clear();
        _builder.Append(MasterData.instance.GetStringMaster(IDUtility.TARGET_SCORE_ID + roundCount));
        TextUIManager.instance.SetResultLowestScoreText(_builder.ToString());

        _builder.Clear();
        _builder.Append(GameUtility.GetHandCount());
        TextUIManager.instance.SetResultHandText(_builder.ToString());

        // ハンドの残り回数によるお金の表示
        _builder.Clear();
        int count = GameUtility.GetHandCount();
        _builder.Append(UIUtility.instance.RewardConversion(count));
        TextUIManager.instance.SetResultMoneyText(_builder.ToString());

        // ラウンドクリア報酬金
        int reward = MasterData.instance.GetIntMaster(IDUtility.REWARD_ID + GameUtility.GetRoundCount());
        TextUIManager.instance.SetResultClearMoneyText(UIUtility.instance.RewardConversion(reward));

        // 清算ボタンの合計金表示
        int allReward = count + reward;
        _builder.Clear();
        _builder.Append("$");
        _builder.Append(allReward);
        TextUIManager.instance.SetClearMoneyText(_builder.ToString());

        GameUtility.SetIsRoundResult(true);
        // 手札のリセット
        CardManager.instance.ResetHand();

        return true;
    }
}
