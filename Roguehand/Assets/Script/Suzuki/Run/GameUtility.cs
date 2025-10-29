using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtility
{
    // ベースになるハンド回数
    private static int _baseHandCount = 5;
    // 変動するハンド回数
    private static int _handCount = 5;
    // 現ラウンド
    private static int _roundCount = 1;
    // ボタンの反応の応対
    private static bool _isPushButton = true;
    // プレイボタンが押されたか
    private static bool _isPlay = false;
    // ラウンドスコアの増加を検知
    private static bool _isRoundScoreUp = false;
    // 次のラウンドへ移動
    private static bool _isNextRound = false;

    // ハンド回数の最大値の設定
    public static void SetBaseHandCount(int value) {  _baseHandCount = value; }
    // ハンド回数の最大値の取得
    public static int GetBaseHandCound() {  return _baseHandCount; }
    // ハンド数を変動
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
    // ラウンド移行のセット
    public static void SetIsNextRound(bool value) { _isNextRound = value; }
    // ラウンド移行の取得
    public static bool IsNextRound() {  return _isNextRound; }

}
