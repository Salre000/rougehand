using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UIManager;

public class DebugComand : MonoBehaviour
{
    private int _score = 0;
    private string _onePear = "ÉèÉìÉyÉA <size=20><color=#FFFFFF>ÉåÉxÉã1";
    private float _time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreCountUP();
    }

    void ScoreCountUP()
    {
        if(!Input.GetKeyDown(KeyCode.Alpha1)) return;
        _score += 100;
        SetRoundScereText(_score.ToString());
        Debug.Log(_score.ToString());
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
    }

    public void OnDisCard()
    {
        int disCount = IntTryParse(GetDiscardText());
        if(disCount<=0) return;
        disCount--;
        SetDiscardText(disCount.ToString());
    }
}
