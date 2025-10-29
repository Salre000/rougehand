using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ラウンドの監視をするクラス
/// </summary>
public class RoundObserver : MonoBehaviour
{

    public static RoundObserver Instance { get; private set; }

    private bool _roundEnd = false;

    private List<Action> _roundEndActions=new List<Action>();   

    public void Awake()
    {
        Instance = this;
    }


    public void LateUpdate()
    {
        if (JokerObjectUtility.PlayCheck() == false && _roundEnd == true)
        {

            // ラウンド終了のアクション
            for (int i = 0; i<_roundEndActions.Count; i++) _roundEndActions[i]();

            _roundEnd = false;
        }


        
    }

    /// <summary>
    /// ラウンドの終了を開始する関数
    /// </summary>
    public void StartRoundEnd() { _roundEnd=true; }

    /// <summary>
    /// ラウンド終了時のアクションを追加
    /// </summary>
    /// <param name="action"></param>
    public void AddRoundEndAction(System.Action action) {  _roundEndActions.Add(action); }

}
