using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TextUIManager;

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
        CheckHand();
        ScoreUp();
        //
        MagUp();
        RoundResult();
        ScoreReset();
    }

    // プレイボタン


    public void OnDisCard()
    {
        int disCount = instance.IntTryParse(instance.GetDiscardText().text);
        if(disCount<=0) return;
        disCount--;
        instance.SetDiscardText(disCount.ToString());
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

    private void CheckHand()
    {
        if (!Input.GetKeyDown(KeyCode.Return)) return;
        // 役のチェック
        // デバック、確認として特定キーを押したら現在出せる役を表示する
        List<Card.Trump> roleCheck = CardManager.instance.GetHand();
        RoleManager.Role role = RoleManager.Role.None;
        role=RoleManager.instance.RoleCheck(roleCheck);
        Debug.Log("CheckRole is : "+role.ToString());
        List<int> indexList = new();
        indexList = RoleManager.instance.GetIndex();
        if(role==RoleManager.Role.highCard) return;

    }
}
