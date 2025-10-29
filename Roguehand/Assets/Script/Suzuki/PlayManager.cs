using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public static PlayManager instance;
    private bool _isSetCard=false;
    // ハンド回数
    private int _checkCount;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        _checkCount =GameUtility.GetHandCount();
        // ハンド数を反映
        TextUIManager.instance.SetHandText(_checkCount.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        ScoreView();
        CheckHandCount();
    }

    private void ScoreView()
    {
        if (!_isSetCard) return;
        // スコアが役の部分に表示される
        ScoreManager.instance.PlayScoreResult();
        StartCoroutine(RoundScorePlus());
    }

    // ハンドスコアをゼロにしてラウンドを加算
    IEnumerator RoundScorePlus()
    {

        yield return new WaitForSeconds(1f);
        ScoreManager.instance.RoundScorePlus();
        _isSetCard = false;
    }

    private void CheckHandCount()
    {
        if(_checkCount==GameUtility.GetHandCount())return;

        _checkCount=GameUtility.GetHandCount();
        TextUIManager.instance.SetHandText(_checkCount.ToString() );
    }

    // プレイを押した後のカードの位置が定位置に付いたかをセットする
    public void SetCardTransComp(bool flag) { _isSetCard = flag; }

}
