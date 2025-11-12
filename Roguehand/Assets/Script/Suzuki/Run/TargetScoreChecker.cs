using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// スコアが目標スコアに達しているかどうか
/// 達していたらそのゲームを終了
/// </summary>
public class TargetScoreChecker : MonoBehaviour
{
    private int _roundCount = -1;
    private StringBuilder _builder = new StringBuilder();

    // Start is called before the first frame update
    void Start()
    {
        // ラウンド数と目標スコアの設定
        RoundStart();
            _builder.Clear();
        _builder.Append(MasterData.instance.GetStringMaster(IDUtility.TARGET_SCORE_ID+ _roundCount));
        TextUIManager.instance.SetLowestScoreText(_builder.ToString());
    }

    private void RoundStart()
    {
        if( _roundCount ==GameUtility.GetRoundCount() )return;
        // ラウンドカウントのセット
        _roundCount=GameUtility.GetRoundCount();
        GameUtility.SetRoundCount(_roundCount);
        _builder.Clear();
        _builder.Append(_roundCount);
        TextUIManager.instance.SetRoundText(_builder.ToString());
    }

}
