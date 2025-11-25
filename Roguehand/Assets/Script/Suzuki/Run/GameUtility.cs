using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtility
{
    // ベースになるハンド回数
    private static int _baseHandCount = 5;
    // ベースになるディスカード回数
    private static int _baseDiscardCount = 5;
    // 変動するハンド回数
    private static int _handCount = 5;
    // 変動するディスカード回数
    private static int _discardCount = 5;
    // 累計ラウンド
    private static int _allRoundCount = 1;
    // 現ラウンド
    private static int _roundCount = 1;
    // 現アンティ
    private static int _anteCount = 1;

    // 所持金
    private static int _myMoney = 0;
    // ボタンの反応の応対
    private static bool _isPushButton = true;
    // プレイボタンが押されたか
    private static bool _isPlay = false;
    // ディスカードが押されているか
    private static bool _isDiscard = false;
    // ラウンドスコアの増加を検知
    private static bool _isRoundScoreUp = false;
    // 次のラウンドへ移動
    private static bool _isNextRound = false;
    // ラウンドクリアのリザルト状態
    private static bool _isRoundResult = false;

    // ハンド回数の最大値の設定
    public static void SetBaseHandCount(int value) {  _baseHandCount = value; }
    // ハンド回数の最大値の取得
    public static int GetBaseHandCound() {  return _baseHandCount; }
    // ハンド数を変動
    public static void SetHandCount(int value) { _handCount = value; }
    // 現在のハンド数を取得
    public static int GetHandCount() { return _handCount; }
    // ディスカード回数の最大値の設定
    public static void SetBaseDiscardCount(int value) { _baseDiscardCount = value; }
    // ディスカード回数の最大値の取得
    public static int GetBaseDiscardCound() {  return _baseDiscardCount; }
    // ディスカード数を変動
    public static void SetDiscardCount(int value) { _discardCount = value; }
    // 現在のディスカード数を取得
    public static int GetDiscardCount() { return _discardCount; }
    // 累計ラウンド数を設定
    public static void SetAllRoundCount(int value) { _allRoundCount = value; }
    // 累計ラウンド数を取得
    public static int GetAllRoundCount() { return _allRoundCount; }
    // ラウンド数を設定
    public static void SetRoundCount(int value) { _roundCount = value; }
    // ラウンド数を取得
    public static int GetRoundCount() { return _roundCount; }
    // アンティ数を設定
    public static void SetAnteCount(int value) { _anteCount = value; }
    // アンティ数を取得
    public static int GetAnteCount() { return _anteCount; }
    // 所持金の設定
    public static void SetMyMoney(int value) { _myMoney = value;TextUIManager.instance.SetMoneyText("$"+_myMoney.ToString()); }
    // 所持金の取得
    public static int GetMyMoney() { return _myMoney; }
    // ボタン受付フラグのセット
    public static void SetIsPushButton(bool value) { _isPushButton = value; }
    // ボタン受付フラグの取得
    public static bool IsPushButton() { return _isPushButton; }
    // プレイボタンが押されたかをセット
    public static void SetIsPlay(bool value) { _isPlay = value;}
    // プレイボタンが押されたかを取得
    public static bool IsPlay() { return _isPlay; }
    // ディスカードボタンが押されたかをセット
    public static void SetIsDiscard(bool value) { _isDiscard = value;}
    // ディスカードボタンが押されたかを取得
    public static bool IsDiscard() { return _isDiscard; }
    // 合計スコアが増加されたかをセット
    public static void SetIsRoundScoreUp(bool value) { _isRoundScoreUp = value; }
    // 合計スコアが増加されたかを取得
    public static bool IsRoundScoreUp() { return _isRoundScoreUp; }
    // ラウンド移行のセット
    public static void SetIsNextRound(bool value) { _isNextRound = value; }
    // ラウンド移行の取得
    public static bool IsNextRound() {  return _isNextRound; }
    // ラウンドクリアの状態をセット
    public static void SetIsRoundResult(bool value) { _isRoundResult = value; }
    // ラウンドクリアの状態を取得
    public static bool IsRoundResult() {  return _isRoundResult; }

}
