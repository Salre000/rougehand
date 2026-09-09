using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 説明分を描画するinterface
/// </summary>
public interface ExplanationInterface
{
    /// <summary>
    /// 名前を返す関数
    /// </summary>
    /// <returns></returns>
    public string GetName();
    /// <summary>
    /// 説明を返す関数
    /// </summary>
    /// <returns></returns>
    public string GetExplanation();
    /// <summary>
    /// 追加の説明を返す関数
    /// </summary>
    /// <returns></returns>
    public string GetExplanation2();
    /// <summary>
    /// 種類を返す関数
    /// </summary>
    /// <returns></returns>
    public string GetTypes();
}
