using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtility
{
    // ハンド回数
    private static int _handCount = 5;
    // 現ラウンド
    private static int _roundCount = 0;
    // ボタンの反応の応対
    private static bool _isPushButton = true;

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

}
