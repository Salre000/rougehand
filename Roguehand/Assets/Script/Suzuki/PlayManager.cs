using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public static PlayManager instance;

    private bool _isSetCard=false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        ScoreCCC();
    }

    private void ScoreCCC()
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

    }

    // プレイを押した後のカードの位置が定位置に付いたかをセットする
    public void SetCardTransComp(bool flag) { _isSetCard = flag; }
}
