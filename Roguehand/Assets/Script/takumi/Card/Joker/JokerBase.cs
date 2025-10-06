using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerBase
{
    /// <summary>
    /// ジョーカーのオブジェクトの動き方
    /// </summary>
    protected int jokerObjecttype = 0;

    /// <summary>
    /// ジョーカーのオブジェクトの動き方を返す関数
    /// </summary>
    /// <returns></returns>
    public int GetJokerObjectType() {  return jokerObjecttype; }    
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
    public virtual float Trun() {  return 0; }

    /// <summary>
    /// ラウンドの終了時のジョーカーの挙動
    /// </summary>
    public virtual void RoundEnd() { }

    /// <summary>
    /// ターン事の処理をするために挟むリセットの処理
    /// </summary>
    public virtual void TrunReset() { }

    /// <summary>
    /// ジョーカーのレアリティを返す関数
    /// </summary>
    /// <returns></returns>
    public virtual JokerActionUseEnum.JokerRarity GetRarity() { return JokerActionUseEnum.JokerRarity.Common; }

    /// <summary>
    /// ジョーカーの倍率の上昇方法が加算なのか乗算なのかを表す関数
    /// </summary>
    /// <returns></returns>
    public virtual bool GetAddType() {  return true; }

}
