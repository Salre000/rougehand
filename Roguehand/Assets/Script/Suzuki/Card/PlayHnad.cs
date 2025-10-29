using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TextUIManager;

public class PlayHnad : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    // Start is called before the first frame update
    void Start()
    {
        _playButton.onClick.AddListener(OnHandPlay);
    }

    /// <summary>
    /// 青いほうのボタンが押されたとき
    /// </summary>
    public void OnHandPlay()
    {
        // ボタン受付の停止中
        if (!GameUtility.IsPushButton()) return;
        // ハンドの数を減らす
        int handCount=GameUtility.GetHandCount();
        if (handCount <= 0) return;
        handCount--;
        // なんのカードも選択されていなければreturn
        if (CardManager.instance.GetPick().Count<=0) return;
        // カードの役の判定結果をもらう
        RoleManager.Role role=RoleManager.instance.GetRole();
        // 何の役もなければreturn
        if(role==RoleManager.Role.None)return;
        GameUtility.SetHandCount(handCount);
        // カードが上に行く
        CardObjectUtility.Play();
        // 

        
        // 手札含めたすべて削除
        //CardObjectUtility.End();

        // 手札だけすべて削除
        //CardObjectUtility.PlayEnd();
        
        // プレイボタンが押されたことを知らせる
        GameUtility.SetIsPlay(true);
    }
}
