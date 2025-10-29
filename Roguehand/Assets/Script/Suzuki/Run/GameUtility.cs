using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtility
{
    // ハンド回数
    private static int _handCount = 5;
    // 現ラウンド
    private static int _roundCount = 0;

    // ハンド数を設定
    public static void SetHandCount(int value) { _handCount = value; }
    // 現在のハンド数を取得
    public static int GetHandCount() { return _handCount; }
    // ラウンド数を設定
    public static void SetRoundCount(int value) { _roundCount = value; }
    // ラウンド数を取得
    public static int GetRoundCount() { return _roundCount; }
}
