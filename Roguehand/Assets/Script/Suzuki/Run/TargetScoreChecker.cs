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
    private const int _TARGET_SCORE_ID = 7000;
    private int _targetScore;
    private int _roundCount = -1;
    private StringBuilder _builder = new StringBuilder();

    // Start is called before the first frame update
    void Start()
    {
        // ラウンド数と目標スコアの設定
        RoundStart();
        _builder.Append(StringMaster.instance.GetMaster(_TARGET_SCORE_ID + _roundCount));
        TextUIManager.instance.SetLowestScoreText(_builder.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RoundStart()
    {
        if( _roundCount ==GameUtility.GetRoundCount() )return;
        _roundCount=GameUtility.GetRoundCount();
    }

    // 達しているか確認
    private void RoundCheck()
    {

    }

}
