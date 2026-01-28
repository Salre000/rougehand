using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static TextUIManager;

/// <summary>
/// ゲームの進行
/// </summary>
public class GameRoot:MonoBehaviour 
{

    public static GameRoot instance;

    [SerializeField] GameObject _dontTouchZone;
    bool _next = false;
    bool clear=false;
    bool over=false;

    private StringBuilder _builder = new StringBuilder();
    float _Dtime = 0f;

    private int _resetRoundCount = 1;

    private void Awake()
    {
        instance = this;
        // ラウンドのカウントを初期値に設定
        GameUtility .SetAllRoundCount(_resetRoundCount);
    }

    private void Start()
    {
        _dontTouchZone.SetActive(false);
    }
    private void Update()
    {
        GameOver();
        if(_next) return;
    }

    // クリアしたかどうか
    public void GameClearCheck()
    {
        if (over) return;
        if(GameUtility.GetAnteCount() != 8) return;
        if(GameUtility.GetRoundCount() != 3) return;
        over = true;
        _builder.Clear();
        _builder.Append("Clear");
        TextUIManager.instance.SetRoleText(_builder.ToString());
        // リザルト画面を開く
        ResultUIManager.Instance.Active("勝利!");

    }
    // ハンドがゼロか
    private void GameOver()
    {
        if (clear) return;
        if (0 < GameUtility.GetHandCount())
        {
            _Dtime = 0f;
            return;
        }
        
            _Dtime += Time.deltaTime;
        if(_Dtime<5f)return;

        // 合計スコアと比較
        float roundScore = ScoreManager.instance.GetRoundScore();
        roundScore = ScoreManager.instance.Rounding(roundScore, 1f);
        int _targetScore = MasterData.instance.GetIntMaster(7000 + GameUtility.GetAllRoundCount());

        if (_targetScore < roundScore) return;
        over =true;
        _builder.Clear();
        _builder.Append("Game Over");
        TextUIManager.instance.SetRoleText(_builder.ToString());
        
        // リザルト画面を開く
        ResultUIManager.Instance.Active("敗北 ");

    }


    IEnumerator NextRound()
    {
        _next = true;
        _dontTouchZone.SetActive(true);
        yield return new WaitForSeconds(1);
        //instance.InitializeText();
    }

    public bool GetGameOver() { return over;}

}
