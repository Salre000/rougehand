using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 説明を作るインターフェースを使いたいけど本体がないなどの
/// 理由から使えない場合にダミーとして作るクラス
/// </summary>
public class DommyExplanation : ExplanationInterface
{

    public System.Func<string> dommyExplanation;
    public string GetExplanation()
    {
        return dommyExplanation();
    }

    public System.Func<string> dommyExplanation2;
    public string GetExplanation2()
    {
        return dommyExplanation2();
    }

    public System.Func<string> dommyName;
    public string GetName()
    {
        return dommyName();
    }

    public System.Func<string> dommyType;
    public string GetTypes()
    {
        return dommyType();
    }
}
