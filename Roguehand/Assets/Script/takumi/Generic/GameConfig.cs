using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームの設定を管理するクラス
/// </summary>
public class GameConfig
{


    /// <summary>
    /// ゲームの速度を管理する変数
    /// </summary>
    static float _gameSpeed = 1;

    static float _accelerateSpeed = 0.001f;

    /// <summary>
    /// ゲームのスピードを管理する変数を返す関数
    /// </summary>
    /// <returns></returns>
    public static float GetGameSpeed() {return _gameSpeed; }
    
    public static void AccelerateGameSpeed() { _gameSpeed += _accelerateSpeed; }

     public static void ResetGameSpeed() {  _gameSpeed = 1; }

}
