using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtility
{
    // ハンド回数
    private static int _handCount = 5;
    // 現ラウンド
    private static int _roundCount = 1;
    // ボタンの反応の応対
    private static bool _isPushButton = true;
    // プレイボタンが押されたか
    private static bool _isPlay = false;
    // ラウンドスコアの増加を検知
    private static bool _isRoundScoreUp = false;

    // ハンド数を設定
    public static void SetHandCount(int value) { _handCount = value; }
    // 現在のハンド数を取得
    public static int GetHandCount() { return _handCount; }
    // ラウンド数を設定
    public static void SetRoundCount(int value) { _roundCount = value; }
    // ラウンド数を取得
    public static int GetRoundCount() { return _roundCount; }
    // ボタン受付フラグのセット
    public static void SetIsPushButton(bool value) { _isPushButton = value; }
    // ボタン受付フラグの取得
    public static bool IsPushButton() { return _isPushButton; }
    // プレイボタンが押されたかをセット
    public static void SetIsPlay(bool value) { _isPlay = value;}
    // プレイボタンが押されたかを取得
    public static bool IsPlay() { return _isPlay; }
    // 合計スコアが増加されたかをセット
    public static void SetIsRoundScoreUp(bool value) { _isRoundScoreUp = value; }
    // 合計スコアが増加されたかを取得
    public static bool IsRoundScoreUp() { return _isRoundScoreUp; }

}
