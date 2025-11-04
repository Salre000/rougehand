using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

/// <summary>
/// 基本スコアと倍率のテキストと結果をいじる
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    // 基本スコア
    private float _basicScore;
    // 倍率
    private float _magnification;
    // ラウンドの合計スコア
    private　float _roundScore;
    // プレイしたハンドのスコア
    private float _handScore;

    private StringBuilder builder;
    // ラウンドスコアの文字が枠外に出るくらいの文字数を検知
    private int _defaultRemit = 9;
    // 減らす文字サイズ
    private const int _DOWNSIZE = 2;
    // 元のフォントサイズ
    private const float _DEFAULT_OFFSET=44.1f;
    // スコア時の文字が枠外に出るくらいの文字数を検知
    private int _scoreRemitLength = 9;
    // スコア時のフォントサイズ
    private const float _SCORE_OFFSET = 70f;
    // 文字数検知のリセット
    private const int _RESET_REMIT_SIZE = 9;
    // ゼロにする
    private const int _RESET_NUM = 0;


    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        builder = new StringBuilder();

    }


    /// <summary>
    /// 基本の加算
    /// </summary>
    /// <param name="value">入れた分だけ加算</param>
    public void BasicPlus(float value)
    {
        _basicScore += value;
        builder.Clear();
        builder.Append(_basicScore);
        TextUIManager.instance.SetBasicScoreText(builder.ToString());

    }

    /// <summary>
    /// 倍率の加算
    /// </summary>
    /// <param name="value">入れた分だけ加算</param>
    public void MagnificationPlus(float value)
    {
        _magnification += value;
        _magnification = Rounding(_magnification,2f);

        builder.Clear();
        builder.Append(_magnification);
        TextUIManager.instance.SetMagnificationText(builder.ToString());
    }

    /// <summary>
    /// 二つの結果を合計ラウンドにまとめる
    /// </summary>
    public void RoundScoreResult()
    {
        _roundScore+= _handScore;
        // 四捨五入した値が返る
        _roundScore = Rounding(_roundScore,1f);

        builder.Clear();
        builder.AppendFormat("{0:#}", _roundScore.ToString("N0"));
        
        if(builder.Length >= _defaultRemit)
        {
            TextUIManager.instance.GetRoundScoreText().fontSize -= _DOWNSIZE;
            _defaultRemit++;
        }
        else
        {
        _defaultRemit = _RESET_REMIT_SIZE;

        }

            TextUIManager.instance.SetRoundScoreText(builder.ToString());
    }

    /// <summary>
    /// 二つの結果を表示
    /// </summary>
    public void PlayScoreResult()
    {

        _handScore = _basicScore * _magnification;
        // 四捨五入した値が返る
        _handScore = Rounding(_handScore, 1f);

        builder.Clear();
        builder.AppendFormat("{0:#}", _handScore.ToString("N0"));

        if (builder.Length >= _scoreRemitLength)
        {
            TextUIManager.instance.GetRoleText().fontSize -= _DOWNSIZE;
            _scoreRemitLength++;
        }
            TextUIManager.instance.GetRoleText().fontSize = _SCORE_OFFSET;

        TextUIManager.instance.SetRoleText(builder.ToString());
        _scoreRemitLength = _RESET_REMIT_SIZE;

        // ゼロにする
        builder.Clear();
        builder.Append(_RESET_NUM);
        TextUIManager.instance.SetBasicScoreText(builder.ToString());
        TextUIManager.instance.SetMagnificationText(builder.ToString());
    }

    /// <summary>
    /// 基本と倍率と結果の表示と中身をゼロにする
    /// </summary>
    //public void ScoreReset()
    //{
    //    builder.Clear();
    //    _roundScore = _magnification =_basicScore = 0;
    //    _defaultRemit = _RESET_REMIT_SIZE;
    //    TextUIManager.instance.GetRoundScoreText().fontSize = _DEFAULT_OFFSET;
    //    builder.Append(_basicScore);
    //    TextUIManager.instance.SetBasicScoreText(builder.ToString());
    //    TextUIManager.instance.SetMagnificationText(builder.ToString());
    //    TextUIManager.instance.SetRoundScoreText(builder.ToString());
    //}

    /// <summary>
    /// 倍率を乗算
    /// </summary>
    /// <param name="value"></param>
    public void Multiplication(float value)
    {
        _magnification*=value;
    }

    /// <summary>
    /// ハンドスコアをゼロにしながらラウンドスコアに加算
    /// </summary>
    public void RoundScorePlus()
    {
        // 合計に加算
        _roundScore += _handScore;
        // 0にする
        _handScore = 0;
        // 空白にする
        builder.Clear();
        builder.Append("");
        TextUIManager.instance.SetRoleText(builder.ToString());


        // ラウンドスコアを表示
        builder.Clear();
        builder.Append(_roundScore);
        TextUIManager.instance.SetRoundScoreText(builder.ToString());

        // 合計スコアの増加をした
        //GameUtility.SetIsRoundScoreUp(true);
    }

    /// <summary>
    /// ラウンドスコアと目標スコアのリセット
    /// </summary>
    public void ResetRoundScore()
    {
        _roundScore = 0;
        builder.Clear();
        builder.Append(_roundScore);
        TextUIManager.instance.SetRoundScoreText(builder.ToString());
        builder.Clear() ;
        builder.Append("");
        TextUIManager.instance.SetLowestScoreText(builder.ToString());
    }

    /// <summary>
    /// 四捨五入
    /// </summary>
    /// <param name="value">したい値</param>
    /// <param name="decPoint">小数第〇を指定</param>
    /// <returns>四捨五入した値</returns>
    public float Rounding(float value,float decPoint)
    {
        // 小数部分の取り出し
        float num1=value-Mathf.FloorToInt(value);
        // 四捨五入したい位を一の位に持ってくる
        int num2 = Mathf.FloorToInt(num1 * Mathf.Pow(10, decPoint));
        // 十以上の位をなくす
        int num3 = num2 - Mathf.FloorToInt(num2 / 10) * 10;
        if( num3 >= 5 )
        {
            // 切り上げ
            // 切り上げたい位まで小数点を移動させて切り上げ
            num1= Mathf.CeilToInt(value * Mathf.Pow(10, decPoint - 1));
            // 戻す
            num1 /= Mathf.Pow(10, decPoint - 1);
        }
        else
        {
            // 切り捨て
            // 同様に
            num1 =Mathf.FloorToInt(value * Mathf.Pow(10, decPoint - 1));
            // 戻す
            num1 /= Mathf.Pow(10, decPoint - 1);
        }

        return num1;
    }


    private const int _TARGET_SCORE_ID = 7000;
    private const int _REWARD_ID = 8000;
    private int _targetScore;
    private int _roundCount = -1;
    float roundScore = -1;
    // 達しているか確認
    public void RoundCheck()
    {
        // プレイボタンが押されてなかったら比較を行わない
        //if (!GameUtility.IsPlay()) return;

        // 合計スコアが増加されてなかったら比較しない
        //if (!GameUtility.IsRoundScoreUp())return ;
        _roundCount=GameUtility.GetRoundCount();
        // 目標スコアと現スコアを比較
        _targetScore = MasterData.instance.GetIntMaster(_TARGET_SCORE_ID + _roundCount);
        // 不正値が返ってきたならreturn
        if (_targetScore < 0) return;
        // 一時的にボタン受付を停止
        GameUtility.SetIsPushButton(false);
        // 合計スコアと比較
        roundScore = ScoreManager.instance.GetRoundScore();
        // 目標スコアを越していたら次のラウンドへ
        RoundManager.NextRoundExecute(roundScore,_targetScore,ref _roundCount,_TARGET_SCORE_ID,_REWARD_ID);
        // 合計スコアの増加フラグをリセット
        GameUtility.SetIsRoundScoreUp(false);
        // 最終的にプレイボタンのフラグをリセット
        GameUtility.SetIsPlay(false);

    }

    public void SetBasic(int value) { _basicScore = value; }
    public void SetMagnification(int value) { _magnification = value; }
    public float GetRoundScore() {  return _roundScore; }
}
