using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class PlayHnad : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _discardButton;
    private StringBuilder _builder=new StringBuilder();
    private int _RESET_NUM = 0;


    // Start is called before the first frame update
    void Start()
    {
        _playButton.onClick.AddListener(OnHandPlay);
        _discardButton.onClick.AddListener(OnHandDiscard);
        CommandUpData.instance.SetPlay(OnHandPlay);
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

        // 役をプレイした回数を追加
        RoleManager.instance.AddRolePlayCountList(role);

        // プレイボタンが押されたことを知らせる
        GameUtility.SetIsPlay(true);
        if (handCount <= 1) VolumeManager.instance.UpBGM();
    }

    /// <summary>
    /// 赤い方のボタンが押されたとき
    /// </summary>
    public void OnHandDiscard() 
    {
        // ボタン受付の停止中
        if (!GameUtility.IsPushButton()) return;
        if (GameUtility.IsDiscard()) return;

        // ディスカードの数を減らす
        int handCount = GameUtility.GetDiscardCount();
        if (handCount <= 0) return;
        handCount--;
        // なんのカードも選択されていなければreturn
        if (CardManager.instance.GetPick().Count <= 0) return;

        GameUtility.SetDiscardCount(handCount);
        // カードが上に行く
        CardObjectUtility.Discard();

        // 空白にする
        _builder.Clear();
        _builder.Append("");
        TextUIManager.instance.SetRoleText(_builder.ToString());
        // ゼロにする
        _builder.Clear();
        _builder.Append(_RESET_NUM);
        TextUIManager.instance.SetBasicScoreText(_builder.ToString());
        TextUIManager.instance.SetMagnificationText(_builder.ToString());

        // 手札だけすべて削除
        CardObjectUtility.PlayEnd();
        GameUtility.SetIsDiscard(true);
    }

}
