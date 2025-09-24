using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// シーン管理
public static class GameSceneManager
{
    // シーンの名前
    public const string titleScene = "TitleScene";
    public const string mainScene = "MainGame";
    public const string selectScene = "SelectScene";
    public const string resultScene = "ResultScene";
    public const string loadScene = "LoadScene";
    public const string changeScene = "ChangeScene";

    // セレクトシーンからタイトルに戻るときにだけ使う変数
    public static bool isTargetTitle = false;

    // 普通のシーン遷移
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // モードを含めた普通のシーン遷移
    public static void LoadScene(string sceneName, LoadSceneMode mode)
    {
        SceneManager.LoadScene(sceneName, mode);
    }
}
