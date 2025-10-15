using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHnad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // プレイのボタンを押したら通ります。
        if(!RoleManager.instance.IsPlay()) return;
        HandRolePlay();
    }

    private void HandRolePlay()
    {
        // デバック、確認として特定キーを押したら現在出せる役を表示する
        List<Card.Trump> roleCheck = CardManager.instance.GetHand();
        RoleManager.instance.RoleCheck(roleCheck);

    }
}
