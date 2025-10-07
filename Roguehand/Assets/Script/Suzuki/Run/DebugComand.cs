using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UIManager;

public class DebugComand : MonoBehaviour
{
    private int _score = 0;
    private string _onePear = "ワンペア <size=20><color=#FFFFFF>レベル1";
    private float _time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreUp();
        //
        MagUp();
        RoundResult();
        ScoreReset();
    }

    void OnePear()
    {
        SetRoleText(_onePear);
        ShakeCamera.Instance.Shake(5, 0.2f);
        int score = 300;
        SetRoundScereText(score.ToString());

    }

    public void OnHandPlay()
    {
        int handCount = IntTryParse(GetHandText());
        if(handCount<=0) return;
        handCount--;
        SetHandText(handCount.ToString());
        OnePear();
        PlayManager.instance.SetIsPlay(true);
    }

    public void OnDisCard()
    {
        int disCount = IntTryParse(GetDiscardText());
        if(disCount<=0) return;
        disCount--;
        SetDiscardText(disCount.ToString());
    }

    #region スコア系
    private void ScoreUp()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ScoreManager.instance.BasicPlus(125);
        }
    }

    private void MagUp()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ScoreManager.instance.MagnificationPlus(12.2f);
        }
    }
    private void RoundResult()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ScoreManager.instance.RoundScoreResult();
    }
    private void ScoreReset()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ScoreManager.instance.ScoreReset();
        }
    }
    #endregion
}
