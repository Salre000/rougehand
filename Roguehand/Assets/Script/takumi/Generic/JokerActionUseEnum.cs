using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ジョーカーのアクションに使う列挙体をまとめたクラス
/// </summary>
public static class JokerActionUseEnum 
{
    //何かをした時の何かの列挙体
    public enum JokerActionTarget
    {
        /// <summary>
        /// 星座カードの使用時
        /// </summary>
        constellation,
        /// <summary>
        /// アイテムの使用時
        /// </summary>
        item,
        /// <summary>
        /// 何かしらの売却時
        /// </summary>
        sale,
        /// <summary>
        /// ハンドが役を決めたとき
        /// </summary>
        role,

        max

    }

    public static readonly string[] JokerActionTargetExplanation = new string[(int)JokerActionTarget.max+1]
    {
        "星座カードの使用時",
        "アイテムの使用時",
        "売却の使用時",
        "役の使用時",
        "未定"

    };

    /// <summary>
    /// 倍率をどのように追加するか
    /// </summary>
    public enum AddType 
    {
        /// <summary>
        /// 加算
        /// </summary>
        addition,

        /// <summary>
        /// 乗算
        /// </summary>
        Multiplication,
        max

    }

    /// <summary>
    /// どのタイミングで加算が入るか
    /// </summary>
    public enum Timing 
    {
        /// <summary>
        /// ジョーカーのターンに加算
        /// </summary>
        trun,

        /// <summary>
        /// 条件が満たされた瞬間
        /// </summary>
        now,

        /// <summary>
        /// 今後ずっとジョーカーの倍率が多くなる
        /// </summary>
        never,

        max

    }
    public static readonly string[] JokerActionTimingExplanation = new string[(int)Timing.max + 1]
{
        "ジョーカーのターンに",
        "条件が満たされた瞬間",
        "今後ずっと",
        "未定"

};




}
