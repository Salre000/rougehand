using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public static PlayManager instance;

    private bool _isPlay = false;
    private int _roleNumber = -1;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    /// <summary>
    /// そろっている役があるか確認する
    /// </summary>
    /// <returns></returns>
    public int RoleCheck(List<Card.Trump> trumps)
    {
        

        return _roleNumber;
    }

    // 役の強さ順
    // ※隠し役
 
    // 革命
    // ロイヤルフラッシュ
    // ストレートフラッシュ
    // ※フェイスファイブカード
    // 
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    //
    


    public void SetIsPlay(bool isPlay) { _isPlay = isPlay; }
    public bool IsPlay() { return _isPlay; }
}
