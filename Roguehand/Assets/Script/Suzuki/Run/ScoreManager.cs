using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

/// <summary>
/// 基本スコアと倍率のテキストと結果をいじる
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    // 基本スコア
    TextMeshProUGUI _basicScoreText;
    private float _basicScore;
    // 倍率
    TextMeshProUGUI _magnificationText;
    private float _magnification;

    TextMeshProUGUI _roundScoreText;
    float _roundScore;

    private StringBuilder builder;
    // ラウンドスコアの文字が枠外に出るくらいの文字数を検知
    private int _remit = 9;
    // 減らす文字サイズ
    private const int _DOWNSIZE = 2;
    // 元のフォントサイズ
    private const float _OFFSET=44.1f;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        builder = new StringBuilder();
        _basicScoreText = GameObject.Find("BasicScoreText").GetComponent<TextMeshProUGUI>();
        _magnificationText = GameObject.Find("MagnificationText").GetComponent<TextMeshProUGUI>();
        _roundScoreText = GameObject.Find("RoundScoreText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 基本の加算
    /// </summary>
    /// <param name="value">入れた分だけ加算</param>
    public void BasicPlus(float value)
    {
        _basicScore += value;
        builder.Clear();
        builder.Append(_basicScore);
        _basicScoreText.text = builder.ToString();

    }

    /// <summary>
    /// 倍率の加算
    /// </summary>
    /// <param name="value">入れた分だけ加算</param>
    public void MagnificationPlus(float value)
    {
        _magnification += value;

        Console.Clear();
        Debug.Log("倍率");
        Debug.Log("前 : "+ _magnification);
        _magnification = Rounding(_magnification,2f);
        Debug.Log("後 : "+_magnification);


        builder.Clear();
        builder.Append(_magnification);
        _magnificationText.text = builder.ToString();

    }

    /// <summary>
    /// 二つの結果をまとめる
    /// </summary>
    public void RoundScoreResult()
    {
        _roundScore=_basicScore*_magnification;

        Debug.Log("ラウンドスコア");
        Debug.Log("前 : "+ _roundScore);
        _roundScore = Rounding(_roundScore,1f);
        Debug.Log("後 : "+ _roundScore);
        builder.Clear();
        builder.AppendFormat("{0:#}", _roundScore.ToString("N0"));
        
        if(builder.Length >= _remit)
        {
            _roundScoreText.fontSize -= _DOWNSIZE;
            _remit++;
        }

        _roundScoreText.text = builder.ToString();
    }

    /// <summary>
    /// 基本と倍率と結果の表示と中身をゼロにする
    /// </summary>
    public void ScoreReset()
    {
        builder.Clear();
        _roundScore = _magnification =_basicScore = 0;
        _remit = 9;
        _roundScoreText.fontSize=_OFFSET;
        builder.Append(_basicScore);
        _basicScoreText.text = builder.ToString();
        _magnificationText.text = builder.ToString();
        _roundScoreText.text= builder.ToString();
    }

    /// <summary>
    /// 倍率を乗算
    /// </summary>
    /// <param name="value"></param>
    public void Multiplication(float value)
    {
        _magnification*=value;
    }

    /// <summary>
    /// 四捨五入
    /// </summary>
    /// <param name="value">したい値</param>
    /// <param name="decPoint">小数第〇を指定</param>
    /// <returns>四捨五入した値</returns>
    private float Rounding(float value,float decPoint)
    {
        // 小数部分の取り出し
        float num1=value-Mathf.FloorToInt(value);
        // 四捨五入したい位を一の位に持ってくる
        int num2 = Mathf.FloorToInt(num1 * Mathf.Pow(10, decPoint));
        // 十以上の位をなくす
        int num3 = num2 - Mathf.FloorToInt(num2 / 10) * 10;
        if( num3 >= 5 )
        {
            // 切り上げ
            // 切り上げたい位まで小数点を移動させて切り上げ
            num1= Mathf.CeilToInt(value * Mathf.Pow(10, decPoint - 1));
            // 戻す
            num1 /= Mathf.Pow(10, decPoint - 1);
        }
        else
        {
            // 切り捨て
            // 同様に
            num1 =Mathf.FloorToInt(value * Mathf.Pow(10, decPoint - 1));
            // 戻す
            num1 /= Mathf.Pow(10, decPoint - 1);
        }

        return num1;
    }
}
