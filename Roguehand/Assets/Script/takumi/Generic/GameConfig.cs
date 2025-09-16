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
    static int _gameSpeed = 1;

    /// <summary>
    /// ゲームのスピードを管理する変数を返す関数
    /// </summary>
    /// <returns></returns>
    public static int GetGameSpeed() {  return _gameSpeed; } 

}
