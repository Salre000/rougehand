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

    private float _roundScore = 0f;
    // ラウンドスコアのリセット時に処理されないように使用する
    private float _scoreZeroChecker = 1f;
    // ラウンドスコアに追加するタイミングを秒単位で遅らせる
    private const float _WAIT_TIME = 1f;

    private bool _isFluctuation = false;
    private bool _isShack = false;

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

        // トランプの行動待ち
        if (CardObjectUtility.GetActionCount() > 0) return;

        // ジョーカーの計算待機
        if (JokerObjectUtility.PlayCheck()) return;

        // スコアが役の部分に表示される
        ScoreManager.instance.PlayScoreResult();
        if (!_isShack)
            ShakeCamera.Instance.Shake(5, 0.2f);
        _isShack = true;
        StartCoroutine(RoundScorePlus());
    }


    // ハンドスコアをゼロにしてラウンドスコアを加算
    IEnumerator RoundScorePlus()
    {

        yield return new WaitForSeconds(_WAIT_TIME);
        if (!_isSetCard) yield break;

        ScoreManager.instance.RoundScorePlus();
        _isSetCard = false;

        // 手札だけすべて削除
        CardObjectUtility.PlayEnd();


        _roundScore = ScoreManager.instance.GetRoundScore();
        // スコアが目標に達しているか確認
        ScoreManager.instance.RoundCheck();
        // 増加の確認
        GameUtility.SetIsRoundScoreUp(true);
        _isShack = false;





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

    public void SetIsFluctuation(bool flag) { _isFluctuation = flag; }
    public bool IsFluctuation() { return _isFluctuation; }
}
