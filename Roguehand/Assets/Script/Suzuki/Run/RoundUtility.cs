using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class RoundUtility
{

    private static StringBuilder _builder = new StringBuilder();

    // 次のラウンド処理
    public static void NextRoundExecute(int _targetScore,ref int _roundCount,int _TARGET_SCORE_ID,int _REWARD_ID)
    {
        // 合計スコアと比較
        float　roundScore = ScoreManager.instance.GetRoundScore();
        roundScore = ScoreManager.instance.Rounding(roundScore, 1f);
        if (_targetScore > roundScore) return;
        
            // 合計スコアのリセット
            ScoreManager.instance.ResetRoundScore();
            // ラウンド数の増加
            _roundCount++;
            GameUtility.SetRoundCount(_roundCount);
            _builder.Clear();
            _builder.Append(_roundCount);
            TextUIManager.instance.SetRoundText(_builder.ToString());
            // 目標スコアの再設定
            _builder.Clear();
            _builder.Append(MasterData.instance.GetStringMaster(_TARGET_SCORE_ID + _roundCount));
            TextUIManager.instance.SetLowestScoreText(_builder.ToString());

            // 次のラウンドへ移行することを他にも知らせる
            //GameUtility.SetIsNextRound(true);
            //GameUtility.SetIsNextRound(false);


            // ハンド回数のリセット
            GameUtility.SetHandCount(GameUtility.GetBaseHandCound());
            // ディスカードのリセット
            GameUtility.SetDiscardCount(GameUtility.GetBaseDiscardCound());

            // デッキのリセット
            CardManager.instance.ResetDeck();

            Debug.Log(CardManager.instance.GetDeck().GetCount(card => card.state == Card.State.deck) + "リセット直後");

            // 手札のリセット
            CardManager.instance.ResetHand();

            // カードオブジェクトをリセット
            CardObjectUtility.ResetCard();

            int reward = MasterData.instance.GetIntMaster(_REWARD_ID + _roundCount);
            // 報奨金のセット
            TextUIManager.instance.SetRewardCountText(UIUtility.instance.RewardConversion(reward));

            // ショップ状態へ移行
            ShopManager.instance.SetIsShop(true);
        
    }

    // スコアが足りてリザルトを出す
    public static void NextStartRound(int _targetScore, ref int _roundCount, int _TARGET_SCORE_ID, int _REWARD_ID)
    {
        // 合計スコアと比較
        float roundScore = ScoreManager.instance.GetRoundScore();
        roundScore = ScoreManager.instance.Rounding(roundScore, 1f);
        if (_targetScore > roundScore) return;

        // 合計スコアのリセット
        ScoreManager.instance.ResetRoundScore();

        // 目標スコアの再設定
        _builder.Clear();
        _builder.Append("");
        TextUIManager.instance.SetLowestScoreText(_builder.ToString());

        // リザルトのスコアとハンドの設定
        _builder.Clear();
        _builder.Append(MasterData.instance.GetStringMaster(_TARGET_SCORE_ID + _roundCount));
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
        int reward = MasterData.instance.GetIntMaster(_REWARD_ID + _roundCount);
        TextUIManager.instance.SetResultClearMoneyText(UIUtility.instance.RewardConversion(reward));



        // ショップ状態へ移行
        //ShopManager.instance.SetIsShop(true);
    }
}
