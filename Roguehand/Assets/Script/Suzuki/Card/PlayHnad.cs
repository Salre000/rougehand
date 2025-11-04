using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TextUIManager;

public class PlayHnad : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _discardButton;
    // Start is called before the first frame update
    void Start()
    {
        _playButton.onClick.AddListener(OnHandPlay);
        _discardButton.onClick.AddListener(OnHandDiscard);
    }

    /// <summary>
    /// 青いほうのボタンが押されたとき
    /// </summary>
    public void OnHandPlay()
    {
        // ボタン受付の停止中
        if (!GameUtility.IsPushButton()) return;
        if (GameUtility.IsPlay())return ;
        // ハンドの数を減らす
        int handCount =GameUtility.GetHandCount();
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

    /// <summary>
    /// 赤い方のボタンが押されたとき
    /// </summary>
    public void OnHandDiscard() 
    {
        // ボタン受付の停止中
        if (!GameUtility.IsPushButton()) return;
        if (GameUtility.IsDiscard()) return;

        // ハンドの数を減らす
        int handCount = GameUtility.GetDiscardCount();
        if (handCount <= 0) return;
        handCount--;
        // なんのカードも選択されていなければreturn
        if (CardManager.instance.GetPick().Count <= 0) return;

        GameUtility.SetDiscardCount(handCount);
        // カードが上に行く
        CardObjectUtility.Discard();

        // 手札だけすべて削除
        CardObjectUtility.PlayEnd();
        GameUtility.SetIsDiscard(true);
    }

}
