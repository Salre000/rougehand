using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 基本スコアと倍率のテキストと結果をいじる
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    // 場に出されたカードの下にスコアを出すためのText群
    [SerializeField] private List<GameObject> score;
    private List<TextMeshProUGUI> scoreViewTexts = new List<TextMeshProUGUI>();
    private List<Transform> scoreViewTrans = new List<Transform>();
    private List<int> scoreViewID = new List<int>();
    private List<int> scoreViewValueBase = new List<int>();
    private List<int> scoreViewValueMagnification = new List<int>();

    private int _scoreIndex = 0;
    public int SCORE_INDEX_MAX = 5;
    private int _pulsColor = IDUtility.RICHTEXT_ID + 8;
    private int _magColor = IDUtility.RICHTEXT_ID + 9;

    public void SetScoreViewID(int ID)
    {
        Debug.Log("ID:" + ID);
        if (scoreViewID.Contains(ID))
        {
            _scoreIndex--;
        }
        else
        {
            scoreViewID.Add(ID);
            scoreViewValueMagnification.Add(0);
            scoreViewValueBase.Add(0);
        }

    }
    public void SetScoreViewText(int text, bool magnification = false)
    {
        Debug.Log(_scoreIndex + ":ID" + text);
        scoreViewTexts[_scoreIndex].text = GetConnectedText(text, magnification);
        _scoreIndex++;
        //scoreViewTexts[_scoreIndex].color = (magnification)? _magColor : _pulsColor;
    }
    public void SetScoreViewTrans(Vector3 position)
    {
        position.y -= 140f;
        scoreViewTrans[_scoreIndex].position = position;


    }
    public void SetViewIndex(int index) 
    {
        _scoreIndex = index; 
        scoreViewID.Clear();

        scoreViewValueMagnification.Clear();
        scoreViewValueBase.Clear();
    }
    private string GetConnectedText(int baseText, bool magnification)
    {
        if (baseText <= 0) return"";

        if (magnification) scoreViewValueMagnification[_scoreIndex] += (baseText);
        else scoreViewValueBase[_scoreIndex] += (baseText);

        StringBuilder sb = new StringBuilder();
        sb.Clear();

        if (scoreViewValueBase[_scoreIndex] > 0 && scoreViewValueMagnification[_scoreIndex] > 0)
            scoreViewTexts[_scoreIndex].fontSize = 30;
        else scoreViewTexts[_scoreIndex].fontSize = 60;

        if (scoreViewValueBase[_scoreIndex] > 0)
        {
            sb.Append(MasterData.instance.GetStringMaster(_pulsColor));
            sb.Append("+");
            sb.Append(scoreViewValueBase[_scoreIndex]);

        }
        sb.Append(" ");

        if (scoreViewValueMagnification[_scoreIndex] > 0)
        {
            sb.Append(MasterData.instance.GetStringMaster(_magColor));
            sb.Append("x");
            sb.Append(scoreViewValueMagnification[_scoreIndex]);

        }

        return sb.ToString();



    }


    // 基本スコア
    private float _basicScore;
    // 倍率
    private float _magnification;
    // ラウンドの合計スコア
    private float _roundScore;
    // プレイしたハンドのスコア
    private float _handScore;
    // これまでのスコアで一番高い物
    private float _highScore = 0;

    private StringBuilder _builder = new StringBuilder();
    // ラウンドスコアの文字が枠外に出るくらいの文字数を検知
    private int _defaultRemit = 9;
    // 減らす文字サイズ
    private const int _DOWNSIZE = 2;
    // 元のフォントサイズ
    private const float _DEFAULT_OFFSET = 44.1f;
    // スコア時の文字が枠外に出るくらいの文字数を検知
    private int _scoreRemitLength = 9;
    // スコア時のフォントサイズ
    private const float _SCORE_OFFSET = 70f;
    // 文字数検知のリセット
    private const int _RESET_REMIT_SIZE = 9;
    // ゼロにする
    private const int _RESET_NUM = 0;

    // ラウンドスコアが加算された時true
    //private bool _additionScore = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        SCORE_INDEX_MAX = score.Count;
        for (int i = 0; i < SCORE_INDEX_MAX; i++)
        {
            scoreViewTexts.Add(score[i].GetComponent<TextMeshProUGUI>());
            scoreViewTrans.Add(score[i].GetComponent<Transform>());
            scoreViewTexts[i].text = "";
        }

    }

    private void Update()
    {
        NextRoundScoreReset();
    }

    private void NextRoundScoreReset()
    {
        // ショップが終わり次ラウンドに移行するときにリセット
        if (!ShopManager.instance.IsPushEndShop()) return;


        // 合計スコアのリセット
        ScoreManager.instance.ResetRoundScore();

        // 目標スコアの再設定
        _builder.Clear();
        int id = IDUtility.TARGET_SCORE_ID + GameUtility.GetAllRoundCount();
        //_builder.Append(MasterData.instance.GetIntMaster(id));
        _builder.AppendFormat("{0:#}", MasterData.instance.GetIntMaster(id).ToString("N0"));
        TextUIManager.instance.SetLowestScoreText(_builder.ToString());

        //リワードが最大になるラウンド数でそれ以上に行かないようにする
        int roundCount = GameUtility.GetRewardMaxCount();

        // 報酬金の再設定
        int reward = MasterData.instance.GetIntMaster(IDUtility.REWARD_ID + roundCount);
        TextUIManager.instance.SetRewardCountText(UIUtility.instance.RewardConversion(reward));
    }

    /// <summary>
    /// 基本の加算
    /// </summary>
    /// <param name="value">入れた分だけ加算</param>
    public void BasicPlus(float value)
    {
        _basicScore += value;
        _builder.Clear();
        _builder.Append(_basicScore);
        TextUIManager.instance.SetBasicScoreText(_builder.ToString());
        VolumeManager.instance.PlayScoreSE();
    }

    /// <summary>
    /// 倍率の加算
    /// </summary>
    /// <param name="value">入れた分だけ加算</param>
    public void MagnificationPlus(float value)
    {
        _magnification += value;
        _magnification = Rounding(_magnification, 2f);

        _builder.Clear();
        _builder.Append(_magnification);
        TextUIManager.instance.SetMagnificationText(_builder.ToString());
        VolumeManager.instance.PlayScoreSE();

    }

    /// <summary>
    /// 二つの結果を合計ラウンドにまとめる
    /// </summary>
    public void RoundScoreResult()
    {
        _roundScore += _handScore;
        // 四捨五入した値が返る
        _roundScore = Rounding(_roundScore, 1f);

        _builder.Clear();
        _builder.AppendFormat("{0:#}", _roundScore.ToString("N0"));

        if (_builder.Length >= _defaultRemit)
        {
            TextUIManager.instance.GetRoundScoreText().fontSize -= _DOWNSIZE;
            _defaultRemit++;
        }
        else
        {
            _defaultRemit = _RESET_REMIT_SIZE;

        }

        TextUIManager.instance.SetRoundScoreText(_builder.ToString());
    }

    /// <summary>
    /// 二つの結果を表示
    /// </summary>
    public void PlayScoreResult()
    {

        _handScore = _basicScore * _magnification;
        // 四捨五入した値が返る
        _handScore = Rounding(_handScore, 1f);

        _builder.Clear();
        _builder.AppendFormat("{0:#}", _handScore.ToString("N0"));

        // フォントサイズの調整
        if (_builder.Length >= _scoreRemitLength)
        {
            TextUIManager.instance.GetRoleText().fontSize -= _DOWNSIZE;
            _scoreRemitLength++;
        }
        TextUIManager.instance.GetRoleText().fontSize = _SCORE_OFFSET;

        TextUIManager.instance.SetRoleText(_builder.ToString());
        _scoreRemitLength = _RESET_REMIT_SIZE;

        ScoreReset();
    }

    /// <summary>
    /// 基本と倍率の表示をゼロにする
    /// </summary>
    public void ScoreReset()
    {
        // 基本と倍率をゼロにする
        _builder.Clear();
        _builder.Append(_RESET_NUM);
        TextUIManager.instance.SetBasicScoreText(_builder.ToString());
        TextUIManager.instance.SetMagnificationText(_builder.ToString());
    }

    /// <summary>
    /// 倍率を乗算
    /// </summary>
    /// <param name="value"></param>
    public void Multiplication(float value)
    {
        _magnification *= value;
    }

    /// <summary>
    /// ハンドスコアをゼロにしながらラウンドスコアに加算
    /// </summary>
    public void RoundScorePlus()
    {
        // 合計に加算
        _roundScore += _handScore;
        if (_highScore < _handScore) _highScore = _handScore;
        // 0にする
        _handScore = 0;
        // 空白にする
        _builder.Clear();
        _builder.Append("");
        TextUIManager.instance.SetRoleText(_builder.ToString());


        // ラウンドスコアを表示
        _builder.Clear();
        //_builder.Append(_roundScore);
        _builder.AppendFormat("{0:#}", _roundScore.ToString("N0"));
        TextUIManager.instance.SetRoundScoreText(_builder.ToString());

    }

    /// <summary>
    /// ラウンドスコアと目標スコアのリセット
    /// </summary>
    public void ResetRoundScore()
    {
        _roundScore = 0;
        _builder.Clear();
        _builder.Append(_roundScore);
        TextUIManager.instance.SetRoundScoreText(_builder.ToString());
        _builder.Clear();
        _builder.Append("");
        TextUIManager.instance.SetLowestScoreText(_builder.ToString());
    }

    /// <summary>
    /// 四捨五入
    /// </summary>
    /// <param name="value">したい値</param>
    /// <param name="decPoint">小数第〇を指定</param>
    /// <returns>四捨五入した値</returns>
    public float Rounding(float value, float decPoint)
    {
        // 小数部分の取り出し
        float num1 = value - Mathf.FloorToInt(value);
        // 四捨五入したい位を一の位に持ってくる
        int num2 = Mathf.FloorToInt(num1 * Mathf.Pow(10, decPoint));
        // 十以上の位をなくす
        int num3 = num2 - Mathf.FloorToInt(num2 / 10) * 10;
        if (num3 >= 5)
        {
            // 切り上げ
            // 切り上げたい位まで小数点を移動させて切り上げ
            num1 = Mathf.CeilToInt(value * Mathf.Pow(10, decPoint - 1));
            // 戻す
            num1 /= Mathf.Pow(10, decPoint - 1);
        }
        else
        {
            // 切り捨て
            // 同様に
            num1 = Mathf.FloorToInt(value * Mathf.Pow(10, decPoint - 1));
            // 戻す
            num1 /= Mathf.Pow(10, decPoint - 1);
        }

        return num1;
    }

    // 達しているか確認
    public void RoundCheck()
    {
        // 一時的にボタン受付を停止
        GameUtility.SetIsPushButton(false);

        // 目標スコアを越していたら次のラウンドへ
        if (!RoundUtility.NextStartRound()) return;

        // ゲームの速度をリセット
        GameConfig.ResetGameSpeed();


        // 最終的にプレイボタンのフラグをリセット
        GameUtility.SetIsPlay(false);



    }

    public float GetBasicScore() { return _basicScore; }
    public void SetBasic(int value) { _basicScore = value; }
    public float GetMagnification() { return _magnification; }
    public void SetMagnification(int value) { _magnification = value; }
    public float GetRoundScore() { return _roundScore; }
    public void SetRoundScore(float value) { _roundScore = value; }
    public float GetHighScore() { return _highScore; }
    public void SetHighScore(float value) { _highScore = value; }
}
