using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 値の変動アニメーション
/// </summary>
public static class NumberFluctuation
{ 
    /// <summary>
    /// アニメーション
    /// </summary>
    /// <param name="nowNumber">増減する値</param>
    /// <param name="targetNumber">増減後の値</param>
    /// <param name="isPlus">増やすか否か</param>
    public static void FluctuationAnim(ref int nowNumber,int targetNumber,bool isPlus)
    {
        // 変動フラグがオフになったら

        // ここに変動中フラグ


        int distance=nowNumber-targetNumber;

        if (distance == 0)
        {
            // 変動中フラグをオフ
            PlayManager.instance.SetIsFluctuation(false);

            return;
        }
        else
            PlayManager.instance.SetIsFluctuation(true);

        // 増やすか否か
        if(isPlus)
            nowNumber++;
        else
            nowNumber--;

        // テキスト反映はこの関数を引用する側で行う
    }
}

