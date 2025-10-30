using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public static PlayManager instance;
    private bool _isSetCard = false;
    // ハンド回数
    private int _checkCount;

    // ディスカード回数
    private int _checkDiscardCount;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        _checkCount = GameUtility.GetHandCount();
        // ハンド数を反映
        TextUIManager.instance.SetHandText(_checkCount.ToString());

        // ディスカード数を反映
        _checkDiscardCount = GameUtility.GetDiscardCount();
        TextUIManager.instance.SetDiscardText(_checkCount.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        ScoreView();
        CheckHandCount();
        CheckDiscardCount();
    }

    private void ScoreView()
    {
        if (!_isSetCard) return;

        // ジョーカーの計算待機
        if (JokerObjectUtility.PlayCheck()) return;

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

        // 手札だけすべて削除
        CardObjectUtility.PlayEnd();

        GameUtility.SetIsRoundScoreUp(true);

    }

    private void CheckHandCount()
    {
        if (_checkCount == GameUtility.GetHandCount()) return;

        _checkCount = GameUtility.GetHandCount();
        TextUIManager.instance.SetHandText(_checkCount.ToString());
    }
    private void CheckDiscardCount()
    {
        if (_checkDiscardCount == GameUtility.GetDiscardCount()) return;

        _checkDiscardCount = GameUtility.GetDiscardCount();
        TextUIManager.instance.SetDiscardText(_checkDiscardCount.ToString());
    }

    // プレイを押した後のカードの位置が定位置に付いたかをセットする
    public void SetCardTransComp(bool flag) { _isSetCard = flag; }

}
