using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UIManager;

public class PlayHnad : MonoBehaviour
{
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button=GameObject.Find("").GetComponent<Button>();
        //button.onClick.AddListener(value=>OnHandPlay());
    }

    // Update is called once per frame
    void Update()
    {
        // プレイのボタンを押したら通ります。
        //if(!RoleManager.instance.IsPlay()) return;
        //HandRolePlay();
    }

    private void HandRolePlay()
    {
        // デバック、確認として特定キーを押したら現在出せる役を表示する
        List<Card.Trump> roleCheck = CardManager.instance.GetHand();
        RoleManager.instance.RoleCheck(roleCheck);

    }
    public void OnHandPlay()
    {
        // なんのカードも選択されていなければreturn
        if (CardManager.instance.GetPick().Count<=0) return;
    }
}
