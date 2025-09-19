using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerBase
{
    /// <summary>
    /// ラウンドの開始時のジョーカーの挙動
    /// </summary>
    public virtual void RoundStart() { }

    /// <summary>
    /// 常に回すジョーカーの挙動（基本的に直ぐにリターンで返す関数）
    /// </summary>
    public virtual void UpData() { }

    /// <summary>
    /// ジョーカーのターンが回って来た時に動く挙動
    /// </summary>
    /// <returns><基本ゼロだけどこれが倍率増加量/returns>
    public virtual int Trun() {  return 0; }

    /// <summary>
    /// ラウンドの終了時のジョーカーの挙動
    /// </summary>
    public virtual void RoundEnd() { }


}
