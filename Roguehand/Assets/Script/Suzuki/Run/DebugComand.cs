using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TextUIManager;

public class DebugComand : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckHand();

    }


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
