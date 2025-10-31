using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// スコアが目標スコアに達しているかどうか
/// 達していたらそのゲームを終了
/// </summary>
public class TargetScoreChecker : MonoBehaviour
{
    private const int _TARGET_SCORE_ID = 7000;
    private const int _REWARD_ID = 8000;
    private int _targetScore;
    private int _roundCount = -1;
    private StringBuilder _builder = new StringBuilder();
    float roundScore = -1;

    // Start is called before the first frame update
    void Start()
    {
        // ラウンド数と目標スコアの設定
        RoundStart();
            _builder.Clear();
        _builder.Append(MasterData.instance.GetStringMaster(_TARGET_SCORE_ID + _roundCount));
        TextUIManager.instance.SetLowestScoreText(_builder.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        RoundCheck();
    }

    private void RoundStart()
    {
        if( _roundCount ==GameUtility.GetRoundCount() )return;
        // ラウンドカウントのセット
        _roundCount=GameUtility.GetRoundCount();
        GameUtility.SetRoundCount(_roundCount);
        _builder.Clear();
        _builder.Append(_roundCount);
        TextUIManager.instance.SetRoundText(_builder.ToString());
    }

    // 達しているか確認
    private void RoundCheck()
    {
        // プレイボタンが押されてなかったら比較を行わない
        if(!GameUtility.IsPlay())return;

        // 合計スコアが増加されてなかったら比較しない
        if (!GameUtility.IsRoundScoreUp())return ;
        // 目標スコアと現スコアを比較
        _targetScore = MasterData.instance.GetIntMaster(_TARGET_SCORE_ID+_roundCount);
        // 不正値が返ってきたならreturn
        if(_targetScore < 0)return;
        // 一時的にボタン受付を停止
        GameUtility.SetIsPushButton(false);
        // 合計スコアと比較
        roundScore = ScoreManager.instance.GetRoundScore();
        // 目標スコアを越していたら次のラウンドへ
        NextRoundExecute();
        // 合計スコアの増加フラグをリセット
        GameUtility.SetIsRoundScoreUp(false);
        // 最終的にプレイボタンのフラグをリセット
        GameUtility.SetIsPlay(false);

    }

    // 次のラウンド処理
    private void NextRoundExecute()
    {

        roundScore = ScoreManager.instance.Rounding(roundScore, 1f);
        if (_targetScore <=roundScore)
        {
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
            // 手札のリセット
            CardManager.instance.ResetHand();

            // カードオブジェクトをリセット
            CardObjectUtility.ResetCard();

            int reward = MasterData.instance.GetIntMaster(_REWARD_ID + _roundCount);
            // 報奨金のセット
            TextUIManager.instance.SetRewardCountText(UIUtility.instance.RewardConversion(reward));
        }
    }

}
