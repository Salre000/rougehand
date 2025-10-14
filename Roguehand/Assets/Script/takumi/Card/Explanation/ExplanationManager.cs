using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 説明などに使うGUIを纏めるマネージャー
/// </summary>
public class ExplanationManager : MonoBehaviour
{
    struct Explanation 
    {
        /// <summary>
        /// 説明時の内容を返す関数
        /// 関数である理由は動的に変更が加わる可能性がある為
        /// </summary>
        public System.Func<string> actionString;

        public ExplanationInterface explanationInterface;

        public System.Func<Vector3> centerPos;

        /// <summary>
        /// 説明の描画位置に関係する列挙体
        /// </summary>
        public ExplanationInterface.ExplanationType explanationType;


    }

    /// <summary>
    /// 説明をまとめた配列
    /// </summary>
    private List<Explanation> _list;

    /// <summary>
    /// instanceをシングルトンで生成
    /// </summary>
    public static ExplanationManager instance;

    public void Awake()
    {
        instance = this;
    }
    public void OnGUI()
    {
        LostExplanation();


        for (int i = 0; i < _list.Count; i++) 
        {
            //内容に不備があるとき
            if (_list[i].actionString == null) continue;
            if (_list[i].explanationInterface == null) continue;
            if (_list[i].centerPos == null) continue;
            _list[i].explanationInterface.CreateExplanation(_list[i].explanationType, _list[i].actionString(), _list[i].centerPos());
        }


    }

    public void AddExplanation(ExplanationInterface explanationInterface, System.Func<string> func, System.Func<Vector3> func2, ExplanationInterface.ExplanationType explanationtype) 
    {
        Explanation explanation = new Explanation();

        explanation.actionString = func;
        explanation.centerPos = func2;
        explanation.explanationInterface = explanationInterface;
        explanation.explanationType = explanationtype;
        _list.Add(explanation);
    }

    public void Remove(GameObject gameObject) 
    {
        ExplanationInterface explanationInterface = gameObject.GetComponent<ExplanationInterface>();

        if (explanationInterface == null) return;

        int index = _list.FindIndex(ss => ss.explanationInterface == explanationInterface);

        if (index < 0) return;

        _list.RemoveAt(index);

    }


    /// <summary>
    /// 中身が無くなった時にその要素を破棄する関数
    /// </summary>
    private void LostExplanation() 
    {
        for (int i = 0; i < _list.Count; i++)
        {

            //内容に不備があるとき
            //オブジェクトの破壊などでも起こる
            if (_list[i].explanationInterface != null) continue;
            _list.RemoveAt(i);
            i--;

        }
    }
}
